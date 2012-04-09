using System;
using AopSample.Core.Commands.Shows.EditShow;
using AopSample.Core.Domain;
using AopSample.Infrastructure.ObjectMapping;

namespace AopSample.Infrastructure.Web.ViewModels.Shows
{
    public class EditShowForm : IMapFromDomain<Show>, IMapToCommand<EditShowCommand>
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ContentRating { get; set; }
        public virtual DateTime? FirstAired { get; set; }
        public virtual DateTime? LastAired { get; set; }
        public virtual string Overview { get; set; }
        public virtual int? Runtime { get; set; }
    }
}