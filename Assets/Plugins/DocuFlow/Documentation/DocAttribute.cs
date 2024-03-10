using System;

namespace Plugin.DocuFlow.Documentation
{
    /// <summary>
    /// An attribute used to provide a description for classes, methods, and properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DocAttribute : Attribute
    {
        /// <summary>
        /// The description of the attribute's target.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Constructs a new DocAttribute with the given description.
        /// </summary>
        /// <param name="description">The description for the attribute's target.</param>
        public DocAttribute(string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}