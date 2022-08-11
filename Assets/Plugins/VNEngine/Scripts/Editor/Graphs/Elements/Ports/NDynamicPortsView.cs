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
        public NPortType Type => _portType;

        public NDynamicPortsView(NGraphView graphView, NNodeView nodeView, IList runtimePorts, string fieldName, NPortType portType, Type type)
        {
            _graphView = graphView;
            _nodeView = nodeView;
            _runtimePorts = runtimePorts;
            _fieldName = fieldName;
            _portType = portType;
            _type = type;
            
            var newPortButton = NUiElementsUtility.CreateButton("New", AddPort);
            Add(newPortButton);
        }

        private void AddPort()
        {
            var dynPort = NPortManager.AddDynamicPort(_fieldName, _nodeView, _type, _portType, _graphView);
            _runtimePorts.Add(dynPort.runtimePort);
            AddPortView(dynPort.portView);
            
            EditorUtility.SetDirty(_graphView.Graph);
        }

        public void AddExistingPort(INPort runtimePort)
        {
            var portView = NPortManager.AddExistingDynamicPort(runtimePort, _fieldName, _nodeView, _portType, _graphView);
            AddPortView(portView);
            
            EditorUtility.SetDirty(_graphView.Graph);
        }

        public void RemovePort(NDynamicPortView portView)
        {
            var runtimePort = portView.RuntimePort;
            _runtimePorts.Remove(runtimePort);
            RemovePortView(portView);
        }

        private void AddPortView(NDynamicPortView view)
        {
            Ports.Add(view);
            Add(view);
        }

        private void RemovePortView(NDynamicPortView view)
        {
            Ports.Remove(view);
            Remove(view);
        }
    }
}