using System;
using System.Reflection;
using ELFinder.Connector.Commands.Results.Common;
using ELFinder.Connector.Drivers.FileSystem.Models.Files;
using ELFinder.Connector.Extensions;
using ELFinder.Connector.Web.Serialization.Values;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ELFinder.Connector.Web.Serialization
{

    /// <summary>
    /// ELFinder response contract resolver
    /// </summary>
    public class ELFinderResponseContractResolver : DefaultContractResolver
    {

        #region Overrides
        
        /// <summary>
        /// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty"/> for the given <see cref="T:System.Reflection.MemberInfo"/>.
        /// </summary>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization"/>.</param><param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty"/> for.</param>
        /// <returns>
        /// A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty"/> for the given <see cref="T:System.Reflection.MemberInfo"/>.
        /// </returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {


            // Get property from base method
            var property = base.CreateProperty(member, memberSerialization);

            // Check declaring type
            if (property.DeclaringType.InheritsOrImplements(typeof (CommandResult)))
            {
                
                // Command result

                // Check property name
                if (property.PropertyName.Equals(nameof(CommandResult.Error), StringComparison.InvariantCultureIgnoreCase)
                    || property.PropertyName.Equals(nameof(CommandResult.ErrorData), StringComparison.CurrentCultureIgnoreCase))
                {

                    // Adjust serialization
                    property.ShouldSerialize =
                        instance =>
                        {

                            // Get command result
                            var result = (CommandResult) instance;

                            // Allow serialization only if errors are defined
                            return result.Error != default(dynamic);

                        };

                }

            }
            else if (property.DeclaringType.InheritsOrImplements(typeof (ImageEntryObjectModel)))
            {

                // Image entry
                
                // Check property name
                if (property.PropertyName.Equals(nameof(ImageEntryObjectModel.Tmb), StringComparison.InvariantCultureIgnoreCase))
                {

                    // Adjust serialization
                    property.Converter = new ELFinderResponseThumbnailValueConverter();

                }

            }

            // Return property
            return property;

        }

        #endregion

    }
}