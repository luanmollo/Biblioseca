using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Filters;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NUnit.Framework;

namespace Biblioseca.Test.DataAccess
{
    [TestFixture]
    public class MemberDaoTest
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
            MemberDao memberDao = new MemberDao(this.sessionFactory);

            IEnumerable<Member> members = memberDao.GetAll();

            Assert.IsTrue(members.Any());
        }

        [Test]
        public void GetByFirstName()
        {
            MemberDao memberDao = new MemberDao(this.sessionFactory);

            MemberFilterDto memberFilterDto = new MemberFilterDto
            {
                FirstName = "pepe"
            };

            IEnumerable<Member> members = memberDao.GetByFilter(memberFilterDto);

            Assert.IsTrue(members.Any());
        }

        [Test]
        public void GetByLastName()
        {
            MemberDao memberDao = new MemberDao(this.sessionFactory);

            MemberFilterDto memberFilterDto = new MemberFilterDto
            {
                LastName = "mollo"
            };

            IEnumerable<Member> members = memberDao.GetByFilter(memberFilterDto);

            Assert.IsTrue(members.Any());
        }

        [Test]
        public void GetByUserName()
        {
            MemberDao memberDao = new MemberDao(this.sessionFactory);

            MemberFilterDto memberFilterDto = new MemberFilterDto
            {
                UserName = "luanmollo"
            };

            IEnumerable<Member> members = memberDao.GetByFilter(memberFilterDto);

            Assert.IsTrue(members.Any());
        }
    }
}
