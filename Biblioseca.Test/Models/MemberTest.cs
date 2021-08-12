using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;

namespace Biblioseca.Test.Models
{
    [TestClass]
    public class MemberTest
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
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
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
