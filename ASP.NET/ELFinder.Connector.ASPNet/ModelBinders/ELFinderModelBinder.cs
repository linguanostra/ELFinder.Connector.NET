using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using ELFinder.Connector.Commands.Operations.Common.Interfaces;
using ELFinder.Connector.Extensions;

namespace ELFinder.Connector.ASPNet.ModelBinders
{

    /// <summary>
    /// ELFinder custom model binder
    /// </summary>
    public class ELFinderModelBinder : DefaultModelBinder
    {

        #region Overrides        

        /// <summary>
        /// Sets the specified property by using the specified controller context, binding context, and property value.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param><param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param><param name="propertyDescriptor">Describes a property to be set. The descriptor provides information such as the component type, property type, and property value. It also provides methods to get or set the property value.</param><param name="value">The value to set for the property.</param>
        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, object value)
        {

            // Check component type: Multiple targets command
            if (propertyDescriptor.ComponentType.InheritsOrImplements(typeof(IConnectorMultipleTargetsCommand)))
            {

                // Check property descriptor
                if (propertyDescriptor.Name == nameof(IConnectorMultipleTargetsCommand.Targets))
                {

                    // Get targets
                    var targets = controllerContext.HttpContext.Request["targets[]"];

                    // Try to split into list
                    var targetsList = targets?.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);

                    // Assign value
                    value = targetsList?.ToList();                    

                }

            }

            // Check component type: Connector command
            if (propertyDescriptor.ComponentType.InheritsOrImplements(typeof(IConnectorCommand)))
            {

                // Check property type
                if (propertyDescriptor.PropertyType == typeof(bool))
                {

                    // Use Get query string value for property where 1 = True and 0 = False
                    value = controllerContext.HttpContext.Request[propertyDescriptor.Name] == "1";

                }

            }

            // Call base method
            base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);

        }

        #endregion
                 
    }
}