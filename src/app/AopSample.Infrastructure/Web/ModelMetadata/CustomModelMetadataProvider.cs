using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Web.ModelMetadata
{
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        private readonly IModelMetadataFilter[] _metadataFilters;

        public CustomModelMetadataProvider(IModelMetadataFilter[] metadataFilters)
        {
            _metadataFilters = metadataFilters;
        }

        protected override System.Web.Mvc.ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            _metadataFilters.Each(m => m.TransformMetadata(metadata, attributes));

            return metadata;
        }
    }
}