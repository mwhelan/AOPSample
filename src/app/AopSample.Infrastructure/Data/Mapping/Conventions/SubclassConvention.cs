using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace AopSample.Infrastructure.Data.Mapping.Conventions
{
    public class SubclassConvention : ISubclassConvention
    {
        public void Apply(ISubclassInstance instance)
        {
            //string entityNameWithSpaces = instance.EntityType.Name.ToProperCaseWords();
            string entityNameWithSpaces = instance.EntityType.Name.InflectTo().Titleized;
            instance.DiscriminatorValue(entityNameWithSpaces);
        }
    }
}