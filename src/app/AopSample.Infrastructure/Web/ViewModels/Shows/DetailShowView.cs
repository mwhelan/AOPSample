using System;
using AutoMapper;
using AopSample.Core.Domain;
using AopSample.Infrastructure.ObjectMapping;

namespace AopSample.Infrastructure.Web.ViewModels.Shows
{
    public class DetailShowView : IHaveCustomMappings
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ContentRating { get; set; }
        public virtual DateTime? FirstAired { get; set; }
        public virtual DateTime? LastAired { get; set; }
        public virtual string Overview { get; set; }
        public virtual int? Runtime { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Show, DetailShowView>();
        }
    }
}