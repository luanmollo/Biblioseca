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
    public partial class Edit : BasePage
    {
        private readonly CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
        private int categoryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.categoryId = Convert.ToInt32(Request.QueryString.Get("id"));

            if (!this.IsPostBack)
            {
                this.BindCategory();
            }
        }

        private void BindCategory()
        {
            CategoryService categoryService = new CategoryService(this.categoryDao);
            Category category = categoryService.Get(this.categoryId);
            Ensure.NotNull(category, "La categoría no existe. ");

            this.textBoxName.Text = category.Name;
        }

        protected void ButtonEditCategory_Click(object sender, EventArgs e)
        {
            CategoryService categoryService = new CategoryService(categoryDao);
            Category category = categoryService.Get(this.categoryId);
            Ensure.NotNull(category, "La categoría no existe. ");

            category.Name = this.textBoxName.Text;

            categoryDao.Save(category);

            Response.Redirect("~/Categories/Index.aspx");
        }
    }
}