using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Books;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;

namespace Biblioseca.Web.Books
{
    public partial class Index : BasePage
    {
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            BookService bookService = new BookService(this.bookDao);
            BookError bookError = bookService.ThereAreBooks();

            if (bookError.HasError)
            {
                Response.Redirect("~/Books/Errors/ThereAreNotBooksError.aspx");
            }

            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            BookService bookService = new BookService(bookDao);

            this.GridViewBooks.DataSource = bookService.List();
            this.GridViewBooks.DataBind();
        }

        protected void LinkCreateBook_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Books/Create.aspx");
        }

        protected void GridViewBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int authorId = Convert.ToInt32(this.GridViewBooks.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect(string.Format("~/Books/Edit.aspx?id={0}", authorId));
        }

        protected void GridViewBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookId = Convert.ToInt32(this.GridViewBooks.DataKeys[e.RowIndex].Values[0]);
            Response.Redirect(string.Format("~/Books/SureToDelete.aspx?id={0}", bookId));
        }
    }
}