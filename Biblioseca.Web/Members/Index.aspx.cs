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
    public partial class Index : BasePage
    {
        private readonly MemberDao memberDao = new MemberDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            MemberService memberService = new MemberService(memberDao);

            this.GridViewMembers.DataSource = memberService.List();
            this.GridViewMembers.DataBind();
        }

        protected void LinkCreateNewMember_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Members/Create.aspx");
        }

        protected void GridViewMembers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int memberId = Convert.ToInt32(this.GridViewMembers.DataKeys[e.NewEditIndex]?.Values?[0]);
            Response.Redirect(string.Format("~/Members/Edit.aspx?id={0}", memberId));
        }

        protected void GridViewMembers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int memberId = Convert.ToInt32(this.GridViewMembers.DataKeys[e.RowIndex]?.Values?[0]);
            Member member = this.memberDao.Get(memberId);
            Ensure.NotNull(member, "El socio no existe. ");

            member.MarkAsDeleted();
            this.memberDao.Save(member);

            this.BindGrid();
            this.PageReload();
        }
    }
}