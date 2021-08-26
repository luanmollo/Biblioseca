using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Web.Categories
{
    public partial class SureToDelete : BasePage
    {
        private readonly CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
        private int categoryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.categoryId = Convert.ToInt32(Request.QueryString.Get("id"));
            this.title.InnerText = "¿Seguro de que querés eliminar la categoría " + this.categoryDao.Get(categoryId).Name + "?";
        }

        protected void LinkDeleteCategory_OnClick(object sender, EventArgs e)
        {
            Category category = this.categoryDao.Get(categoryId);
            Ensure.NotNull(category, "La categoría no existe. ");

            category.MarkAsDeleted();
            this.categoryDao.Save(category);

            Response.Redirect("~/Categories/Index.aspx");
        }


        protected void LinkDoNotDeleteCategory_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Categories/Index.aspx");
        }
    }
}