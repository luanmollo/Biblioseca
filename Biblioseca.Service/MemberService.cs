using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Members;
using Biblioseca.DataAccess.Members.Filters;
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

            Console.WriteLine("Lista de socios:");
            foreach(Member member in members)
            {
                Console.WriteLine($"\t{member.FirstName} {member.LastName}. UserName: {member.UserName}");
            }

            return members;

        }

        public IEnumerable<Member> SearchByLastName(string memberLastName)
        {
            MemberFilterDto memberFilterDtoByTitle = new MemberFilterDto
            {
                LastName = memberLastName
            };

            IEnumerable<Member> members = this.memberDao.GetByFilter(memberFilterDtoByTitle);
            Ensure.IsTrue(members.Count() > 0, "Socio no existe. ");

            Console.WriteLine("Socios encontrados:");
            foreach (Member member in members)
            {
                Console.WriteLine($"\tSocio: {member.FirstName} {member.LastName}");
            }

            return members;
        }

    }
}
