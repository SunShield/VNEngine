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
            var nodeView = graphView.Nodes[portNode.Id];
            var dynamicPortsView = OerPortViewFactory.ConstructDynamicPortsView(runtimePort.PortIds, portNode, runtimePort.Type, graphView);
            foreach (var portId in runtimePort.PortIds)
            {
                var port = graphView.Graph.GetPort(portId);
                var view = OerPortViewFactory.ConstructDynamicPort(port, nodeView, runtimePort.Type, graphView);
                dynamicPortsView.AddPortView(view, () =>
                {
                    dynamicPortsView.RemovePortView(view);
                    graphView.Graph.RemovePortFromDynamicPort(view.RuntimePortId, runtimePort.Id);
                });
            }
            
            dynamicPortsView.onAddPortClick += () =>
            {
                var port = graphView.Graph.AddPortToDynamicPort(runtimePort.PortKey, $"{runtimePort.Name}-{runtimePort.PortIds.Count}", runtimePort.NodeId, runtimePort.Id);
                var view = OerPortViewFactory.ConstructDynamicPort(port, nodeView, runtimePort.Type, graphView);
                dynamicPortsView.AddPortView(view, () =>
                {
                    dynamicPortsView.RemovePortView(view);
                    graphView.Graph.RemovePortFromDynamicPort(view.RuntimePortId, runtimePort.Id);
                });
            };
            return dynamicPortsView;
        }

        /*private static void AddNewPort(OerNode node, OerGraphView graphView, FieldInfo fieldData, NPortType type)
        {
            var newPort = Activator.CreateInstance(fieldData.FieldType, graphView.Graph.RuntimeGraph.PortId);
            fieldData.SetValue(node, newPort);
            var value = fieldData.GetValue(node);
            
            if (value is INPort port) 
                AddNewRegularPort(port, fieldData.Name, node, type, graphView);
            else if (value is IList valueTyped)
                AddNewDynamicPortsList(fieldData.Name, valueTyped, node, type, graphView, GetPortType(valueTyped));
        }

        private static void AddNewRegularPort(INPort port, string fieldName, NNode node, NPortType type, NGraphView graphView)
        {
            port.Initialize(node, fieldName, type);
            node.Graph.AddPort(port);
            var nodeView = graphView.Nodes[node.Id];
            var portParams = NAttributeChecker.GetPortHasParamsAttribute(port);
            NPortViewFactory.ConstructPort(port, fieldName, nodeView, type, graphView, portParams);
        }

        private static void AddNewDynamicPortsList(string fieldName, IList runtimePortsList, NNode node, NPortType type, NGraphView graphView, Type portType)
        {
            var dynamicPortsView = NPortViewFactory.ConstructDynamicPortsView(runtimePortsList, node, type, graphView);
            var nodeView = graphView.Nodes[node.Id];
            
            dynamicPortsView.onAddPortClick += () =>
            {
                var portView = AddNewDynamicPort(runtimePortsList, fieldName, nodeView, portType, type, graphView);
                var removePortAction = BuildRemoveDynamicPortViewAction(dynamicPortsView, portView, runtimePortsList, graphView);
                dynamicPortsView.AddPortView(portView, removePortAction);
            };
            
            foreach (var dynPort in runtimePortsList)
            {
                var dynPortTyped = dynPort as INPort;
                var portView = NPortViewFactory.ConstructPort(dynPortTyped, $"{fieldName} {dynPortTyped.Id}", nodeView, type, graphView);
                var removePortAction = BuildRemoveDynamicPortViewAction(dynamicPortsView, portView, runtimePortsList, graphView);
                dynamicPortsView.AddPortView(portView, removePortAction);
            }
        }

        private static NPortView AddNewDynamicPort(IList dynamicPorts, string fieldName, NNodeView nodeView, Type portValueType, NPortType type, NGraphView graphView)
        {
            var runtimeGraph = graphView.Graph.RuntimeGraph; 
            var newPortId = runtimeGraph.PortId;
            // TODO: change to creation through reflection (move port constructors baked reflection elsewhere)
            var runtimePort = PortByTypeFactory.CreatePort(portValueType, newPortId);
            runtimeGraph.AddPort(runtimePort);
            var runtimeNode = runtimeGraph.Nodes[nodeView.Id];
            runtimePort.Initialize(runtimeNode, fieldName, type);
            dynamicPorts.Add(runtimePort);
            
            var portParams = NAttributeChecker.GetPortHasParamsAttribute(runtimePort);
            var portView = NPortViewFactory.ConstructPort(runtimePort, $"{fieldName} {newPortId}", nodeView, type, graphView, portParams);
            
            return portView;
        }
        
        // TODO: think on moving somewhere
        private static Action BuildRemoveDynamicPortViewAction(NDynamicPortsView dynamicPortsView, NPortView portView, IList runtimePortsList, NGraphView graphView)
        {
            void RemovePortAction()
            {
                dynamicPortsView.RemovePortView(portView);
                RemoveDynamicPort(runtimePortsList, portView.RuntimePort);
                var edges = new HashSet<Edge>(portView.ConnectedEdges);
                foreach (var edge in edges)
                {
                    NSelectionDeleter.DeleteEdge(graphView, graphView.Graph.RuntimeGraph, edge);
                }
            }

            return RemovePortAction;
        }

        private static void RemoveDynamicPort(IList runtimePorts, INPort portToRemove)
        {
            runtimePorts.Remove(portToRemove);
            portToRemove.Node.Graph.RemovePort(portToRemove.Id);
        }

        public static void AddAllNodeExistingPorts(NNode runtimeNode, NNodeView nodeView, NGraphView graphView)
        {
            var nodeType = runtimeNode.GetType();
            var fieldsData = nodeType.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );

            foreach (var fieldData in fieldsData)
            {
                var attributes = fieldData.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is not NPortAttribute pa) continue;

                    AddExistingPort(runtimeNode, nodeView, graphView, fieldData, pa);
                }
            }
        }

        private static void AddExistingPort(NNode runtimeNode, NNodeView nodeView, NGraphView graphView, FieldInfo fieldData,
            NPortAttribute pa)
        {
            var value = fieldData.GetValue(runtimeNode);
            if (value is INPort port)
                AddExistingRegularPort(nodeView, fieldData.Name, port, port.PortType, graphView);
            else if (value is IList valueTyped)
            {
                var portType = GetPortType(valueTyped);
                AddNewDynamicPortsList(fieldData.Name, valueTyped, runtimeNode, pa.Type, graphView, portType);
            }
        }

        private static void AddExistingRegularPort(NNodeView nodeView, string fieldName, INPort port, NPortType type, NGraphView graphView)
        {
            var portParams = NAttributeChecker.GetPortHasParamsAttribute(port);
            NPortViewFactory.ConstructPort(port, fieldName, nodeView, type, graphView, portParams);
        }

        // TODO: move elsewhere
        private static Type GetPortType(IList valueTyped)
        {
            var valueElementType = valueTyped.GetType().GetGenericArguments()[0];
            var backingValue = valueElementType.GetField("BackingValue",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var portType = backingValue.FieldType;
            return portType;
        }*/
    }
}