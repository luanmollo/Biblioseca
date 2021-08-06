using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Authors.Filters;
using Biblioseca.DataAccess.Books;
using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Biblioseca.Test.DataAccess
{
    [TestClass]
    public class AuthorDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
        public void GetAll()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IEnumerable<Author> authors = authorDao.GetAll();

            Assert.IsTrue(authors.Any());
        }

        [TestMethod]
        public void GetByHqlQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("FirstName", "%William%");

            Author author = authorDao.GetUniqueByHqlQuery("FROM Author WHERE FirstName LIKE :FirstName", parameters);

            Assert.IsNotNull(author);
            Assert.AreEqual("William", author.FirstName);
        }

        [TestMethod]
        public void GetByQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "FirstName", "William" } };
            Author author = authorDao.GetUniqueByQuery(parameters);

            Assert.IsNotNull(author);
            Assert.AreEqual("William", author.FirstName);
        }

        [TestMethod]
        public void GetByFilter()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            AuthorFilterDto authorFilterDto = new AuthorFilterDto
            {
                FirstName = "Julio",
                LastName = "Verne"
            };

            IEnumerable<Author> authors = authorDao.GetByFilter(authorFilterDto);

            Assert.IsTrue(authors.Any());
        }
    }
}