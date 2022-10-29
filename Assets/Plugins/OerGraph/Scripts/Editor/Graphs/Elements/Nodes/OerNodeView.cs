using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Systems.Styling;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using UnityEditor.Experimental.GraphView;

namespace OerGraph.Editor.Graphs.Elements.Nodes
{
    public class OerNodeView : Node
    {
        public int RuntimeNodeId { get; private set; }
        public OerGraphView View { get; private set; }
        public Dictionary<int, OerPortView> Inputs { get; private set; }
        public Dictionary<int, OerPortView> Outputs { get; private set; }

        private OerMainGraph RuntimeGraph => View.Graph;

        public OerNodeView(OerGraphView view, int runtimeNodeId)
        {
            RuntimeNodeId = runtimeNodeId;
            View = view;
            Inputs = new();
            Outputs = new();
            AddToClassList(OerViewConsts.ViewClasses.OerNodeView);
            AddDefaultStyleSheet();
        }
        
        private void AddDefaultStyleSheet() => OerStyleSheetResourceLoader.TryAddStyleSheetFromPath(OerViewConsts.StyleSheets.DefaultNodeStyleSheetPath, this);
        
        public void AddPort(OerPortView port)
        {
            if (port.direction == Direction.Input)
            {
                Inputs.Add(port.RuntimePortId, port);
                inputContainer.Add(port);
            }
            else
            {
                Outputs.Add(port.RuntimePortId, port);
                outputContainer.Add(port);
            }
        }
        
        public void AddDynamicPortsView(OerDynamicPortsView dynamicPortsView)
        {
            if (dynamicPortsView.Direction == Direction.Input)
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