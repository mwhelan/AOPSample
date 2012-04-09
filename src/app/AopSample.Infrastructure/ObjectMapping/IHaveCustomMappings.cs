using AutoMapper;

namespace AopSample.Infrastructure.ObjectMapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}