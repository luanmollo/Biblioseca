using System.Collections.Generic;
using Biblioseca.DataAccess.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess
{
    public abstract class Dao<T, Filter> : IDao<T>
    {
        private readonly ISessionFactory sessionFactory;

        protected Dao(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public virtual ISession Session
        {
            get { return this.sessionFactory.GetCurrentSession(); }
        }

        public void Save(T entity)
        {
            this.Session
                .SaveOrUpdate(entity);
            this.Session.Flush();
            //si no hacia session.flush los cambios no se guardaban en la bd. tambien deberia hacer .flush en el metodo delete()?
        }

        public void Delete(T entity)
        {
            this.Session
                .Delete(entity);
        }

        public virtual T Get(int id)
        {
            return this.Session
                .Get<T>(id);
        }

        //tuve que hacer el metodo GetAll virtual para poder correr los test de List(). ¿está bien o perjudica en algo?
        public virtual IEnumerable<T> GetAll()
        {
            return this.Session
                .Query<T>();
        }

        public T GetUniqueByHqlQuery(string queryString, IDictionary<string, object> parameters)
        {
            IQuery query = this.Session
                .CreateQuery(queryString);

            foreach (KeyValuePair<string, object> keyValue in parameters)
            {
                query.SetParameter(keyValue.Key, keyValue.Value);
            }

            return query.UniqueResult<T>();
        }

        public T GetUniqueByQuery(IDictionary<string, object> parameters)
        {
            ICriteria criteria = this.Session
                .CreateCriteria(typeof(T));

            foreach (KeyValuePair<string, object> keyValue in parameters)
            {
                criteria.Add(Restrictions.Eq(keyValue.Key, keyValue.Value));
            }

            return criteria.UniqueResult<T>();
        }

        public abstract IEnumerable<T> GetByFilter(Filter filter);

        //quiero poder hacer que cada dao de cada entidad sobreescriba e implemente el metodo GetByFilter
        //public abstract IEnumerable<T> GetByFilter(Filter filter);


    }
}