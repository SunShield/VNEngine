using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Ports;
using VNEngine.Runtime.Core.Data;
using VNEngine.Runtime.Core.Data.Elements.Ports.Implementations;

namespace VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes.Implementations
{
    public class NBasicTestNode : NNode
    {
        [Input] public NIntPort A;
        [Output] public NIntPort B;

        public NBasicTestNode(NGraph graph, int id) : base(graph, id)
        {
            A = new(graph.PortId);
            B = new(graph.PortId)
            {
                HasBackingField = false
            };
        }
    }
}