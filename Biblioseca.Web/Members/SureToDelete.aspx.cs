using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Web.Members
{
    public partial class SureToDelete : BasePage
    {
        private readonly MemberDao memberDao = new MemberDao(Global.SessionFactory);
        private int memberId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.memberId = Convert.ToInt32(Request.QueryString.Get("id"));
            this.title.InnerText = "¿Seguro de que querés eliminar el socio " + this.memberDao.Get(memberId).FullName + "?";
        }

        protected void LinkDeleteMember_OnClick(object sender, EventArgs e)
        {
            Member member = this.memberDao.Get(memberId);
            Ensure.NotNull(member, "El socio no existe. ");

            member.MarkAsDeleted();
            this.memberDao.Save(member);

            Response.Redirect("~/Members/Index.aspx");
        }


        protected void LinkDoNotDeleteMember_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Members/Index.aspx");
        }
    }
}