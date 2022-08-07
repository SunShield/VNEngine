using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes.Implementations;

namespace VNEngine.Runtime.Core.Data.Factories
{
    public class NNodeFactory
    {
        public NNode ConstructNode(NGraph graph, string nodeType)
        {
            var node = ContructByType(nodeType, graph.NodeId);
            graph.AddNode(node);
            return node;
        }

        private NNode ContructByType(string nodeType, int id) => nodeType switch
        {
            "BasicNode" => new NBasicTestNode(id)
        };
    }
}