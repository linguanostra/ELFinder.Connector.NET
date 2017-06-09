using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace ELFinder.Connector.ASPNetCore.ModelBinders
{
    public class ELFinderModelBinderProvider: IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.IsComplexType && !context.Metadata.IsCollectionType)
            {
                var propertyBinders = new Dictionary<ModelMetadata, IModelBinder>(); ;
                foreach (var property in context.Metadata.Properties)
                {
                    propertyBinders.Add(property, context.CreateBinder(property));
                }

                return new ELFinderModelBinder(propertyBinders);
            }

            return null;
        }
    }
}
