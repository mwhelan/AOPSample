using System;
using AopSample.Core.Commands.Shows.CreateShow;
using AopSample.Infrastructure.ObjectMapping;

namespace AopSample.Infrastructure.Web.ViewModels.Shows
{
    public class CreateShowForm : IMapToCommand<CreateShowCommand>
    {
        public virtual string Name { get; set; }
        public virtual string ContentRating { get; set; }
        public virtual DateTime? FirstAired { get; set; }
        public virtual DateTime? LastAired { get; set; }
        public virtual string Overview { get; set; }
        public virtual int? Runtime { get; set; }
    }
}