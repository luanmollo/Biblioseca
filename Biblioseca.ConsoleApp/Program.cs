using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;

namespace Biblioseca.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            ISessionFactory sessionFactory = new Configuration()
                .Configure()
                .BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();
            
            //Author author = new Author();
           // author.FirstName = "William";
           // author.LastName = "Shakespeare";

            Book book = new Book();
            book.Title = "Padre Rico Padre Pobre";
            book.AuthorId = 1;

            session.Save(book);
            //session.Save(author);

            
            



          
        }
    }
}
