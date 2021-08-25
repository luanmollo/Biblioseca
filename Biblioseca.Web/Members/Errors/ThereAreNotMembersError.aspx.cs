using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Members.Errors
{
    public partial class ThereAreNotMembersError : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkCreateNewMember_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Members/Create.aspx");
        }
    }
}