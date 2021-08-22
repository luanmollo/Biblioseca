using System.Collections.Generic;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Lendings
{
    public class LendingDao : Dao<Lending, LendingFilterDto>, ILendingDao
    {
        public LendingDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public override IEnumerable<Lending> GetByFilter(LendingFilterDto lendingFilterDto)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Lending>();

            if (lendingFilterDto.MemberId != 0)
            {
                criteria.CreateCriteria("Member")
                    .Add(Restrictions.Eq("Id", lendingFilterDto.MemberId));
            }

            if (lendingFilterDto.BookId != 0)
            {
                criteria.CreateCriteria("Book")
                    .Add(Restrictions.Eq("Id", lendingFilterDto.BookId));
            }

            criteria.Add(Restrictions.Eq("ReturnDate", lendingFilterDto.ReturnDate));

            criteria.Add(Restrictions.Eq("Deleted", false));

            return criteria.List<Lending>();
        }

    }
}