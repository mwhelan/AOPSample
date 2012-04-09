using System;
using System.Linq;
using System.Linq.Expressions;
using AopSample.Core.Domain;

namespace AopSample.Core.Interfaces.Data
{
    public interface IQuery<T> where T : Entity
    {
        T Single(Expression<Func<T, bool>> expression);
        IQueryable<T> All();
        PagedResult<T> PagedList(IQueryable<T> query, int pageNumber, int pageSize, string sort);
    }
}