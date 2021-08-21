﻿using System;
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

        public IEnumerable<Category> GetAll()
        {
            return this.categoryDao.GetAll();
        }

        public Category Get(int id)
        {
            return this.categoryDao.Get(id);
        }

        public IEnumerable<Category> List()
        {
            IEnumerable<Category> categories = this.categoryDao.GetAll();
            Ensure.IsTrue(categories.Any(), "No hay categorías para listar");

            return categories;
        }

    }
}
