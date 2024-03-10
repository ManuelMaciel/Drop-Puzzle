using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.DocuFlow.Documentation
{
    /// <summary>
    /// Manages the collection of class documentation data within the application.
    /// </summary>
    public static class DocumentationManager
    {
        public static List<ClassDocumentationData> Documentation { get; private set; }
    
        public static async Task RefreshDocumentationAsync()
        {
            Documentation = await DocumentationCollector.GetDocumentationAsync();
        }
    }
}

