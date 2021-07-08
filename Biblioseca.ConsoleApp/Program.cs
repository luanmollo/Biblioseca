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
            book.Title = "El amor en tiempos del cólera";
            book.AuthorId = 2;

            session.Save(book);
            //session.Save(author);

            Member member = new Member();
            member.FirstName = "Luan";
            member.LastName = "Mollo";
            member.UserName = "luanmollo";

            session.Save(member);
            



          
        }
    }
}
