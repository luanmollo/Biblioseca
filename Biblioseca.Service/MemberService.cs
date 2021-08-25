using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Members;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.DataAccess.Lendings;

namespace Biblioseca.Service
{
    public class MemberService
    {
        private readonly MemberDao memberDao;
        public MemberService(MemberDao memberDao)
        {
            this.memberDao = memberDao;
        }

        public Member Get(int memberId)
        {
            return this.memberDao.Get(memberId);
        }

        public IEnumerable<Member> List()
        {
            IEnumerable<Member> members = this.memberDao.GetAll();
            Ensure.IsTrue(members.Any(), "No hay socios para listar. ");

            return members;
        }

        public MemberError ThereAreMembers()
        {
            IEnumerable<Member> members = this.memberDao.GetAll();

            MemberError memberError = new MemberError
            {
                HasError = !members.Any()
            };

            return memberError;
        }

    }
}
