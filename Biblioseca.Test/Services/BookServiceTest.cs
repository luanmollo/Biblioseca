using System.Collections.Generic;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Services
{
    [TestClass]
    public class BookServiceTest
    {
        private Mock<BookDao> bookDao;
        private Mock<LendingDao> lendingDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
            this.lendingDao = new Mock<LendingDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void IsAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.lendingDao.Setup(dao => dao.GetLendingsByBookId(bookId)).Returns(default(List<Lending>));

            BookService bookService = new BookService(this.bookDao.Object, this.lendingDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsTrue(isAvailable);
        }

        [TestMethod]
        public void IsNotAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(1)).Returns(GetBook());
            this.lendingDao.Setup(dao => dao.GetLendingsByBookId(bookId)).Returns(GetLendings());

            BookService bookService = new BookService(this.bookDao.Object, this.lendingDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsFalse(isAvailable);
        }

        private static IEnumerable<Lending> GetLendings()
        {
            List<Lending> lendings = new List<Lending> { new Lending { Id = 1 } };

            return lendings;
        }

        private static Book GetBook()
        {
            Book book = new Book
            {
                Title = "A title",
                Description = "A description",
                Price = 1.0
            };

            return book;
        }
    }
}