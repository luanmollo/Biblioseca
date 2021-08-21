using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Members;
using Biblioseca.Service;

namespace Biblioseca.Web
{
    public partial class Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemberDao memberDao = new MemberDao(Global.SessionFactory);
            MemberService memberService = new MemberService(memberDao);

            this.GridViewMembers.DataSource = memberService.List();
            this.GridViewMembers.DataBind();
        }
    }
}