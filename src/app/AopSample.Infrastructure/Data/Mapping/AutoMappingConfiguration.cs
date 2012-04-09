using System;
using AopSample.Core.Domain;
using FluentNHibernate.Automapping;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Data.Mapping
{
    public class AutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.CanBeCastTo<Entity>() && base.ShouldMap(type);
        }

        public override bool IsComponent(Type type)
        {
            return false;
            //return type == typeof(CommitteePreference)
            //    || type == typeof(ContactDetails)
            //        || type == typeof(PhoneNumber)
            //            || type == typeof(Address);
        }

        public override bool IsDiscriminated(Type type)
        {
            return true;
        }
    }
}