using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement
{
    public static class OerGraphNodeCreator
    {
        public static OerNode CreateNode(OerMainGraph mainGraph, string nodeKey, int id)
        {
            // TODO: later can be changed to something more efficient,
            // but this approach is not that bad, after all
            OerNode node = null;
            if (nodeKey == "TestOerNode") 
                node = new TestOerNumberOperationsNode();
            node.Initialize(mainGraph, id);
            return node;
        }
    }
}