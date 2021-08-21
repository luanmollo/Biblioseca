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
    public class CategoryTest
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
        public void CreateCategory()
        {
            
            Category category = new Category
            {
                Name = "Adventure"
            };

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(category.Id > 0);

            Category created = this.session.Get<Category>(category.Id);

            Assert.AreEqual(category.Id, created.Id);
        }
    }
}
