using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Books;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Web.Books
{
    public partial class SureToDelete : BasePage
    {
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private int bookId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.bookId = Convert.ToInt32(Request.QueryString.Get("id"));
            this.title.InnerText = "¿Seguro de que querés eliminar el libro '" + this.bookDao.Get(bookId).Title + "'?";

        }

        protected void LinkDeleteBook_OnClick(object sender, EventArgs e)
        {
            Book book = this.bookDao.Get(bookId);
            Ensure.NotNull(book, "El libro no existe. ");

            book.MarkAsDeleted();
            this.bookDao.Save(book);

            Response.Redirect("~/Books/Index.aspx");
        }


        protected void LinkDoNotDeleteBook_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Books/Index.aspx");
        }
    }
}