using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Authors.Filters;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Books.Filters;
using Biblioseca.DataAccess.Categories;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.DataAccess.Lendings.Filters;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Service;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Biblioseca.ConsoleApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {

            ISessionFactory sessionFactory = new Configuration()
                .Configure()
                .BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            BookDao bookDao = new BookDao(sessionFactory);
            AuthorDao authorDao = new AuthorDao(sessionFactory);
            CategoryDao categoryDao = new CategoryDao(sessionFactory);
            LendingDao lendingDao = new LendingDao(sessionFactory);
            MemberDao memberDao = new MemberDao(sessionFactory);

            BookService bookService = new BookService(bookDao, lendingDao);
            AuthorService authorService = new AuthorService(authorDao);
            CategoryService categoryService = new CategoryService(categoryDao);
            LendingService lendingService = new LendingService(lendingDao, bookDao, memberDao);
            MemberService memberService = new MemberService(memberDao);





            BookFilterDto bookFilterDto = new BookFilterDto
            {
                Title = "El amor en tiempos del cólera",
                AuthorLastName = "Rogers",
                CategoryName = "Aventuras",
                Price = 60.0
            };

            IEnumerable<Book> booksByFilter = bookDao.GetByFilter(bookFilterDto);
            
            foreach (Book book in booksByFilter)
            {
                Console.WriteLine($"Book: {book.Title}, Id: {book.Id}, Author: {book.Author.FirstName} {book.Author.LastName}, Price: {book.Price}");
            }

            Console.WriteLine();

            AuthorFilterDto authorFilterDto = new AuthorFilterDto()
            {
                FirstName = "Julio",
            };

            IEnumerable<Author> authorsByFilter = authorDao.GetByFilter(authorFilterDto);

            foreach (Author author in authorsByFilter)
            {
                Console.WriteLine($"Author: {author.FirstName} {author.LastName}");
            }

            Console.WriteLine();

            IEnumerable<Book> booksByTitle = bookService.SearchByTitle("amor");
            foreach (Book book in booksByTitle)
            {
                Console.WriteLine($"Book: {book.Title}");
            }

            Console.WriteLine();

            authorService.List();

            Console.WriteLine();

            bookService.List();

            Console.WriteLine();

            categoryService.List();

            Console.WriteLine();

            //lendingService.List();

            Console.WriteLine();

            memberService.List();

            Console.WriteLine();

            Console.WriteLine(bookService.IsAvailable(1));

            Console.WriteLine();

            //lendingService.LendABook(2, 2);

            //lendingService.ReturnABook(2, 2);

            memberService.SearchByLastName("mollo");

            Console.WriteLine();

            bookService.SearchByTitle("amor");







            

        }
    }
}
