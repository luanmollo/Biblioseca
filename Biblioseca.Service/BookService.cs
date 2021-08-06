using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Books.Filters;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class BookService
    {
        private readonly BookDao bookDao;
        private readonly LendingDao lendingDao;

        public BookService(BookDao bookDao, LendingDao lendingDao)
        {
            this.bookDao = bookDao;
            this.lendingDao = lendingDao;
        }

        public bool IsAvailable(int bookId)
        {
            Ensure.IsTrue(bookId > 0, "Book.Id debe ser mayor que 0.");

            Book book = this.bookDao.Get(bookId);
            Ensure.NotNull(book, "El libro no existe. ");
            Ensure.IsTrue(book.Stock > 0, "No hay stock disponible del libro. ");

            return book.Stock > 0;
        }

        public IEnumerable<Book> List()
        {
            IEnumerable<Book> books = this.bookDao.GetAll();
            Ensure.IsTrue(books.Any(), "No hay libros para listar");

            Console.WriteLine("Lista de libros:");
            foreach (Book book in books)
            {
                Console.WriteLine($"\t{book.Title}, por {book.Author.FirstName} {book.Author.LastName}");
            }

            return books;
        }

        public IEnumerable<Book> SearchByTitle(string bookTitle)
        {
            BookFilterDto bookFilterDtoByTitle = new BookFilterDto
            {
                Title = bookTitle
            };

            IEnumerable<Book> books = this.bookDao.GetByFilter(bookFilterDtoByTitle);
            Ensure.IsTrue(books.Count() > 0, "Libro no existe. ");

            Console.WriteLine("Libros encontrados:");
            foreach (Book book in books)
            {
                Console.WriteLine($"\tLibro: {book.Title}. Autor: {book.Author.FirstName} {book.Author.LastName}");
            }

            return books;
        }

    }
}