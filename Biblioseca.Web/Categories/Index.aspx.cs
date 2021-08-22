using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;

namespace Biblioseca.Web.Categories
{
    public partial class Index : BasePage
    {
        private readonly CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            CategoryService categoryService = new CategoryService(categoryDao);

            this.GridViewCategories.DataSource = categoryService.List();
            this.GridViewCategories.DataBind();
        }

        protected void LinkCreateNewCategory_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Categories/Create.aspx");
        }

        protected void GridViewCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int categoryId = Convert.ToInt32(this.GridViewCategories.DataKeys[e.NewEditIndex]?.Values?[0]);
            Response.Redirect(string.Format("~/Categories/Edit.aspx?id={0}", categoryId));
        }

        protected void GridViewCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int categoryId = Convert.ToInt32(this.GridViewCategories.DataKeys[e.RowIndex]?.Values?[0]);
            Category category = this.categoryDao.Get(categoryId);
            Ensure.NotNull(category, "La categoría no existe. ");

            category.MarkAsDeleted();
            this.categoryDao.Save(category);

            this.BindGrid();
            this.PageReload();
        }
    }
}