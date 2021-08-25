using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Books.Errors
{
    public partial class ThereAreNotBooksError : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkCreateNewBook_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Books/Create.aspx");
        }
    }
}