﻿using System.Collections.Generic;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Nodes;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.PortFields;
using VNEngine.Runtime.Core.Data;
using VNEngine.Runtime.Core.Data.Elements.Ports.Implementations;

namespace VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes.Implementations
{
    [NNodeParams(nodeClassName: "nodeView-basictest", nodeStylesheetPath: "Styles/Nodes/NBaseTestNodeStyles")]
    public class NBasicTestNode : NNode
    {
        [NInput] public List<NIntPort> IntsIn;
        [NInput] public NBoolPort BoolIn;
        [NInput] public NStringPort StringIn;
        [NOutput] public List<NIntPort> IntsOut;
        [NOutput] public List<NStringPort> StringOut;

        public NBasicTestNode(NGraph graph, int id) : base(graph, id)
        {
        }

        public override object GetOutputValue(string portName, int index = 0)
        {
            if (portName == "B")
            {
                if (IntsIn.Count == 0) return index;

                var maxInput = (int)IntsIn[0].GetValue();
                foreach (var port in IntsIn)
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