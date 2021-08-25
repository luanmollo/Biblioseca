using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Filters;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class BookService
    {
        private readonly BookDao bookDao;

        public BookService(BookDao bookDao)
        {
            this.bookDao = bookDao;
        }

        public Book Get(int bookId)
        {
            return this.bookDao.Get(bookId);
        }

        public bool IsAvailable(int bookId)
        {
            Ensure.IsTrue(bookId > 0, "Book.Id debe ser mayor que 0.");

            Book book = this.bookDao.Get(bookId);
            Ensure.NotNull(book, "El libro no existe. ");

            return book.Stock > 0;
        }

        public IEnumerable<Book> List()
        {
            IEnumerable<Book> books = this.bookDao.GetAll();
            Ensure.IsTrue(books.Any(), "No hay libros para listar");

            return books;
        }

        public IEnumerable<Book> ListAvailableBooks()
        {
            BookFilterDto bookFilterDto = new BookFilterDto
            {
                Stock = 0
            };

            IEnumerable<Book> books = this.bookDao.GetByFilter(bookFilterDto);
            Ensure.IsTrue(books.Any(), "No hay libros disponibles para listar");

            return books;

        }

        public bool VerifyISBN(int bookId)
        {
            Book book = bookDao.Get(bookId);
            if(book.ISBN.Length == 13)
            {
                book.ISBNVerified = true;
            }
            else
            {
                book.ISBNVerified = false;
            }

            return book.ISBNVerified;
        }

        public BookError ThereAreBooks()
        {
            IEnumerable<Book> books = this.bookDao.GetAll();

            BookError bookError = new BookError
            {
                HasError = !books.Any()
            };

            return bookError;
        }

    }
}