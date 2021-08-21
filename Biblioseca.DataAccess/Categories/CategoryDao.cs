using System.Collections.Generic;
using Biblioseca.DataAccess.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Categories
{
    public class CategoryDao : Dao<Category, CategoryFilterDto>, ICategoryDao
    {
        public CategoryDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public override IEnumerable<Category> GetByFilter(CategoryFilterDto categoryFilterDto)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Category>();

            if (!string.IsNullOrEmpty(categoryFilterDto.Name))
            {
                criteria.Add(Restrictions.Like("Name", categoryFilterDto.Name, MatchMode.Anywhere));
            }

            criteria.Add(Restrictions.Eq("Deleted", false));

            return criteria.List<Category>();
        }

        
    }
}