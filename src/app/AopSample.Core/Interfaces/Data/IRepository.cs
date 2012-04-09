using System.Collections.Generic;
using System.Linq;
using AopSample.Core.Domain;

namespace AopSample.Core.Interfaces.Data
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, int> where T : Entity { }
    
    public interface IRepositoryWithTypedId<T, TId> where T : Entity
    {
        T Get(TId id);
        IQueryable<T> GetAll();
        T SaveOrUpdate(T entity);
        IEnumerable<T> SaveOrUpdate(IEnumerable<T> entities);
        void Delete(T entity);
    }
}
