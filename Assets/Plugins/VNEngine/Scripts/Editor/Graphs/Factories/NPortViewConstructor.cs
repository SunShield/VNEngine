using System;
using System.Collections;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public class NPortViewConstructor
    {
        public NPortView Construct(INPort port, string fieldName, NNodeView nodeView, NPortType type, NGraphView graphView)
        {
            var portView = new NPortView(graphView, fieldName, port, type, port.Type);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
        }
        
        public NDynamicPortView ConstructDynamicPort(INPort port, string fieldName, NNodeView nodeView, NPortType type, NGraphView graphView)
        {
            var portView = new NDynamicPortView(graphView, fieldName, port, type, port.Type);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
        }

        public NDynamicPortsView ConstructDynamicPortsView(string fieldName, IList runtimePortsList, NNode node, NPortType type, NGraphView graphView, Type portType)
        {
            var nodeView = graphView.Nodes[node.Id];
            var dynamicPortsView = new NDynamicPortsView(graphView, nodeView, runtimePortsList, fieldName, type, portType);
            nodeView.AddDynamicPortsView(dynamicPortsView);
            return dynamicPortsView;
        }
    }
}