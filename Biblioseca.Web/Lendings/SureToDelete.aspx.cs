using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Web.Lendings
{
    public partial class SureToDelete : BasePage
    {
        private readonly LendingDao lendingDao = new LendingDao(Global.SessionFactory);
        private int lendingId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lendingId = Convert.ToInt32(Request.QueryString.Get("id"));
            this.title.InnerText = "¿Seguro de que querés eliminar el préstamo del libro '" + this.lendingDao.Get(lendingId).Book.Title + "' prestado a " + this.lendingDao.Get(lendingId).Member.FullName + "?";
        }

        protected void LinkDeleteLending_OnClick(object sender, EventArgs e)
        {
            Lending lending = this.lendingDao.Get(lendingId);
            Ensure.NotNull(lending, "El préstamo no existe. ");

            lending.MarkAsDeleted();
            this.lendingDao.Save(lending);

            Response.Redirect("~/Lendings/Index.aspx");
        }


        protected void LinkDoNotDeleteLending_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Lendings/Index.aspx");
        }
    }
}