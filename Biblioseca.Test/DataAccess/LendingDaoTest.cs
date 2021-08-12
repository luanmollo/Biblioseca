using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Filters;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Biblioseca.Test.DataAccess
{
    [TestClass]
    public class LendingDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
        public void GetAll()
        {
            LendingDao lendingDao = new LendingDao(this.sessionFactory);

            IEnumerable<Lending> lendings = lendingDao.GetAll();

            Assert.IsTrue(lendings.Any());
        }

        [TestMethod]
        public void GetByBook()
        {
            
            LendingDao lendingDao = new LendingDao(this.sessionFactory);

            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                BookId = 1
            };

            IEnumerable<Lending> lendings = lendingDao.GetByFilter(lendingFilterDto);

            Assert.IsTrue(lendings.Any());
        }
    }
}