using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Categories;
using Biblioseca.DataAccess.Filters;
using Biblioseca.DataAccess.Lendings;
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

            BookService bookService = new BookService(bookDao);
            AuthorService authorService = new AuthorService(authorDao);
            CategoryService categoryService = new CategoryService(categoryDao);
            LendingService lendingService = new LendingService(lendingDao, bookDao, memberDao);
            MemberService memberService = new MemberService(memberDao);


            



        }
    }
}
