using System;
using System.Collections;
using System.Reflection;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Factories;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Attributes.Ports;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Systems.ElementsManipulation
{
    public static class NPortManager
    {
        private static NPortViewConstructor _portViewConstructor = new();
        
        public static void AddAllNodePorts(NNode node, NGraphView graphView)
        {
            var nodeType = node.GetType();
            var fieldsData = nodeType.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );

            foreach (var fieldData in fieldsData)
            {
                var attributes = fieldData.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is not NPortAttribute pa) continue;

                    AddNewPort(node, graphView, fieldData, pa.Type);
                }
            }
        }

        private static void AddNewPort(NNode node, NGraphView graphView, FieldInfo fieldData, NPortType type)
        {
            var value = fieldData.GetValue(node);
            
            if (value is INPort port) 
                AddRegularPort(port, fieldData.Name, node, type, graphView);
            else if (value is IList valueTyped)
                AddDynamicPortsList(fieldData.Name, valueTyped, node, type, graphView, GetPortType(valueTyped));
        }

        public static void AddRegularPort(INPort port, string fieldName, NNode node, NPortType type, NGraphView graphView)
        {
            port.Initialize(node, type);
            var nodeView = graphView.Nodes[node.Id];
            _portViewConstructor.Construct(port, fieldName, nodeView, type, graphView);
        }

        public static (INPort runtimePort, NDynamicPortView portView) AddDynamicPort(string fieldName, NNodeView nodeView, Type portValueType, NPortType type, NGraphView graphView)
        {
            var runtimeGraph = graphView.Graph.RuntimeGraph; 
            var newPortId = runtimeGraph.PortId;
            var runtimePort = PortByTypeFactory.CreatePort(portValueType, newPortId);
            var runtimeNode = runtimeGraph.Nodes[nodeView.Id];
            runtimePort.Initialize(runtimeNode, type);
            
            var portView = _portViewConstructor.ConstructDynamicPort(runtimePort, $"{fieldName} {newPortId}", nodeView, type, graphView);
            
            return (runtimePort, portView);
        }
        
        public static NDynamicPortView AddExistingDynamicPort(INPort runtimePort, string fieldName, NNodeView nodeView, NPortType type, NGraphView graphView)
        {
            var portView = _portViewConstructor.ConstructDynamicPort(runtimePort, fieldName, nodeView, type, graphView);
            return portView;
        }

        public static NDynamicPortsView AddDynamicPortsList(string fieldName, IList runtimePortsList, NNode node, NPortType type, NGraphView graphView, Type portType)
        {
            var dynamicPortsView = _portViewConstructor.ConstructDynamicPortsView(fieldName, runtimePortsList, node, type, graphView, portType);
            return dynamicPortsView;
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
                var dynPortsView = AddDynamicPortsList(fieldData.Name, valueTyped, runtimeNode, pa.Type, graphView, portType);

                foreach (var dynPort in valueTyped)
                {
                    var dynPortTyped = dynPort as INPort;
                    dynPortsView.AddExistingPort(dynPortTyped);
                }
            }
        }

        public static NPortView AddExistingRegularPort(NNodeView nodeView, string fieldName, INPort port, NPortType type, NGraphView graphView)
            => _portViewConstructor.Construct(port, fieldName, nodeView, type, graphView);

        private static Type GetPortType(IList valueTyped)
        {
            var valueElementType = valueTyped.GetType().GetGenericArguments()[0];
            var backingValue = valueElementType.GetField("BackingValue",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var portType = backingValue.FieldType;
            return portType;
        }
    }
}