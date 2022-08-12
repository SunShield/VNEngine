using System;
using System.Collections;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public class NPortViewFactory
    {
        public NPortView Construct(INPort port, string fieldName, NNodeView nodeView, NPortType type, NGraphView graphView)
        {
            var portView = new NPortView(graphView, fieldName, port, type, port.Type);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
        }
        
        public NDynamicPortView ConstructDynamicPortView(INPort port, string fieldName, NNodeView nodeView, NPortType type, NGraphView graphView)
        {
            var portView = new NDynamicPortView(graphView, fieldName, port, type, port.Type);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
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