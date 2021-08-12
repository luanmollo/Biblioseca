using System.Collections.Generic;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Members
{
    public class MemberDao : Dao<Member, MemberFilterDto>, IMemberDao
    {
        public MemberDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public override IEnumerable<Member> GetByFilter(MemberFilterDto memberFilterDto)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Member>();

            if (!string.IsNullOrEmpty(memberFilterDto.FirstName))
            {
                criteria.Add(Restrictions.Like("FirstName", memberFilterDto.FirstName, MatchMode.Anywhere));
            }
            
            if (!string.IsNullOrEmpty(memberFilterDto.LastName))
            {
                criteria.Add(Restrictions.Like("LastName", memberFilterDto.LastName, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(memberFilterDto.UserName))
            {
                criteria.Add(Restrictions.Like("UserName", memberFilterDto.UserName, MatchMode.Anywhere));
            }

           




            return criteria.List<Member>();
        }
    }
}