using System.Collections.Generic;

namespace AopSample.Core.Interfaces.Data
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> PageOfResults { get; set; }
    }
}