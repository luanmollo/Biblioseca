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
    public class AuthorTest
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
        public void CreateAuthor()
        {
            Author author = new Author
            {
                FirstName = "Juan Ramon",
                LastName = "Jimenez"
            };

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(author.Id > 0);

            Author created = this.session.Get<Author>(author.Id);

            Assert.AreEqual(author.Id, created.Id);
        }

        [TestMethod]
        public void GetLendings()
        {
            Author author = new Author
            {
                FirstName = "Juan Ramon",
                LastName = "Jimenez"
            };

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Category category = new Category
            {
                Name = "Aventuras"
            };

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            Book book1 = new Book
            {
                AuthorId = 1,
                CategoryId = 1,
                Description = "Un cuento entretenido",
                Price = 1000.0,
                Title = "Platero y yo",
                ISBN = 1234
            };

            this.session.Save(book1);
            this.session.Flush();
            this.session.Clear();

            Member member1 = new Member
            {
                UserName = "elonmusk",
                FirstName = "Elon",
                LastName = "Musk"
            };

            this.session.Save(member1);
            this.session.Flush();
            this.session.Clear();

            Lending lending1 = new Lending
            {
                BookId = 1,
                MemberId = 1,
                LendDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(2)
            };



            member1.Lendings.Add(lending1);

            this.session.SaveOrUpdate(member1);
            this.session.Flush();
            this.session.Clear();

            Member createdMember = this.session.Get<Member>(member1.Id);

            Assert.IsNotNull(createdMember);
            Assert.IsNotNull(createdMember.Lendings);
            Assert.AreEqual(2, createdMember.Lendings.Count);
        }
        }
}
