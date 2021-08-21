using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Biblioseca.Test.Services
{
    [TestFixture]

    public class CategoryServiceTest
    {
        private CategoryService categoryService;
        private Mock<CategoryDao> categoryDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [SetUp]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.categoryDao = new Mock<CategoryDao>(this.sessionFactory.Object);
        }

        [Test]
        public void Get()
        {
            int categoryId = 1;

            this.categoryDao.Setup(x => x.Get(categoryId)).Returns(new Category { Id = 1 });

            CategoryService categoryService = new CategoryService(this.categoryDao.Object);

            Category category = categoryService.Get(categoryId);

            Assert.NotNull(category);
        }

        [Test]
        public void GetAll()
        {
            this.categoryDao.Setup(x => x.GetAll()).Returns(GetCategories());

            CategoryService categoryService = new CategoryService(this.categoryDao.Object);

            IEnumerable<Category> categories = categoryService.GetAll();

            Assert.NotNull(categories);
        }

        [Test]
        public void List()
        {
            this.categoryDao.Setup(x => x.GetAll()).Returns(GetCategories());
            this.categoryService = new CategoryService(this.categoryDao.Object);

            IEnumerable<Category> categories = categoryService.List();
            Assert.IsTrue(categories.Any());

        }

        [Test]
        public void ListWhenThereAreNotCategories()
        {
            this.categoryDao.Setup(x => x.GetAll()).Returns(new List<Category>());
            this.categoryService = new CategoryService(this.categoryDao.Object);

            Assert.Throws<BusinessRuleException>(() => this.categoryService.List(),
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
