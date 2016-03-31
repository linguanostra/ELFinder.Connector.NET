using Nancy.ModelBinding;

namespace ELFinder.Connector.Nancy.Converters
{
    /// <summary>
    /// ELFinder custom field name converter
    /// </summary>
    public class ELFinderFieldNameConverter : IFieldNameConverter
    {

        #region Properties

        /// <summary>
        /// Default field name converter
        /// </summary>
        protected static DefaultFieldNameConverter Default = new DefaultFieldNameConverter();

        #endregion

        #region Overrides

        /// <summary>
        /// Converts a field name to a property name
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Property name</returns>
        public string Convert(string fieldName)
        {

            // Check it ends with []
            if (fieldName.EndsWith(@"[]"))
            {
                
                // Normalize it
                fieldName = fieldName.Substring(0, fieldName.Length - 2);

            }
            
            // Invoke default converter
            var result = Default.Convert(fieldName);

            // Return result
            return result;

        }

        #endregion

    }
}