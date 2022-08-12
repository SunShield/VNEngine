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

        public override object GetOutputValue(string portName, int index = 0)
        {
            if (portName == "B")
            {
                if (A.Count == 0) return index;

                var maxInput = (int)A[0].GetValue();
                foreach (var port in A)
                {
                    var value = (int)port.GetValue();
                    if (value <= maxInput) continue;
                    maxInput = value;
                }
                
                return index + maxInput;
            }

            return null;
        }
    }
}