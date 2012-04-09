using System.Collections.Generic;

namespace AopSample.Core.Domain
{
    public class Genre : Entity
    {
        public virtual string Name { get; set; }

        public virtual IList<Show> Shows { get; set; }
    }
}