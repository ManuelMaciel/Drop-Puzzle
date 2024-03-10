using System.Collections.Generic;

namespace Plugin.DocuFlow.Documentation
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
        public bool IsEndOfWord { get; set; } = false;
    }
}

