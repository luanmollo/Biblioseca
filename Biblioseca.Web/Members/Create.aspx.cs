using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Service;

namespace Biblioseca.Web.Members
{
    public partial class Create : BasePage
    {
        private readonly MemberDao memberDao = new MemberDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonCreateMember_Click(object sender, EventArgs e)
        {
            MemberService memberService = new MemberService(memberDao);

            Member member = new Member
            {
                FirstName = textBoxFirstName.Text,
                LastName = textBoxLastName.Text,
                UserName = textBoxUserName.Text
            };

            memberDao.Save(member);

            Response.Redirect("~/Members/Index.aspx");
        }
    }
}