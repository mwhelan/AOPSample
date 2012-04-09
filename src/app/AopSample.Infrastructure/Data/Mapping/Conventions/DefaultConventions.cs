using System.Collections.Generic;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace AopSample.Infrastructure.Data.Mapping.Conventions
{
    public class DefaultConventions : IIdConvention, IHasManyToManyConvention, IClassConvention, IHasManyConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "Id");
            instance.GeneratedBy.HiLo("Hilo", instance.EntityType.Name, "1000");
        }

        public void Apply(IClassInstance instance)
        {
            instance.Table(instance.EntityType.Name.InflectTo().Pluralized);
            //instance.Table(Inflector.Net.Inflector.Pluralize(instance.EntityType.Name));
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.LazyLoad();
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            var names = new List<string>
                            {
                                instance.EntityType.Name,
                                instance.ChildType.Name
                            };

            names.Sort();

            instance.Table(string.Format("{0}To{1}", names[0], names[1]));
            instance.LazyLoad();
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Cascade.SaveUpdate();
        }
    }
}