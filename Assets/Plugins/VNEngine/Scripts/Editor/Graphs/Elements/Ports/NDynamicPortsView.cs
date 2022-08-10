using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Editor.Service.Utilities;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    public class NDynamicPortsView : GraphElement
    {
        private NGraphView _graphView;
        private NNodeView _nodeView;
        private IList _runtimePorts;
        private string _fieldName;
        private NPortType _portType;
        private Type _type;
        
        public List<NPortView> Ports = new();

        public NDynamicPortsView(NGraphView graphView, NNodeView nodeView, IList runtimePorts, string fieldName, NPortType portType, Type type)
        {
            _graphView = graphView;
            _nodeView = nodeView;
            _runtimePorts = runtimePorts;
            _fieldName = fieldName;
            _portType = portType;
            _type = type;
            
            var newPortButton = UiElementsUtility.CreateButton("New", AddPort);
            Add(newPortButton);
        }

        private void AddPort()
        {
            var newPortId = _graphView.Graph.RuntimeGraph.PortId;
            var runtimePort = PortByTypeFactory.CreatePort(_type, newPortId);
            _runtimePorts.Add(runtimePort);

            var portView = PortManager.AddExistingDynamicPort(_nodeView, $"{_fieldName} {newPortId}", runtimePort, _portType, _graphView);
            Ports.Add(portView);
            Add(portView);
            
            EditorUtility.SetDirty(_graphView.Graph);
        }

        public void AddExistingPort(INPort runtimePort)
        {
            var portView = PortManager.AddExistingDynamicPort(_nodeView, $"{_fieldName} {runtimePort.Id}", runtimePort, _portType, _graphView);
            Ports.Add(portView);
            Add(portView);
            
            EditorUtility.SetDirty(_graphView.Graph);
        }

        public void RemovePort(NDynamicPortView portView)
        {
            var runtimePort = portView.RuntimePort;
            _runtimePorts.Remove(runtimePort);
            Ports.Remove(portView);
            Remove(portView);
        }
    }
}