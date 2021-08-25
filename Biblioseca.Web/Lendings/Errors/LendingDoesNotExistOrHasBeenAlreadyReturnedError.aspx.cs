using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Lendings.Errors
{
    public partial class LendingDoesNotExistOrHasBeenAlreadyReturnedError : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LinkBackToLendings_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Lendings/Index.aspx");
        }
    }
}