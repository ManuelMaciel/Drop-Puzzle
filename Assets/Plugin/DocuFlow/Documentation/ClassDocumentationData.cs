using System;
using System.Collections.Generic;

namespace Plugin.DocuFlow.Documentation
{
    /// <summary>
    /// Represents the documentation data for a class including its methods and properties.
    /// </summary>
    public class ClassDocumentationData
    {
        /// <summary>
        /// The Type of the class that this instance is documenting.
        /// </summary>
        public Type ClassType { get; }
    
        /// <summary>
        /// A textual description of the class.
        /// </summary>
        public string Description { get; }

        private readonly List<MethodDocumentationData> methodsData = new List<MethodDocumentationData>();
        public IReadOnlyList<MethodDocumentationData> MethodsData => methodsData;

        private readonly List<PropertyDocumentationData> propertiesData = new List<PropertyDocumentationData>();
        public IReadOnlyList<PropertyDocumentationData> PropertiesData => propertiesData;

    
        /// <summary>
        /// Initializes a new instance of the ClassDocumentationData class.
        /// </summary>
        /// <param name="classType">The Type of the class to be documented.</param>
        /// <param name="description">A textual description of the class.</param>
        public ClassDocumentationData(Type classType, string description)
        {
            ClassType = classType;
            Description = description;
        }

        /// <summary>
        /// Adds a method's documentation data to the list of documented methods for this class.
        /// </summary>
        /// <param name="methodData">The documentation data of the method to be added.</param>
        public void AddMethodData(MethodDocumentationData methodData)
        {
            if (methodData == null)
            {
                throw new ArgumentNullException(nameof(methodData));
            }
            methodsData.Add(methodData);
        }

        /// <summary>
        /// Adds a property's documentation data to the list of documented properties for this class.
        /// </summary>
        /// <param name="propertyData">The documentation data of the property to be added.</param>
        public void AddPropertyData(PropertyDocumentationData propertyData)
        {
            if (propertyData == null)
            {
                throw new ArgumentNullException(nameof(propertyData));
            }
            propertiesData.Add(propertyData);
        }
    }
}