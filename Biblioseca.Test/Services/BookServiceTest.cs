using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Filters;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Services
{
    [TestClass]
    public class BookServiceTest
    {
        private BookService bookService;
        private Mock<BookDao> bookDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void IsAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook(1));

            this.bookService = new BookService(this.bookDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsTrue(isAvailable);
        }

        [TestMethod]
        public void IsNotAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook(0));

            this.bookService = new BookService(this.bookDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsFalse(isAvailable);
        }

        [TestMethod]
        public void List()
        {
            this.bookDao.Setup(x => x.GetAll()).Returns(GetBooks());

            this.bookService = new BookService(this.bookDao.Object);

            IEnumerable<Book> books = bookService.List();

            Assert.IsTrue(books.Any());
        }

        [TestMethod]
        public void ListWhenThereAreNotBooks()
        {
            this.bookDao.Setup(x => x.GetAll()).Returns(new List<Book>());

            this.bookService = new BookService(this.bookDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.bookService.List(),
                "No hay libros para listar. ");
        }
        
        //arreglar
        [TestMethod]
        public void ListAvailableBooks()
        {

            this.bookDao.Setup(x => x.GetByFilter(new BookFilterDto())).Returns(GetBooks());

            this.bookService = new BookService(this.bookDao.Object);

            IEnumerable<Book> books = this.bookService.ListAvailableBooks();

            Assert.IsTrue(books.Any());
        }

        //arreglar
        [TestMethod]
        public void ListAvailableBooksWhenThereAreNotAvailableBooks()
        {
            this.bookDao.Setup(x => x.GetByFilter(It.IsAny<BookFilterDto>())).Returns(GetBooks());

            this.bookService = new BookService(this.bookDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.bookService.ListAvailableBooks(),
                "No hay libros disponibles para listar. ");
        }


        //arreglar
        [TestMethod]
        public void SearchByTitle()
        {
            const string bookTitle = "book title";

            this.bookDao.Setup(x => x.GetByFilter(It.IsAny<BookFilterDto>())).Returns(GetBooks());

            this.bookService = new BookService(this.bookDao.Object);

            IEnumerable<Book> books = bookService.SearchByTitle(bookTitle);

            Assert.IsTrue(books.Any());

        }

        //arreglar
        [TestMethod]
        public void SearchByTitleWhenBookDoesNotExist()
        {
            const string bookTitle = "book title";

            this.bookDao.Setup(x => x.GetByFilter(It.IsAny<BookFilterDto>())).Returns(new List<Book>());

            this.bookService = new BookService(this.bookDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.bookService.SearchByTitle(bookTitle),
                "Libro no existe. ");

        }

        [TestMethod]
        public void VerifyISBN()
        {
            const int bookId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(GetBook(2));

            this.bookService = new BookService(this.bookDao.Object);

            bool isVerified = bookService.VerifyISBN(bookId);

            Assert.IsTrue(isVerified);

        }

        [TestMethod]
        public void VerifyISBNWhenISBNIsNotCorrect()
        {
            const int bookId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(new Book {ISBN = "123" });

            this.bookService = new BookService(this.bookDao.Object);

            bool isVerified = bookService.VerifyISBN(bookId);

            Assert.IsFalse(isVerified);

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

        private static IEnumerable<Lending> GetLendings()
        {
            List<Lending> lendings = new List<Lending> { new Lending { Id = 1 } };

            return lendings;
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