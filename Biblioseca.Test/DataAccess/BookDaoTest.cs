using System.Collections.Generic;
using System.Linq;
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
    public class BookDaoTest
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
            BookDao bookDao = new BookDao(this.sessionFactory);

            IEnumerable<Book> books = bookDao.GetAll();

            Assert.IsTrue(books.Any());
        }

        [Test]
        public void GetByTitle()
        {
            BookDao bookDao = new BookDao(this.sessionFactory);

            BookFilterDto bookFilterDto = new BookFilterDto
            {
                Title = "Romeo y Julieta",
            };

            IEnumerable<Book> books = bookDao.GetByFilter(bookFilterDto);

            Assert.IsTrue(books.Any());
        }

        [Test]
        public void GetAvailableBooks()
        {
            BookDao bookDao = new BookDao(this.sessionFactory);

            BookFilterDto bookFilterDto = new BookFilterDto
            {
                Stock = 0
            };

            IEnumerable<Book> books = bookDao.GetByFilter(bookFilterDto);

            Assert.IsTrue(books.Any());
        }
    }
}