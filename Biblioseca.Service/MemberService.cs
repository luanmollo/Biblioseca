using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Members;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class MemberService
    {
        private readonly MemberDao memberDao;

        public MemberService(MemberDao memberDao)
        {
            this.memberDao = memberDao;
        }

        public IEnumerable<Member> List()
        {
            IEnumerable<Member> members = this.memberDao.GetAll();
            Ensure.IsTrue(members.Any(), "No hay socios para listar. ");

            return members;

        }

    }
}
