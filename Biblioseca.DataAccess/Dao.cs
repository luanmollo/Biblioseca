﻿using System.Collections.Generic;
using Biblioseca.DataAccess.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess
{
    public abstract class Dao<T, KFilter> : IDao<T>
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
        }

        public virtual T Get(int id)
        {

            ICriteria criteria = this.Session
               .CreateCriteria(typeof(T));

            criteria.Add(Restrictions.Eq("Id", id));
            criteria.Add(Restrictions.Eq("Deleted", false));

            return criteria.UniqueResult<T>();
        }

        //tuve que hacer el metodo GetAll virtual para poder correr los test de List(). ¿está bien o perjudica en algo?
        public virtual IEnumerable<T> GetAll()
        {

            ICriteria criteria = this.Session
               .CreateCriteria(typeof(T));

            criteria.Add(Restrictions.Eq("Deleted", false));

            return criteria.List<T>();
        }

        //el metodo getuniquebyhqlquery no tiene filtro de deleted
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
                criteria.Add(Restrictions.Eq("Deleted", false));
            }

            return criteria.UniqueResult<T>();
        }

        public abstract IEnumerable<T> GetByFilter(KFilter filter);

        //quiero poder hacer que cada dao de cada entidad sobreescriba e implemente el metodo GetByFilter
        //public abstract IEnumerable<T> GetByFilter(Filter filter);


    }
}