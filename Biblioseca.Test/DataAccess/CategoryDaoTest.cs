using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Categories;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NUnit.Framework;

namespace Biblioseca.Test.DataAccess
{
    [TestFixture]
    public class CategoryDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [SetUp]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
        }

        [TearDown]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [Test]
        public void GetAll()
        {
            CategoryDao categoryDao = new CategoryDao(this.sessionFactory);

            IEnumerable<Category> categories = categoryDao.GetAll();

            Assert.IsTrue(categories.Any());
        }

        [Test]
        public void GetByName()
        {
            CategoryDao categoryDao = new CategoryDao(this.sessionFactory);

            CategoryFilterDto categoryFilterDto = new CategoryFilterDto
            {
                Name = "aventuras"
            };

            IEnumerable<Category> categories = categoryDao.GetByFilter(categoryFilterDto);

            Assert.IsTrue(categories.Any());
        }
    }
}
