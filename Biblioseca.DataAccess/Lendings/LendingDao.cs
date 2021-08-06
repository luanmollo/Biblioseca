using System.Collections.Generic;
using Biblioseca.DataAccess.Lendings.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Lendings
{
    public class LendingDao : Dao<Lending>, ILendingDao
    {
        public LendingDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Lending> GetByFilter(LendingFilterDto lendingFilterDto)
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

            
            criteria.Add(Restrictions.Eq("WasReturned", lendingFilterDto.WasReturned));
            

            return criteria.List<Lending>();
        }

        public virtual IEnumerable<Lending> GetLendingsByBookId(int bookId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Lending>();

            criteria.CreateCriteria("Book")
                .Add(Restrictions.Eq("Id", bookId));

            return criteria.List<Lending>();
        }

        public IEnumerable<Lending> GetNotReturnedLendingsByMemberId(int memberId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Lending>();

            criteria.CreateCriteria("Member")
                .Add(Restrictions.Eq("Id", memberId));

            criteria.Add(Restrictions.Eq("WasReturned", false));

            return criteria.List<Lending>();
        }
    }
}