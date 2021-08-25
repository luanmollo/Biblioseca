using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Authors.Errors
{
    public partial class ThereAreNotAuthorsError : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkCreateNewAuthor_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Authors/Create.aspx");
        }
    }
}