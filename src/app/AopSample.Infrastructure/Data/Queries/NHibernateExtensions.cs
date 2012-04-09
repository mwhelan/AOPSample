using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace AopSample.Infrastructure.Data.Queries
{
    public static class NHibernateExtensions
    {
        public static IFutureValue<TResult> ToFutureValue<TSource, TResult>(
            this IQueryable<TSource> source,
            Expression<Func<IQueryable<TSource>, TResult>> selector) where TResult : struct
        {
            var provider = (INhQueryProvider)source.Provider;
            var method = ((MethodCallExpression)selector.Body).Method;
            var expression = Expression.Call(null, method, source.Expression);

            return (IFutureValue<TResult>)provider.ExecuteFuture(expression);
        }
    }
}