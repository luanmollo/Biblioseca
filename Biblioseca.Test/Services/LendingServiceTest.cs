using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Filters;
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

            this.bookDao.Setup(x => x.Get(bookId)).Returns(GetBook());
            this.memberDao.Setup(x => x.Get(memberId)).Returns(GetMember());

            this.lendingDao.Setup(x => x.GetByFilter(It.IsAny<LendingFilterDto>())).Returns(new List<Lending>());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.lendingDao.Setup(x => x.Session).Returns(this.session.Object);

            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Lending lending = this.lendingService.LendABook(bookId, memberId);

            Assert.IsNotNull(lending);
        }

        [TestMethod]
        public void LendABookWhenBookDoesNotExist()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(default(Book));
            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "Libro no existe. ");
        }


        [TestMethod]
        public void LendABookWhenMemberDoesNotExist()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(GetBook());
            this.memberDao.Setup(x => x.Get(memberId)).Returns(default(Member));
            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "Socio no existe. ");
        }


        [TestMethod]
        public void LendABookWhenBooksIsNotAvailable()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(new Book { Title = "new book", Stock = 0 });
            this.memberDao.Setup(x => x.Get(memberId)).Returns(GetMember());
            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "El libro no está disponible. ");
        }
        
        [TestMethod]
        public void LendABookWhenMemberCanNotGetLendings()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(GetBook());
            this.memberDao.Setup(x => x.Get(memberId)).Returns(GetMember());
            this.lendingDao.Setup(x => x.GetByFilter(It.IsAny<LendingFilterDto>())).Returns(GetLendings());

            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "El socio no puede pedir prestado más libros. ");
        }

        [TestMethod]
        public void ReturnABook()
        {
            const int bookId = 1;
            const int memberId = 2;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(GetBook());
            this.memberDao.Setup(x => x.Get(memberId)).Returns(GetMember());
            this.lendingDao.Setup(x => x.GetByFilter(It.IsAny<LendingFilterDto>())).Returns(GetLendings());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.lendingDao.Setup(x => x.Session).Returns(this.session.Object);

            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            bool result = this.lendingService.ReturnABook(bookId, memberId);

            Assert.IsTrue(result);

            
        }

        [TestMethod]
        public void ReturnABookWhenBookDoesNotExist()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(default(Book));
            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "Libro no existe. ");
        }


        [TestMethod]
        public void ReturnABookWhenMemberDoesNotExist()
        {
            const int bookId = 1;
            const int memberId = 1;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(GetBook());
            this.memberDao.Setup(x => x.Get(memberId)).Returns(default(Member));
            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "Socio no existe. ");
        }

        [TestMethod]
        public void ReturnABookWhenLendingDoesNotExistOrWasReturned()
        {
            const int bookId = 1;
            const int memberId = 2;

            this.bookDao.Setup(x => x.Get(bookId)).Returns(GetBook());
            this.memberDao.Setup(x => x.Get(memberId)).Returns(GetMember());
            this.lendingDao.Setup(x => x.GetByFilter(It.IsAny<LendingFilterDto>())).Returns(GetLendings());

            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.LendABook(bookId, memberId),
                "Préstamo no existe o ya fue devuelto. ");
        }

        [TestMethod]
        public void List()
        {
            this.lendingDao.Setup(x => x.GetAll()).Returns(GetLendings());

            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            IEnumerable<Lending> lendings = lendingService.List();

            Assert.IsTrue(lendings.Any());
        }

        [TestMethod]
        public void ListWhenThereAreNotLendings()
        {
            this.lendingDao.Setup(x => x.GetAll()).Returns(new List<Lending>());

            this.lendingService = new LendingService(this.lendingDao.Object, this.bookDao.Object, this.memberDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.lendingService.List(),
                "No hay préstamos para listar. ");
        }

        private static IEnumerable<Lending> GetLendings()
        {
            List<Lending> lendings = new List<Lending> 
            { 
                new Lending
                { 
                    Id = 1,
                    Book = GetBook(),
                    Member = GetMember(),
                    LendDate = DateTime.Now
                } 
            };

            return lendings;
        }

        private static Member GetMember()
        {
            Member member = new Member()
            {
                Id = 2,
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
                Id = 1,
                Title = "A title",
                Description = "A description",
                Price = 1.0,
                Stock = 2
            };

            return book;
        }

    }
}