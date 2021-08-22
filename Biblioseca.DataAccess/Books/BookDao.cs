using System.Collections.Generic;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Books
{
    public class BookDao : Dao<Book, BookFilterDto>, IBookDao
    {
        public BookDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            
        }

        public override IEnumerable<Book> GetByFilter(BookFilterDto bookFilterDto)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Book>();

            if (!string.IsNullOrEmpty(bookFilterDto.Title))
            {
                criteria.Add(Restrictions.Like("Title", bookFilterDto.Title, MatchMode.Anywhere));
            }

            if (bookFilterDto.Stock.HasValue)
            {
                criteria.Add(Restrictions.Gt("Stock", bookFilterDto.Stock));
            }

            criteria.Add(Restrictions.Eq("Deleted", false));

            return criteria.List<Book>();
        }

        
    }
}