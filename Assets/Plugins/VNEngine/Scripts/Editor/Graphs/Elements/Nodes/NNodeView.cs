using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Systems.Styling;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Nodes
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
            AddToClassList("n-nodeView");
            AddDefaultStyleSheet();
        }
        
        private void AddDefaultStyleSheet() => NStyleSheetResourceLoader.TryAddStyleSheetFromPath(NViewConstants.DefaultNodeStyleSheetPath, this);

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
