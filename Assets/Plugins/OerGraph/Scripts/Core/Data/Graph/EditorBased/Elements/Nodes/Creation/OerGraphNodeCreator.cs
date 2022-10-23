using OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes.Impl;

namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes.Creation
{
    public static class OerGraphNodeCreator
    {
        public static OerNode CreateNode(OerGraph graph, string nodeKey, int id)
        {
            // TODO: later can be changed to something more efficient,
            // but this approach is not that bad, after all
            OerNode node = null;
            if (nodeKey == "TestOerNode") 
                node = new TestOerNumberOperationsNode();
            node.Initialize(graph, id);
            return node;
        }
    }
}