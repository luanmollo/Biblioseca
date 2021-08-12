using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Services
{
    [TestClass]
    public class AuthorServiceTest
    {
        private AuthorService authorService;
        private Mock<AuthorDao> authorDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.authorDao = new Mock<AuthorDao>(this.sessionFactory.Object);
        }


        [TestMethod]
        public void List()
        {

            this.authorDao.Setup(x => x.GetAll()).Returns(GetAuthors());

            this.authorService = new AuthorService(this.authorDao.Object);

            IEnumerable<Author> authors = authorService.List();

            Assert.IsTrue(authors.Any());

        }

        [TestMethod]
        public void ListWhenThereAreNotAuthors()
        {
            this.authorDao.Setup(x => x.GetAll()).Returns(new List<Author>());

            this.authorService = new AuthorService(this.authorDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.authorService.List(),
                "No hay autores para listar. ");
        }

        private static IEnumerable<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>
            {
                new Author
                {
                    Id = 1,
                    FirstName = "Pepe",
                    LastName = "Lopez"
                },

                new Author
                {
                    Id = 2,
                    FirstName = "Juan",
                    LastName = "Perez"
                },
            };
            return authors;
        }

    }
}
