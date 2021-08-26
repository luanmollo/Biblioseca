using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Filters;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Biblioseca.Test.Services
{
    [TestFixture]
    public class BookServiceTest
    {
        private BookService bookService;
        private Mock<BookDao> bookDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [SetUp]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
        }

        [Test]
        public void Get()
        {
            int bookId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(new Book { Id = 1 });

            BookService bookService = new BookService(this.bookDao.Object);

            Book book = bookService.Get(bookId);

            Assert.NotNull(book);
        }

        [Test]
        public void IsAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook(1));

            this.bookService = new BookService(this.bookDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsTrue(isAvailable);
        }

        [Test]
        public void IsNotAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook(0));

            this.bookService = new BookService(this.bookDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsFalse(isAvailable);
        }

        [Test]
        public void List()
        {
            this.bookDao.Setup(x => x.GetAll()).Returns(GetBooks());

            this.bookService = new BookService(this.bookDao.Object);

            IEnumerable<Book> books = bookService.List();

            Assert.IsTrue(books.Any());
        }

        [Test]
        public void ListWhenThereAreNotBooks()
        {
            this.bookDao.Setup(x => x.GetAll()).Returns(new List<Book>());

            this.bookService = new BookService(this.bookDao.Object);

            Assert.Throws<BusinessRuleException>(() => this.bookService.List(),
                "No hay libros para listar. ");
        }

        [Test]
        public void ListAvailableBooks()
        {
            this.bookDao.Setup(x => x.GetByFilter(It.IsAny<BookFilterDto>())).Returns(GetBooks());

            BookService bookService = new BookService(this.bookDao.Object);

            IEnumerable<Book> books = bookService.ListAvailableBooks();

            Assert.IsTrue(books.Any());
        }

        [Test]
        public void ListAvailableBooksWhenThereAreNotAvailableBooks()
        {
            this.bookDao.Setup(x => x.GetByFilter(It.IsAny<BookFilterDto>())).Returns(new List<Book>());

            this.bookService = new BookService(this.bookDao.Object);

            Assert.Throws<BusinessRuleException>(() => this.bookService.ListAvailableBooks(),
                "No hay libros disponibles para listar. ");
        }


        [Test]
        public void VerifyISBN()
        {
            const int bookId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(GetBook(2));

            this.bookService = new BookService(this.bookDao.Object);

            bool isVerified = bookService.VerifyISBN(bookId);

            Assert.IsTrue(isVerified);

        }

        [Test]
        public void VerifyISBNWhenISBNIsNotCorrect()
        {
            const int bookId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(new Book {ISBN = "123" });

            this.bookService = new BookService(this.bookDao.Object);

            bool isVerified = bookService.VerifyISBN(bookId);

            Assert.IsFalse(isVerified);

        }

        [Test]
        public void ThereAreBooks()
        {
            this.bookDao.Setup(x => x.GetAll()).Returns(new List<Book>());

            this.bookService = new BookService(this.bookDao.Object);

            BookError bookError = this.bookService.ThereAreBooks();

            Assert.IsTrue(bookError.HasError);
        }

        private static IEnumerable<Book> GetBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book
                {
                    Title = "book title",
                    Stock = 4
                },

                new Book
                {
                    Title = "another book title",
                    Stock = 2
                }
            };

            return books;
        }


        private static Book GetBook(int stock)
        {
            Book book = new Book
            {
                Title = "A title",
                Description = "A description",
                Price = 1.0,
                Stock = stock,
                Author = new Author
                {
                    FirstName = "Pepe",
                    LastName = "Lopez"
                },
                ISBN = "1234567891234"
                
            };

            return book;
        }
    }
}