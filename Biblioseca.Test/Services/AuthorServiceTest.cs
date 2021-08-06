using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Services
{
    [TestClass]
    public class AuthorServiceTest
    {
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
            this.authorDao.Setup(dao => dao.GetAll()).Returns(default(List<Author>));

            AuthorService authorService = new AuthorService(this.authorDao.Object);

            IEnumerable<Author> authors = authorService.List();

            Assert.AreEqual(authors, authorDao);

        }

    }
}
