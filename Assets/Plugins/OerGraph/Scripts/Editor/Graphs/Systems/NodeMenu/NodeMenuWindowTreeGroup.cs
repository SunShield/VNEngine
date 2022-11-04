using System.Collections.Generic;

namespace OerGraph.Editor.Graphs.Systems.NodeMenu
{
    public class NodeMenuWindowTreeGroup
    {   
        public string Name;
        public SortedDictionary<string, NodeMenuWindowTreeGroup> Groups = new();

        /// <summary>
        /// Short entry name - full entry name
        /// </summary>
        public SortedSet<(string, string)> Entries = new();

        public NodeMenuWindowTreeGroup() { }
        public NodeMenuWindowTreeGroup(string name) => Name = name;
    }
}