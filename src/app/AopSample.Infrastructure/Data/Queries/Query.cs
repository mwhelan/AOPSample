using System;
using System.Linq;
using System.Linq.Expressions;
using AopSample.Core.Domain;
using AopSample.Core.Interfaces.Data;
using NHibernate;
using NHibernate.Linq;

namespace AopSample.Infrastructure.Data.Queries
{
    public class Query<T> : IQuery<T> where T : Entity
    {
        readonly ISession _session;

        public Query(ISession session)
        {
            _session = session;
        }

        public virtual T Single(Expression<Func<T, bool>> expression)
        {
            return Transact(() => All().Where(expression).SingleOrDefault());
        }

        public virtual IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public PagedResult<T> PagedList(IQueryable<T> query, int pageNumber, int pageSize, string sort = null)
        {
            return Transact(() =>
            {
                if (sort != null)
                {
                    query = query.NhOrderBy(sort);
                }

                return new PagedResult<T>()
                {
                    TotalItems = query.ToFutureValue(x => x.Count()).Value,
                    PageOfResults = query.Skip(pageNumber * pageSize).Take(pageSize).ToFuture()
                };
            });
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
