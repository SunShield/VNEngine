using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Factories;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Editor.Graphs.Systems.ElementManagement
{
    public static class OerPortManager
    {
        public static void AddAllNodePorts(OerNode node, OerGraphView graphView)
        {
            foreach (var inPortId in node.InPortIds)
            {
                var runtimePort = graphView.Graph.GetPort(inPortId);
                AddRegularPort(graphView, runtimePort, node);
            }

            foreach (var outPortId in node.OutPortIds)
            {
                var runtimePort = graphView.Graph.GetPort(outPortId);
                AddRegularPort(graphView, runtimePort, node);
            }

            foreach (var inDynamicPortId in node.InDynamicPortIds)
            {
                var runtimePort = graphView.Graph.GetDynamicPort(inDynamicPortId);
                AddDynamicPorts(graphView, runtimePort, node);
            }
            
            foreach (var outDynamicPortId in node.OutDynamicPortIds)
            {
                var runtimePort = graphView.Graph.GetDynamicPort(outDynamicPortId);
                AddDynamicPorts(graphView, runtimePort, node);
            }
        }

        private static OerPortView AddRegularPort(OerGraphView graphView, IOerPort runtimePort, OerNode portNode)
        {
            var nodeView = graphView.Nodes[portNode.Id];
            var view = OerPortViewFactory.ConstructRegularPort(runtimePort, nodeView, runtimePort.Type, graphView);
            return view;
        }

        private static OerDynamicPortsView AddDynamicPorts(OerGraphView graphView, IOerDynamicPort runtimePort, OerNode portNode)
        {
            var dynamicPortsView = OerPortViewFactory.ConstructDynamicPortsView(runtimePort.PortIds, runtimePort.Name, portNode, runtimePort.Type, graphView);
            foreach (var portId in runtimePort.PortIds)
            {
                var port = graphView.Graph.GetPort(portId);
                var view = OerPortViewFactory.ConstructDynamicPort(port, runtimePort.Type, graphView);
                dynamicPortsView.AddPortView(view, () =>
                {
                    dynamicPortsView.RemovePortView(view);
                    graphView.Graph.RemovePortFromDynamicPort(view.RuntimePortId, runtimePort.Id);
                });
            }
            
            dynamicPortsView.onAddPortClick += () =>
            {
                var port = graphView.Graph.AddPortToDynamicPort(runtimePort.PortKey, $"{runtimePort.Name}-{runtimePort.PortIds.Count}", runtimePort.NodeId, runtimePort.Id);
                var view = OerPortViewFactory.ConstructDynamicPort(port, runtimePort.Type, graphView);
                dynamicPortsView.AddPortView(view, () =>
                {
                    dynamicPortsView.RemovePortView(view);
                    graphView.Graph.RemovePortFromDynamicPort(view.RuntimePortId, runtimePort.Id);
                });
            };
            return dynamicPortsView;
        }
    }
}