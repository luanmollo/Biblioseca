﻿using System;
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
                FirstName = "Wanda",
                LastName = "Maximoff"
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

            Book book1 = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890"
            };

            this.session.Save(book1);
            this.session.Flush();
            this.session.Clear();

            Book book2 = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890"
            };

            this.session.Save(book2);
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

            Lending lending1 = new Lending
            {
                Book = book1,
                Member = member,
                LendDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(2)
            };

            Lending lending2 = new Lending
            {
                Book = book2,
                Member = member,
                LendDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(2)
            };

            member.Lendings.Add(lending1);
            member.Lendings.Add(lending2);

            this.session.SaveOrUpdate(member);
            this.session.Flush();
            this.session.Clear();

            Member createdMember = this.session.Get<Member>(member.Id);

            Assert.IsNotNull(createdMember);
            Assert.IsNotNull(createdMember.Lendings);
            Assert.AreEqual(2, createdMember.Lendings.Count);
        }
        }
}
