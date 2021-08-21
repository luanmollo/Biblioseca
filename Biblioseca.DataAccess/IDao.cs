using System.Collections.Generic;

namespace Biblioseca.DataAccess
{
    public interface IDao<T>
    {
        void Save(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}