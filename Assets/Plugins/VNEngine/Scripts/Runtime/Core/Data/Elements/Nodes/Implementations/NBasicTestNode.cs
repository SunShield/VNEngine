using System.Collections.Generic;
using VNEngine.Runtime.Core.Data;
using VNEngine.Runtime.Core.Data.Elements.Ports.Implementations;
using VNEngine.Scripts.Runtime.Core.Attributes.Ports;

namespace VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes.Implementations
{
    public class NBasicTestNode : NNode
    {
        [NInput] public List<NIntPort> A = new();
        [NOutput] public List<NIntPort> B = new();

        public NBasicTestNode(NGraph graph, int id) : base(graph, id)
        {
        }
    }
}