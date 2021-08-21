using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace Biblioseca.Test.Models
{
    [TestFixture]
    public class MemberTest
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
        }

        [TearDown]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [Test]
        public void CreateMember()
        {
            Member member = new Member
            {
                UserName = "elonmusk",
                FirstName = "Elon",
                LastName = "Musk"
            };

            this.session.Save(member);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(member.Id > 0);

            Member created = this.session.Get<Member>(member.Id);

            Assert.AreEqual(member.Id, created.Id);
        }

    }
}
