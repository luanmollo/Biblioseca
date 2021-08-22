using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Biblioseca.Test.Services
{
    [TestFixture]
    public class MemberServiceTest
    {
        private MemberService memberService;
        private Mock<MemberDao> memberDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [SetUp]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.memberDao = new Mock<MemberDao>(this.sessionFactory.Object);
        }

        [Test]
        public void Get()
        {
            int memberId = 1;

            this.memberDao.Setup(x => x.Get(memberId)).Returns(new Member { Id = 1 });

            MemberService memberService = new MemberService(this.memberDao.Object);

            Member member = memberService.Get(memberId);

            Assert.NotNull(member);
        }

        [Test]
        public void List()
        {
            this.memberDao.Setup(x => x.GetAll()).Returns(GetMembers());
            this.memberService = new MemberService(this.memberDao.Object);

            IEnumerable<Member> members = memberService.List();
            Assert.IsTrue(members.Any());

        }

       
        [Test]
        public void ListWhenThereAreNotMembers()
        {
            this.memberDao.Setup(x => x.GetAll()).Returns(new List<Member>());
            this.memberService = new MemberService(this.memberDao.Object);

            Assert.Throws<BusinessRuleException>(() => this.memberService.List(),
                "No hay socios para listar. ");
        }

        private static IEnumerable<Member> GetMembers()
        {
            List<Member> members = new List<Member>
            {
                new Member
                {
                    Id = 1
                }
            };

            return members;
        }

    }
}
