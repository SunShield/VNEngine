using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VNEngine.Editor.Service.Utilities;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    public class NDynamicPortsView : GraphElement
    {
        private VisualElement _portsContainer;
        private VisualElement _deleteButtonsContainer;
        private VisualElement _portBodysContainer;
        
        private NGraphView _graphView;
        private IList _runtimePorts;
        private NPortType _portType;
        
        public List<NPortView> Ports = new();
        public NPortType Type => _portType;

        public NDynamicPortsView(NGraphView graphView, IList runtimePorts, NPortType portType)
        {
            _graphView = graphView;
            _runtimePorts = runtimePorts;
            _portType = portType;

            AddToClassList("dynamicPortsView");
            ConfigureStyle();
            AddAddNewPortButton();
            ConfigureElementGeometry(portType);
        }

        private void ConfigureStyle()
        {
            style.marginRight = 0f;
            style.marginLeft = 0f;
            style.paddingRight = 0f;
            style.paddingLeft = 0f;
            style.borderLeftWidth = 0f;
            style.borderRightWidth = 0f;
        }

        private void AddAddNewPortButton()
        {
            var newPortButton = NUiElementsUtility.CreateButton("New", OnAddPortClicked);
            Add(newPortButton);
        }

        private void ConfigureElementGeometry(NPortType portType)
        {
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

        private void OnAddPortClicked() => onAddPortClick?.Invoke();

        public void AddPortView(NDynamicPortView dynamicPortView, Action onDeleteButtonClick)
        {
            AddPortViewInternal(dynamicPortView, onDeleteButtonClick);
            
            EditorUtility.SetDirty(_graphView.Graph);
        }

        private void AddPortViewInternal(NDynamicPortView view, Action onDeleteButtonClick)
        {
            Ports.Add(view);
            _portBodysContainer.Add(view);

            var newPortButton = new Button() { text = "X" };

            newPortButton.clicked += () =>
            {
                onDeleteButtonClick();
                _deleteButtonsContainer.Remove(newPortButton);
            };
            _deleteButtonsContainer.Add(newPortButton);
            
            SetPortNames();
        }

        public void RemovePortView(NDynamicPortView view)
        {
            Ports.Remove(view);
            _portBodysContainer.Remove(view);
            SetPortNames();
        }

        private void SetPortNames()
        {
            for (int i = 0; i < _runtimePorts.Count; i++)
            {
                var port = _runtimePorts[i] as INPort;
                port.SetIndex(i);
            }
        }

        public event Action onAddPortClick;
    }
}