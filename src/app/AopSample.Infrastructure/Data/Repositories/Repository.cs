using System;
using System.Collections.Generic;
using System.Linq;
using AopSample.Core.Domain;
using AopSample.Core.Interfaces.Data;
using NHibernate;
using NHibernate.Linq;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Data.Repositories
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T> where T : Entity
    {
        public Repository(ISession session) : base(session) { }
    }

    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : Entity
    {
        readonly ISession _session;

        public RepositoryWithTypedId(ISession session)
        {
            _session = session;
        }

        public virtual T Get(TId id)
        {
            return Transact(() => _session.Get<T>(id));
        }

        public virtual IQueryable<T> GetAll()
        {
            return Transact(() => _session.Query<T>());
        }

        public virtual T SaveOrUpdate(T entity)
        {
            Transact(() => _session.SaveOrUpdate(entity));
            return entity;
        }

        public virtual IEnumerable<T> SaveOrUpdate(IEnumerable<T> entities)
        {
            Transact(() => entities.Each(entity => _session.SaveOrUpdate(entity)));
            return entities;
        }

        public virtual void Delete(T entity)
        {
            _session.Delete(entity);
            _session.Flush();
        }

        protected virtual TResult Transact<TResult>(Func<TResult> func)
        {
            if (!_session.Transaction.IsActive)
            {
                // Wrap in transaction
                TResult result;
                using (var tx = _session.BeginTransaction())
                {
                    result = func.Invoke();
                    tx.Commit();
                }

                return result;
            }

            // Don't wrap
            return func.Invoke();
        }

        protected virtual void Transact(Action action)
        {
            Transact<bool>(() =>
            {
                action.Invoke();
                return false;
            });
        }

    }

}
