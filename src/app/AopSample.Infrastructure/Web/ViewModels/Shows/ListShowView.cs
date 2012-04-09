using AopSample.Core.Domain;
using AopSample.Infrastructure.ObjectMapping;

namespace AopSample.Infrastructure.Web.ViewModels.Shows
{
    public class ListShowView : IMapFromDomain<Show>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
    }
}