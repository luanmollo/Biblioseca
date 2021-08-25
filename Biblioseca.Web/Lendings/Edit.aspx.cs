using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;

namespace Biblioseca.Web.Lendings
{
    public partial class Edit : BasePage
    {
        private readonly LendingDao lendingDao = new LendingDao(Global.SessionFactory);
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly MemberDao memberDao = new MemberDao(Global.SessionFactory);
        private int lendingId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lendingId = Convert.ToInt32(Request.QueryString.Get("id"));

            if (!this.IsPostBack)
            {
                this.BindData();
            }
        }

        private void BindData()
        {
            BindBooks();
            BindMembers();
            BindLending();
        }

        private void BindBooks()
        {
            BookService bookService = new BookService(this.bookDao);
            this.bookList.DataValueField = nameof(Book.Id);
            this.bookList.DataTextField = nameof(Book.Title);
            this.bookList.DataSource = bookService.ListAvailableBooks();
            this.bookList.DataBind();
        }

        private void BindMembers()
        {
            MemberService memberService = new MemberService(this.memberDao);
            this.memberList.DataValueField = nameof(Member.Id);
            this.memberList.DataTextField = nameof(Member.FullName);
            this.memberList.DataSource = memberService.List();
            this.memberList.DataBind();
        }

        private void BindLending()
        {
            LendingService lendingService = new LendingService(this.lendingDao, this.bookDao, this.memberDao);
            Lending lending = lendingService.Get(this.lendingId);
            Ensure.NotNull(lending, "El préstamo no existe. ");

            this.bookList.SelectedValue = lending.Book.Id.ToString();
            this.memberList.SelectedValue = lending.Member.Id.ToString();

        }

        protected void ButtonEditLending_Click(object sender, EventArgs e)
        {
            MemberService memberService = new MemberService(this.memberDao);
            Member member = memberService.Get(Convert.ToInt32(this.memberList.SelectedValue));
            Ensure.NotNull(member, "El socio no existe. ");

            BookService bookService = new BookService(this.bookDao);
            Book book = bookService.Get(Convert.ToInt32(this.bookList.SelectedValue));
            Ensure.NotNull(book, "El libro no existe. ");

            LendingService lendingService = new LendingService(this.lendingDao, this.bookDao, this.memberDao);
            Lending lending = lendingService.Get(this.lendingId);

            LendingError lendingError = lendingService.CanGetLending(member.Id);
            
            if (lendingError.HasError)
            {
                Response.Redirect("~/Lendings/Errors/MemberCanNotGetLendingError.aspx");
            }

            lending.Book = book;
            lending.Member = member;

            //solamente se puede cambiar el libro y el socio, no se puede cambiar la fecha de prestamo
            lendingDao.Save(lending);

            Response.Redirect("~/Lendings/Index.aspx");
        }
    }
}