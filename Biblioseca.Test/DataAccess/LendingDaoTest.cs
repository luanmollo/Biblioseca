using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Filters;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NUnit.Framework;

namespace Biblioseca.Test.DataAccess
{
    [TestFixture]
    public class LendingDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [SetUp]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
        }

        [TearDown]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [Test]
        public void GetAll()
        {
            LendingDao lendingDao = new LendingDao(this.sessionFactory);

            IEnumerable<Lending> lendings = lendingDao.GetAll();

            Assert.IsTrue(lendings.Any());
        }

        [Test]
        public void GetByBook()
        {
            
            LendingDao lendingDao = new LendingDao(this.sessionFactory);

            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                BookId = 895
            };

            IEnumerable<Lending> lendings = lendingDao.GetByFilter(lendingFilterDto);

            Assert.IsTrue(lendings.Any());
        }

        [Test]
        public void GetByMember()
        {
            LendingDao lendingDao = new LendingDao(this.sessionFactory);

            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                MemberId = 4134
            };

            IEnumerable<Lending> lendings = lendingDao.GetByFilter(lendingFilterDto);

            Assert.IsTrue(lendings.Any());
        }

        [Test]
        public void GetActualLendings()
        {
            LendingDao lendingDao = new LendingDao(this.sessionFactory);

            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                Returned = false
            };

            IEnumerable<Lending> lendings = lendingDao.GetByFilter(lendingFilterDto);

            Assert.IsTrue(lendings.Any());
        }
    }
}