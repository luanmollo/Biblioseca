using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NUnit.Framework;

namespace Biblioseca.Test.DataAccess
{
    [TestFixture]
    public class AuthorDaoTest
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
        public void Get()
        {
            int authorId = 1;

            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            Author author = authorDao.Get(authorId);

            Assert.IsNotNull(author);
            Assert.AreEqual(author.Id, authorId);
        }

        [Test]
        public void GetAll()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IEnumerable<Author> authors = authorDao.GetAll();

            Assert.IsTrue(authors.Any());
        }

        [Test]
        public void GetByHqlQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("FirstName", "%William%");

            Author author = authorDao.GetUniqueByHqlQuery("FROM Author WHERE FirstName LIKE :FirstName", parameters);

            Assert.IsNotNull(author);
            Assert.AreEqual("William", author.FirstName);
        }

        [Test]
        public void GetByQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "FirstName", "William" } };
            Author author = authorDao.GetUniqueByQuery(parameters);

            Assert.IsNotNull(author);
            Assert.AreEqual("William", author.FirstName);
        }

        [Test]
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