using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class CategoryService
    {
        private readonly CategoryDao categoryDao;

        public CategoryService(CategoryDao categoryDao)
        {
            this.categoryDao = categoryDao;
        }

        public Category Get(int categoryId)
        {
            return this.categoryDao.Get(categoryId);
        }

        public IEnumerable<Category> List()
        {
            IEnumerable<Category> categories = this.categoryDao.GetAll();
            Ensure.IsTrue(categories.Any(), "No hay categorías para listar");

            return categories;
        }

    }
}
