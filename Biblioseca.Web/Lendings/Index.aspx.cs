using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;

namespace Biblioseca.Web.Lendings
{
    public partial class Index : BasePage
    {
        private readonly LendingDao lendingDao = new LendingDao(Global.SessionFactory);
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
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
            LendingService lendingService = new LendingService(lendingDao, bookDao, memberDao);

            this.GridViewLendings.DataSource = lendingService.List();
            this.GridViewLendings.DataBind();
        }

        protected void LinkCreateNewLending_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Lendings/Create.aspx");
        }

        protected void LinkReturnLending_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Lendings/Return.aspx");
        }

        protected void GridViewLendings_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int lendingId = Convert.ToInt32(this.GridViewLendings.DataKeys[e.NewEditIndex]?.Values?[0]);
            Response.Redirect(string.Format("~/Lendings/Edit.aspx?id={0}", lendingId));
        }

        protected void GridViewLendings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int lendingId = Convert.ToInt32(this.GridViewLendings.DataKeys[e.RowIndex]?.Values?[0]);
            Lending lending = this.lendingDao.Get(lendingId);
            Ensure.NotNull(lending, "El préstamo no existe. ");

            lending.MarkAsDeleted();
            this.lendingDao.Save(lending);

            this.BindGrid();
            this.PageReload();
        }


    }
}