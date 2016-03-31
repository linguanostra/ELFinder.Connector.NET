using System;
using ELFinder.Connector.Commands.Operations.Common.Interfaces;
using ELFinder.Connector.Extensions;
using Nancy.ModelBinding;

namespace ELFinder.Connector.Nancy.Converters
{
    /// <summary>
    /// ELFinder boolean type value converter
    /// </summary>
    public class ELFinderBooleanValueConverter : ITypeConverter
    {

        #region Methods

        /// <summary>
        /// Whether the converter can convert to the destination type
        /// </summary>
        /// <param name="destinationType">Destination type</param>
        /// <param name="context">The current binding context</param>
        /// <returns>True if conversion supported, false otherwise</returns>
        public bool CanConvertTo(Type destinationType, BindingContext context)
        {

            // Only allow conversion for boolean values of connector commands
            return
                destinationType == typeof (bool)
                && context.Model.GetType().InheritsOrImplements(typeof (IConnectorCommand));

        }

        /// <summary>
        /// Convert the string representation to the destination type
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="destinationType">Destination type</param>
        /// <param name="context">Current context</param>
        /// <returns>Converted object of the destination type</returns>
        public object Convert(string input, Type destinationType, BindingContext context)
        {

            return input == "1";

        }

        #endregion

    }

}