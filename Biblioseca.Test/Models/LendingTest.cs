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
    public class LendingTest
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
        public void CreateLending()
        {
            Author author = new Author
            {
                FirstName = "Wanda",
                LastName = "Maximoff"
            };

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Category category = new Category
            {
                Name = "Adventure"
            };

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            Book book = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890",
                Stock = 1
            };

            this.session.Save(book);
            this.session.Flush();
            this.session.Clear();

            Member member = new Member
            {
                UserName = "elonmusk",
                FirstName = "Elon",
                LastName = "Musk"
            };

            this.session.Save(member);
            this.session.Flush();
            this.session.Clear();

            Lending lending = new Lending
            {
                Book = book,
                Member = member,
                LendDate = DateTime.Now,
            };

            this.session.Save(lending);

            Assert.IsTrue(author.Id > 0);

            Lending created = this.session.Get<Lending>(lending.Id);

            Assert.AreEqual(lending.Id, created.Id);
        }
        }
}
