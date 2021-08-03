using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess.Members
{
    public class MemberDao : Dao<Member>, IMemberDao
    {
        public MemberDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}