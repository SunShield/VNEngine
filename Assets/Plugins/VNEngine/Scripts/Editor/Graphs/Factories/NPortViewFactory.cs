using System.Collections;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Systems.Styling;
using VNEngine.Runtime.Core.Graphs.Attributes.Ports;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;
using VNEngine.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public class NPortViewFactory
    {
        public NPortView Construct(INPort port, string fieldName, NNodeView nodeView, NPortType type, NGraphView graphView, NPortParamsAttribute @params = null)
        {
            var portView = new NPortView(graphView, fieldName, port, type, port.Type);
            if (@params != null) ApplyParams(portView, @params);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
        }
        
        public NDynamicPortView ConstructDynamicPortView(INPort port, string fieldName, NNodeView nodeView, NPortType type, NGraphView graphView, NPortParamsAttribute @params = null)
        {
            var portView = new NDynamicPortView(graphView, fieldName, port, type, port.Type);
            if (@params != null) ApplyParams(portView, @params);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
        }

        private void ApplyParams(NPortView portView, NPortParamsAttribute @params)
        {
            portView.AddToClassList(@params.PortClassName);
            if (!string.IsNullOrEmpty(@params.PortStyleSheetPath))
                NStyleSheetResourceLoader.TryAddStyleSheetFromPath(@params.PortStyleSheetPath, portView);
        }

        public NDynamicPortsView ConstructDynamicPortsView(IList runtimePortsList, NNode node, NPortType type, NGraphView graphView)
        {
            var nodeView = graphView.Nodes[node.Id];
            var dynamicPortsView = new NDynamicPortsView(graphView, runtimePortsList, type);
            nodeView.AddDynamicPortsView(dynamicPortsView);
            return dynamicPortsView;
        }
    }
}