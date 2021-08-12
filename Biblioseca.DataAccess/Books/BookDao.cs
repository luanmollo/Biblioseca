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

            if (!string.IsNullOrEmpty(bookFilterDto.AuthorFirstName))
            {
                criteria.CreateCriteria("Author")
                    .Add(Restrictions.Like("FirstName", bookFilterDto.AuthorFirstName, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(bookFilterDto.AuthorLastName))
            {
                criteria.CreateCriteria("Author")
                    .Add(Restrictions.Like("LastName", bookFilterDto.AuthorLastName, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(bookFilterDto.CategoryName))
            {
                criteria.CreateCriteria("Category")
                    .Add(Restrictions.Like("Name", bookFilterDto.CategoryName, MatchMode.Anywhere));
            }

            if (bookFilterDto.Price.HasValue)
            {
                criteria.Add(Restrictions.Gt("Price", bookFilterDto.Price));
            }

            if (bookFilterDto.Stock.HasValue)
            {
                criteria.Add(Restrictions.Gt("Stock", bookFilterDto.Stock));
            }

            return criteria.List<Book>();
        }

        
    }
}