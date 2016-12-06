using ELFinder.Connector.Commands.Operations.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections.Generic;
using System.Linq;

namespace ELFinder.Connector.ASPNetCore.ModelBinders
{

    /// <summary>
    /// ELFinder custom model binder
    /// </summary>
    /// 

    public class ELFinderModelBinder : ComplexTypeModelBinder
    {
        public ELFinderModelBinder(Dictionary<ModelMetadata, IModelBinder> propertyBinders) : base(propertyBinders)
        {
        }

        protected override void SetProperty(ModelBindingContext bindingContext, string modelName, ModelMetadata propertyMetadata, ModelBindingResult result)
        {
            if (bindingContext.ModelType.GetInterfaces().Contains(typeof(IConnectorMultipleTargetsCommand)))
            {

                // Check property descriptor
                if (modelName == nameof(IConnectorMultipleTargetsCommand.Targets))
                {
                    // Get targets
                    var targetsList = bindingContext.ActionContext.HttpContext.Request.Query["targets[]"].ToList();
                    result = ModelBindingResult.Success(targetsList);
                }

            }

            if (bindingContext.ModelType.GetInterfaces().Contains(typeof(IConnectorCommand)))
            {
                if (result.Model is bool)
                {
                    // Use Get query string value for property where 1 = True and 0 = False
                    var blnResult = bindingContext.ActionContext.HttpContext.Request.Query[modelName] == "1";
                    result=ModelBindingResult.Success(blnResult);
                }
            }

                base.SetProperty(bindingContext, modelName, propertyMetadata, result);
        }
    }


}