using System;
using System.Collections.Generic;

namespace AopSample.Infrastructure.Web.ModelMetadata
{
    public interface IModelMetadataFilter
    {
        void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes);
    }
}