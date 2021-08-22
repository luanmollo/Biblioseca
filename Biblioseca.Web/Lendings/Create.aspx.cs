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
using Biblioseca.Service;

namespace Biblioseca.Web.Lendings
{
    public partial class Create : System.Web.UI.Page
    {
        private readonly LendingDao lendingDao = new LendingDao(Global.SessionFactory);
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly MemberDao memberDao = new MemberDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
            }
        }

        private void BindData()
        {
            BindBooks();
            BindMembers();
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

        protected void ButtonCreateLending_Click(object sender, EventArgs e)
        {
            BookService bookService = new BookService(this.bookDao);
            Book book = bookService.Get(Convert.ToInt32(this.bookList.SelectedValue));

            MemberService memberService = new MemberService(this.memberDao);
            Member member = memberService.Get(Convert.ToInt32(this.memberList.SelectedValue));

            LendingService lendingService = new LendingService(lendingDao, bookDao, memberDao);

            Lending lending = lendingService.LendABook(book.Id, member.Id);

            lendingDao.Save(lending);

            Response.Redirect("~/Lendings/Index.aspx");
        }
    }
}