using System;
using System.Reflection;

namespace Plugin.DocuFlow.Documentation
{
    /// <summary>
    /// Represents the documentation data for a method.
    /// </summary>
    public class MethodDocumentationData
    {
        /// <summary>
        /// The MethodInfo object of the method.
        /// </summary>
        public MethodInfo Method { get; }
    
        /// <summary>
        /// A textual description of the method.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Constructs a new MethodDocumentationData with the given MethodInfo and description.
        /// </summary>
        /// <param name="method">The MethodInfo of the method to be documented.</param>
        /// <param name="description">A textual description of the method.</param>
        public MethodDocumentationData(MethodInfo method, string description)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
