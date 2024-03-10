using System;
using System.Reflection;

namespace Plugin.DocuFlow.Documentation
{
    /// <summary>
    /// Represents the documentation data for a property.
    /// </summary>
    public class PropertyDocumentationData
    {
        /// <summary>
        /// The PropertyInfo object of the property.
        /// </summary>
        public PropertyInfo Property { get; }
    
        /// <summary>
        /// A textual description of the property.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Constructs a new PropertyDocumentationData with the given PropertyInfo and description.
        /// </summary>
        /// <param name="property">The PropertyInfo of the property to be documented.</param>
        /// <param name="description">A textual description of the property.</param>
        public PropertyDocumentationData(PropertyInfo property, string description)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}