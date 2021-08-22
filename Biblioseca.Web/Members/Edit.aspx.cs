using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;

namespace Biblioseca.Web.Members
{
    public partial class Edit : BasePage
    {
        private readonly MemberDao memberDao = new MemberDao(Global.SessionFactory);
        private int memberId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.memberId = Convert.ToInt32(Request.QueryString.Get("id"));

            if (!this.IsPostBack)
            {
                this.BindMember();
            }
        }

        private void BindMember()
        {
            MemberService memberService = new MemberService(this.memberDao);
            Member member = memberService.Get(this.memberId);
            Ensure.NotNull(member, "El socio no existe. ");

            this.textBoxFirstName.Text = member.FirstName;
            this.textBoxLastName.Text = member.LastName;
            this.textBoxUserName.Text = member.UserName;
        }

        protected void ButtonEditMember_Click(object sender, EventArgs e)
        {
            MemberService memberService = new MemberService(memberDao);
            Member member = memberService.Get(this.memberId);
            Ensure.NotNull(member, "El socio no existe. ");

            member.FirstName = this.textBoxFirstName.Text;
            member.LastName = this.textBoxLastName.Text;
            member.UserName = this.textBoxUserName.Text;

            memberDao.Save(member);

            Response.Redirect("~/Members/Index.aspx");
        }
    }
}