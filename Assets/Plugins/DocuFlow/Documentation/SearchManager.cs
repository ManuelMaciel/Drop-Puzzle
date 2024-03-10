using System;
using System.Collections.Generic;
using System.Linq;

namespace Plugin.DocuFlow.Documentation
{
    public static class SearchManager
    {
        private static readonly TrieNode root = new TrieNode();

        // Build Trie tree for the given list of class documentation
        public static void BuildTrie(List<ClassDocumentationData> documentation)
        {
            foreach (var classDoc in documentation)
            {
                Insert(classDoc.ClassType.Name);
                foreach (var method in classDoc.MethodsData)
                {
                    Insert(method.Method.Name);
                }
                foreach (var property in classDoc.PropertiesData)
                {
                    Insert(property.Property.Name);
                }
                Insert(classDoc.Description);
            }
        }

        private static void Insert(string key)
        {
            var node = root;
            foreach (var ch in key.ToLowerInvariant())
            {
                if (!node.Children.ContainsKey(ch))
                {
                    node.Children[ch] = new TrieNode();
                }
                node = node.Children[ch];
            }
            node.IsEndOfWord = true;
        }

        // Trie based filter
        public static List<ClassDocumentationData> FilterAndScoreDocumentation(string query, List<ClassDocumentationData> documentation)
        {
            const int MaxLevenshteinDistance = 2; // adjust this to control how fuzzy the search is

            if (string.IsNullOrWhiteSpace(query))
            {
                return documentation;
            }

            // Get all words in the Trie that start with the query as a prefix
            var suggestions = GetSuggestions(query);
    
            // Filter the documents based on these suggestions
            var filteredDocs = documentation
                .Where(classDoc => 
                    suggestions.Any(suggestion => 
                        classDoc.ClassType.Name.Contains(suggestion, StringComparison.OrdinalIgnoreCase) ||
                        classDoc.MethodsData.Any(m => m.Method.Name.Contains(suggestion, StringComparison.OrdinalIgnoreCase)) ||
                        classDoc.PropertiesData.Any(p => p.Property.Name.Contains(suggestion, StringComparison.OrdinalIgnoreCase)) ||
                        classDoc.Description.Contains(suggestion, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();

            // Levenshtein Distance-based filtering
            var fuzzyFilteredDocs = filteredDocs
                .Where(classDoc =>
                    ComputeLevenshteinDistance(classDoc.ClassType.Name, query) <= MaxLevenshteinDistance ||
                    classDoc.MethodsData.Any(m => ComputeLevenshteinDistance(m.Method.Name, query) <= MaxLevenshteinDistance) ||
                    classDoc.PropertiesData.Any(p => ComputeLevenshteinDistance(p.Property.Name, query) <= MaxLevenshteinDistance) ||
                    ComputeLevenshteinDistance(classDoc.Description, query) <= MaxLevenshteinDistance)
                .ToList();

            return fuzzyFilteredDocs;
        }

        public static int ComputeLevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                if (string.IsNullOrEmpty(target)) return 0;
                return target.Length;
            }
            if (string.IsNullOrEmpty(target)) return source.Length;

            var matrix = new int[source.Length + 1, target.Length + 1];

            // Initialize the first row and column of the matrix
            for (var i = 0; i <= source.Length; i++) matrix[i, 0] = i;
            for (var j = 0; j <= target.Length; j++) matrix[0, j] = j;

            // Fill in the rest of the matrix
            for (var i = 1; i <= source.Length; i++)
            {
                for (var j = 1; j <= target.Length; j++)
                {
                    var cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[source.Length, target.Length];
        }
        
        public static List<string> GetSuggestions(string prefix) 
        {
            var node = root;
            foreach (var ch in prefix.ToLowerInvariant())
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return new List<string>();
                }
                node = node.Children[ch];
            }
            return GetAllWords(node, prefix);
        }

        public static List<string> GetAllWords(TrieNode node, string prefix) 
        {
            var words = new List<string>();
            if (node.IsEndOfWord)
            {
                words.Add(prefix);
            }
            foreach (var pair in node.Children)
            {
                var childPrefix = prefix + pair.Key;
                var childWords = GetAllWords(pair.Value, childPrefix);
                words.AddRange(childWords);
            }
            return words;
        }

    }
}


