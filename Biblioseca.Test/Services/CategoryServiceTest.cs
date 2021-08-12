using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Services
{
    [TestClass]

    public class CategoryServiceTest
    {
        private CategoryService categoryService;
        private Mock<CategoryDao> categoryDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.categoryDao = new Mock<CategoryDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void List()
        {
            this.categoryDao.Setup(x => x.GetAll()).Returns(GetCategories());
            this.categoryService = new CategoryService(this.categoryDao.Object);

            IEnumerable<Category> categories = categoryService.List();
            Assert.IsTrue(categories.Any());

        }

        [TestMethod]
        public void ListWhenThereAreNotCategories()
        {
            this.categoryDao.Setup(x => x.GetAll()).Returns(new List<Category>());
            this.categoryService = new CategoryService(this.categoryDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.categoryService.List(),
                "No hay categorías para listar. ");
        }

        private static IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>
            {
                new Category
                {
                    Id = 1
                }
            };

            return categories;
        }
    }
}
