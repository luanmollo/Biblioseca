using System;
using System.Collections.Generic;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Services
{
    [TestClass]
    public class LendingServiceTest
    {
        private LendingService lendingService;
        private Mock<LendingDao> lendingDao;
        private Mock<BookDao> bookDao;
        private Mock<MemberDao> memberDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.lendingDao = new Mock<LendingDao>(this.sessionFactory.Object);
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
            this.memberDao = new Mock<MemberDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void LendABook()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.memberDao.Setup(dao => dao.Get(memberId)).Returns(GetMember());
            this.lendingDao.Setup(dao => dao.GetLendingsByBookId(bookId)).Returns(new List<Lending>());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.lendingDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Lending lending = this.lendingService.LendABook(bookId, memberId);

            Assert.IsNotNull(lending);
        }

        [TestMethod]
        public void LendABookWhenBookDoesNotExist()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);
            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "Libro no existe. ");
        }


        [TestMethod]
        public void LendABookWhenMemberDoesNotExist()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.memberDao.Setup(dao => dao.Get(memberId)).Returns(default(Member));
            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);
            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "Socio no existe. ");
        }

        [TestMethod]
        public void LendABookWhenBooksWasLended()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.memberDao.Setup(dao => dao.Get(memberId)).Returns(GetMember());
            this.lendingDao.Setup(dao => dao.GetLendingsByBookId(bookId)).Returns(GetLendings());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.lendingDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "El libro ya fue prestado. ");
        }

        private static IEnumerable<Lending> GetLendings()
        {
            List<Lending> lendings = new List<Lending> { new Lending{ Id = 1 } };

            return lendings;
        }

        private static Member GetMember()
        {
            Member member = new Member()
            {
                FirstName = "John",
                LastName = "Smith",
                UserName = "johnsmith"
            };

            return member;
        }

        private static Book GetBook()
        {
            Book book = new Book
            {
                Title = "A title",
                Description = "A description",
                Price = 1.0
            };

            return book;
        }
    }
}