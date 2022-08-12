﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Editor.Graphs.Systems.ElementDeletion;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Factories;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Attributes.Ports;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Systems.ElementsManipulation
{
    public static class NPortManager
    {
        private static NPortViewFactory _portViewFactory = new();
        
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
            var newPort = Activator.CreateInstance(fieldData.FieldType, graphView.Graph.RuntimeGraph.PortId);
            fieldData.SetValue(node, newPort);
            var value = fieldData.GetValue(node);
            
            if (value is INPort port) 
                AddRegularPort(port, fieldData.Name, node, type, graphView);
            else if (value is IList valueTyped)
                AddNewDynamicPortsList(fieldData.Name, valueTyped, node, type, graphView, GetPortType(valueTyped));
        }

        public static void AddRegularPort(INPort port, string fieldName, NNode node, NPortType type, NGraphView graphView)
        {
            port.Initialize(node, fieldName, type);
            node.Graph.AddPort(port);
            var nodeView = graphView.Nodes[node.Id];
            _portViewFactory.Construct(port, fieldName, nodeView, type, graphView);
        }

        public static NDynamicPortView AddNewDynamicPort(IList dynamicPorts, string fieldName, NNodeView nodeView, Type portValueType, NPortType type, NGraphView graphView)
        {
            var runtimeGraph = graphView.Graph.RuntimeGraph; 
            var newPortId = runtimeGraph.PortId;
            var runtimePort = PortByTypeFactory.CreatePort(portValueType, newPortId);
            runtimeGraph.AddPort(runtimePort);
            var runtimeNode = runtimeGraph.Nodes[nodeView.Id];
            runtimePort.Initialize(runtimeNode, fieldName, type);
            dynamicPorts.Add(runtimePort);
            
            var portView = _portViewFactory.ConstructDynamicPortView(runtimePort, $"{fieldName} {newPortId}", nodeView, type, graphView);
            
            return portView;
        }

        public static void AddNewDynamicPortsList(string fieldName, IList runtimePortsList, NNode node, NPortType type, NGraphView graphView, Type portType)
        {
            var dynamicPortsView = _portViewFactory.ConstructDynamicPortsView(runtimePortsList, node, type, graphView);
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
                var portView = _portViewFactory.ConstructDynamicPortView(dynPortTyped, $"{fieldName} {dynPortTyped.Id}", nodeView, type, graphView);
                var removePortAction = BuildRemoveDynamicPortViewAction(dynamicPortsView, portView, runtimePortsList, graphView);
                dynamicPortsView.AddPortView(portView, removePortAction);
            }
        }
        
        private static Action BuildRemoveDynamicPortViewAction(NDynamicPortsView dynamicPortsView, NDynamicPortView portView, IList runtimePortsList, NGraphView graphView)
        {
            Action removePortAction = () =>
            {
                dynamicPortsView.RemovePortView(portView);
                RemoveDynamicPort(runtimePortsList, portView.RuntimePort);
                var edges = new HashSet<Edge>(portView.ConnectedEdges);
                foreach (var edge in edges)
                {
                    NSelectionDeleter.DeleteEdge(graphView, graphView.Graph.RuntimeGraph, edge);
                }
            };

            return removePortAction;
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

        public static void AddExistingRegularPort(NNodeView nodeView, string fieldName, INPort port, NPortType type, NGraphView graphView)
            => _portViewFactory.Construct(port, fieldName, nodeView, type, graphView);

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