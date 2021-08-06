using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess.Categories
{
    public class CategoryDao : Dao<Category>, ICategoryDao
    {
        public CategoryDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}