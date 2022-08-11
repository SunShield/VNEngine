using System;
using System.Collections;
using System.Reflection;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Ports;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
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
                    if (attribute is InputAttribute)
                    {
                        var value = fieldData.GetValue(node);
                        if (value is INPort port) 
                            AddPort(port, fieldData.Name, node, NPortType.Input, graphView);
                        else if (value is IList && value.GetType().IsGenericType) // list
                        {
                            // TODO: checks and errors here and in similar places later
                            var valueTyped = value as IList;
                            var valueElementType = valueTyped.GetType().GetGenericArguments()[0];
                            var backingValue = valueElementType.GetField("BackingValue",
                                BindingFlags.NonPublic | BindingFlags.Instance);
                            var portType = backingValue.FieldType;
                            AddDynamicPortsList(fieldData.Name, valueTyped, node, NPortType.Input, graphView, portType);
                        }
                    }

                    if (attribute is OutputAttribute)
                    {
                        var value = fieldData.GetValue(node);
                        if (value is INPort port) 
                            AddPort(port, fieldData.Name, node, NPortType.Output, graphView);
                        else if (value is IList && value.GetType().IsGenericType) // list
                        {
                            
                        }
                    }
                }
            }
        }

        public static void AddPort(INPort port, string fieldName, NNode node, NPortType type, NGraphView graphView)
        {
            port.Initialize(node, type);
            var nodeView = graphView.Nodes[node.Id];
            _portViewConstructor.Construct(port, fieldName, nodeView, type, graphView);
        }

        public static NDynamicPortsView AddDynamicPortsList(string fieldName, IList runtimePortsList, NNode node, NPortType type, NGraphView graphView, Type portType)
        {
            var nodeView = graphView.Nodes[node.Id];
            var dynamicPortsView = new NDynamicPortsView(graphView, nodeView, runtimePortsList, fieldName, type, portType);
            nodeView.inputContainer.Add(dynamicPortsView);
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
                    if (attribute is InputAttribute or OutputAttribute)
                    { 
                        var value = fieldData.GetValue(runtimeNode);
                        if (value is INPort port) 
                            AddExistingPort(nodeView, fieldData.Name, port, port.PortType, graphView);
                        else
                        {
                            var valueTyped = value as IList;
                            var valueElementType = valueTyped.GetType().GetGenericArguments()[0];
                            var backingValue = valueElementType.GetField("BackingValue",
                                BindingFlags.NonPublic | BindingFlags.Instance);
                            var portType = backingValue.FieldType;
                            var dynPortsView = AddDynamicPortsList(fieldData.Name, valueTyped, runtimeNode, NPortType.Input, graphView, portType);

                            foreach (var dynPort in valueTyped)
                            {
                                var dynPortTyped = dynPort as INPort;
                                dynPortsView.AddExistingPort(dynPortTyped);
                            }
                        }
                    }
                }
            }
        }

        public static NPortView AddExistingPort(NNodeView nodeView, string fieldName, INPort port, NPortType type, NGraphView graphView)
        {
            var portView = _portViewConstructor.Construct(port, fieldName, nodeView, type, graphView);
            return portView;
        }
        
        public static NPortView AddExistingDynamicPort(NNodeView nodeView, string fieldName, INPort port, NPortType type, NGraphView graphView)
        {
            var portView = _portViewConstructor.ConstructDynamicPort(port, fieldName, nodeView, type, graphView);
            return portView;
        }
    }
}