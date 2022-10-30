using System;
using System.Collections.Generic;
using OerGraph.Editor.Graphs;
using OerGraph.Editor.Service.Utilities;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace OerGrap.Editor.Graphs.Elements.Ports
{
    public class OerDynamicPortsView : GraphElement
    {
        public Direction Direction { get; private set; }
        
        private VisualElement _portsContainer;
        private VisualElement _deleteButtonsContainer;
        private VisualElement _portBodysContainer;
        
        private OerGraphView _graphView;
        private List<int> _runtimePortIds;
        private OerPortType _portType;
        
        public List<OerPortView> Ports = new();
        public OerPortType Type => _portType;

        public OerDynamicPortsView(OerGraphView graphView, List<int> runtimePortIds, OerPortType portType)
        {
            _graphView = graphView;
            _runtimePortIds = runtimePortIds;
            _portType = portType;

            AddToClassList(OerViewConsts.ViewClasses.OerDynPortView);
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
            var newPortButton = OerUiElementsUtility.CreateButton("New", OnAddPortClicked);
            Add(newPortButton);
        }

        private void ConfigureElementGeometry(OerPortType portType)
        {
            _portsContainer = new VisualElement();
            _portsContainer.style.flexDirection = FlexDirection.Row;
            Add(_portsContainer);

            _deleteButtonsContainer = new VisualElement();
            _portBodysContainer = new VisualElement();

            if (portType == OerPortType.Input)
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

        public void AddPortView(OerPortView dynamicPortView, Action onDeleteButtonClick)
        {
            AddPortViewInternal(dynamicPortView, onDeleteButtonClick);
            
            EditorUtility.SetDirty(_graphView.GraphAsset);
        }

        private void AddPortViewInternal(OerPortView view, Action onDeleteButtonClick)
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

        public void RemovePortView(OerPortView view)
        {
            Ports.Remove(view);
            _portBodysContainer.Remove(view);
            SetPortNames();
        }

        private void SetPortNames()
        {
            for (int i = 0; i < _runtimePortIds.Count; i++)
            {
                //var port = _runtimePortIds[i] as IOerPort;
            }
        }

        public event Action onAddPortClick;
    }
}