using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Scripts.Editor.Graphs.Elements.Nodes
{
    public class NNodeView : Node
    {
        private NNode _node;
        private Dictionary<string, NPortView> _inputs = new();
        private Dictionary<string, NPortView> _outputs = new();

        public int Id => _node.Id;
        
        public NNodeView(NNode node)
        {
            _node = node;
        }

        public void AddPort(NPortView port)
        {
            if (port.direction == Direction.Input)
            {
                _inputs.Add(port.portName, port);
                inputContainer.Add(port);
            }
            else
            {
                _outputs.Add(port.portName, port);
                outputContainer.Add(port);
            }
        }
    }
}
