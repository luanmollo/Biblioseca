using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using Biblioseca.Service;

namespace Biblioseca.Web.Categories
{
    public partial class Create : BasePage
    {
        private readonly CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonCreateCategory_Click(object sender, EventArgs e)
        {
            CategoryService categoryService = new CategoryService(categoryDao);

            Category category = new Category
            {
                Name = textBoxName.Text,
            };

            categoryDao.Save(category);

            Response.Redirect("~/Categories/Index.aspx");
        }
    }
}