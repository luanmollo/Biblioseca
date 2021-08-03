using System.Collections.Generic;
using Biblioseca.DataAccess.Books.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Books
{
    public class BookDao : Dao<Book>, IBookDao
    {
        public BookDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Book> GetByFilter(BookFilterDto bookFilterDto)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Book>();

            if (!string.IsNullOrEmpty(bookFilterDto.Title))
            {
                criteria.Add(Restrictions.Like("Title", bookFilterDto.Title, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(bookFilterDto.AuthorFirstName))
            {
                criteria.CreateCriteria("Author")
                    .Add(Restrictions.Like("FirstName", bookFilterDto.AuthorFirstName, MatchMode.Anywhere));
            }

            return criteria.List<Book>();
        }
    }
}