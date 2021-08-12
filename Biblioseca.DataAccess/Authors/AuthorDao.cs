using System.Collections.Generic;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Authors
{
    public class AuthorDao : Dao<Author, AuthorFilterDto>, IAuthorDao
    {
        public AuthorDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            
        }

        public override IEnumerable<Author> GetByFilter(AuthorFilterDto authorFilterDto)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Author>();

            if (!string.IsNullOrEmpty(authorFilterDto.FirstName))
            {
                criteria.Add(Restrictions.Like("FirstName", authorFilterDto.FirstName, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(authorFilterDto.LastName))
            {
                criteria.Add(Restrictions.Like("LastName", authorFilterDto.LastName, MatchMode.Anywhere));
            }

            return criteria.List<Author>();
        }

       
    }
}