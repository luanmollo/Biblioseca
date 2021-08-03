using System.Collections.Generic;
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

        public virtual IEnumerable<Lending> GetLendingsByBookId(int bookId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Lending>();

            criteria.CreateCriteria("Book")
                .Add(Restrictions.Eq("Id", bookId));

            return criteria.List<Lending>();
        }
    }
}