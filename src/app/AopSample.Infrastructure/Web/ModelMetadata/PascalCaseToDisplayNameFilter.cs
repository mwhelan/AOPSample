using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Web.ModelMetadata
{
    public class PascalCaseToDisplayNameFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(metadata.PropertyName) && !attributes.OfType<DisplayNameAttribute>().Any())
            {
                metadata.DisplayName = metadata.PropertyName.ToProperCaseWords();
            }
        }
    }
}