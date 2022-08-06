using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Runtime.Core.Data.Factories
{
    public class NNodeFactory
    {
        public NNode ConstructNode(NGraph graph)
        {
            var node = new NNode(graph.NodeId);
            graph.AddNode(node);
            return node;
        }
    }
}