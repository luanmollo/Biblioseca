using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Categories.Errors
{
    public partial class ThereAreNotCategoriesError : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkCreateNewCategory_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Categories/Create.aspx");
        }
    }
}