using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Scripts.Editor.Graphs.Elements.Nodes
{
    public class NNodeView : Node
    {
        private NNode _node;
        private Dictionary<int, NPortView> _inputs = new();
        private Dictionary<int, NPortView> _outputs = new();
        public IReadOnlyDictionary<int, NPortView> Inputs => _inputs;
        public IReadOnlyDictionary<int, NPortView> Outputs => _inputs;

        public int Id => _node.Id;
        
        public NNodeView(NNode node)
        {
            _node = node;
        }

        public void AddPort(NPortView port)
        {
            if (port.direction == Direction.Input)
            {
                _inputs.Add(port.RuntimePort.Id, port);
                inputContainer.Add(port);
            }
            else
            {
                _outputs.Add(port.RuntimePort.Id, port);
                outputContainer.Add(port);
            }
        }
        
        public void AddDynamicPortsView(NDynamicPortsView dynamicPortsView)
        {
            if (dynamicPortsView.Type == NPortType.Input)
            {
                inputContainer.Add(dynamicPortsView);
            }
            else
            {
                outputContainer.Add(dynamicPortsView);
            }
        }
    }
}
