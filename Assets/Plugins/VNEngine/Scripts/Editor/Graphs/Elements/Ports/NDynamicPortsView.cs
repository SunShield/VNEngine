using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Systems.ElementDeletion;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Editor.Service.Utilities;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    public class NDynamicPortsView : GraphElement
    {
        private VisualElement _portsContainer;
        private VisualElement _deleteButtonsContainer;
        private VisualElement _portBodysContainer;
        
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

            _portsContainer = new VisualElement();
            _portsContainer.style.flexDirection = FlexDirection.Row;
            Add(_portsContainer);

            _deleteButtonsContainer = new VisualElement();
            _portBodysContainer = new VisualElement();

            if (portType == NPortType.Input)
            {
                _portsContainer.Add(_portBodysContainer);
                _portsContainer.Add(_deleteButtonsContainer);
            }
            else
            {
                _portsContainer.Add(_deleteButtonsContainer);
                _portsContainer.Add(_portBodysContainer);
            }
        }

        private void AddPort()
        {
            var dynPort = NPortManager.AddDynamicPort(_fieldName, _nodeView, _type, _portType, _graphView);
            _runtimePorts.Add(dynPort.runtimePort);
            AddPortView(dynPort.portView);
            SetPortNames();
            
            EditorUtility.SetDirty(_graphView.Graph);
        }

        private void SetPortNames()
        {
            for (int i = 0; i < _runtimePorts.Count; i++)
            {
                var port = _runtimePorts[i] as INPort;
                port.SetIndex(i);
            }
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
            NPortManager.RemovePort(runtimePort, _graphView.Graph.RuntimeGraph);
            SetPortNames();
            RemovePortView(portView);
        }

        private void AddPortView(NDynamicPortView view)
        {
            Ports.Add(view);
            _portBodysContainer.Add(view);

            var newPortButton = new Button()
            {
                text = "X"
            };

            // TODO: refactor this shit
            // TODO: ...actually, refactor ALL shit related to dynamic ports >.<
            Action dlg = () =>
            {
                RemovePort(view);
                var edges = new HashSet<Edge>(view.ConnectedEdges);
                foreach (var edge in edges)
                {
                    NSelectionDeleter.DeleteEdge(_graphView, _graphView.Graph.RuntimeGraph, edge);
                }

                _deleteButtonsContainer.Remove(newPortButton);
            };
            newPortButton.clicked += dlg;
            _deleteButtonsContainer.Add(newPortButton);
        }

        private void RemovePortView(NDynamicPortView view)
        {
            Ports.Remove(view);
            _portBodysContainer.Remove(view);
        }
    }
}