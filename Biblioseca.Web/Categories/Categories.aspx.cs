using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Service;

namespace Biblioseca.Web
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
            CategoryService categoryService = new CategoryService(categoryDao);

            this.GridViewCategories.DataSource = categoryService.List();
            this.GridViewCategories.DataBind();
        }
    }
}