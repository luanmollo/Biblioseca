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
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Biblioseca.Test.Services
{
    [TestFixture]
    public class AuthorServiceTest
    {
        private AuthorService authorService;
        private Mock<AuthorDao> authorDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [SetUp]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.authorDao = new Mock<AuthorDao>(this.sessionFactory.Object);
        }

        [Test]
        public void Get()
        {
            int authorId = 1;

            this.authorDao.Setup(x => x.Get(authorId)).Returns(new Author { Id = 1 });

            AuthorService authorService = new AuthorService(this.authorDao.Object);

            Author author = authorService.Get(authorId);

            Assert.NotNull(author);
        }

        [Test]
        public void List()
        {

            this.authorDao.Setup(x => x.GetAll()).Returns(GetAuthors());

            this.authorService = new AuthorService(this.authorDao.Object);

            IEnumerable<Author> authors = authorService.List();

            Assert.IsTrue(authors.Any());

        }

        [Test]
        public void ListWhenThereAreNotAuthors()
        {
            this.authorDao.Setup(x => x.GetAll()).Returns(new List<Author>());

            this.authorService = new AuthorService(this.authorDao.Object);

            Assert.Throws<BusinessRuleException>(() => this.authorService.List(),
                "No hay autores para listar. ");
        }

        [Test]
        public void ThereAreAuthors()
        {
            this.authorDao.Setup(x => x.GetAll()).Returns(new List<Author>());

            this.authorService = new AuthorService(this.authorDao.Object);

            AuthorError authorError = this.authorService.ThereAreAuthors();

            Assert.IsTrue(authorError.HasError);
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
