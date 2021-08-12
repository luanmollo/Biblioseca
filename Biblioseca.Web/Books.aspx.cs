using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Books;
using Biblioseca.Service;

namespace Biblioseca.Web
{
    public partial class Books : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BookDao bookDao = new BookDao(Global.SessionFactory);
            BookService bookService = new BookService(bookDao);

            this.GridViewBooks.DataSource = bookService.List();
            this.GridViewBooks.DataBind();
        }
    }
}