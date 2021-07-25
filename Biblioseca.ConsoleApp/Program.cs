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
    public class Program
    {
        private static void Main(string[] args)
        {

            ISessionFactory sessionFactory = new Configuration()
                .Configure()
                .BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();


            Book book = new Book()
            {
                Title = "El amor en tiempos del cólera",
                AuthorId = 1,
                Description = "Un libro histórico",
                CategoryId = 1,
                ISBN = 1234,
                Price = 123.4
            };

            session.Save(book);


        }
    }
}
