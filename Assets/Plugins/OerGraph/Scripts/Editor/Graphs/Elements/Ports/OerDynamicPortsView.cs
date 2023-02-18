using System;
using System.Collections.Generic;
using OerGraph.Editor.Graphs;
using OerGraph.Editor.Service.Utilities;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGrap.Editor.Graphs.Elements.Ports
{
    public class OerDynamicPortsView : GraphElement
    {
        private VisualElement _portsContainer;
        private VisualElement _headerContainer;
        
        private OerGraphView _graphView;
        private List<int> _runtimePortIds;
        private OerPortType _portType;
        
        public List<OerDynamicPortView> Ports = new();
        public OerPortType Type => _portType;

        public OerDynamicPortsView(OerGraphView graphView, string name, List<int> runtimePortIds, OerPortType portType)
        {
            _graphView = graphView;
            _runtimePortIds = runtimePortIds;
            _portType = portType;

            AddToClassList(OerViewConsts.ViewClasses.OerDynPortView);
            ConfigureStyle();
            ConfigureElementGeometry();
            AddHeaderLabel(name);
            AddAddNewPortButton();
        }

        private void ConfigureStyle()
        {
            style.backgroundColor = new StyleColor(new Color(0.4f, 0.4f, 0.4f));
            style.marginRight = 2f;
            style.marginLeft = 2f;
            style.marginTop = 3f;
            style.marginBottom = 1f;
            style.paddingRight = 0f;
            style.paddingLeft = 0f;
            style.borderLeftWidth = 0f;
            style.borderRightWidth = 0f;
        }

        private void ConfigureElementGeometry()
        {
            _headerContainer = new VisualElement();
            _headerContainer.style.flexDirection = FlexDirection.Row;
            _headerContainer.style.paddingLeft = 8f;
            _headerContainer.style.marginBottom = 2f;
            Add(_headerContainer);

            var divider = new VisualElement();
            divider.style.height = 0.01f;
            divider.style.backgroundColor = new StyleColor(Color.black);
            divider.style.borderBottomWidth = 1;
            divider.style.marginBottom = 3;
            divider.style.marginTop = 0f;
            Add(divider);
            
            _portsContainer = new VisualElement();
            _portsContainer.style.flexDirection = FlexDirection.Column;
            Add(_portsContainer);
        }

        private void AddHeaderLabel(string name)
        {
            var headerLabel = new Label();
            headerLabel.text = name;
            headerLabel.style.fontSize = 14f;
            headerLabel.style.alignSelf = new StyleEnum<Align>(Align.Center);
            _headerContainer.Add(headerLabel);
        }

        private void AddAddNewPortButton()
        {
            var newPortButton = OerUiElementsUtility.CreateButton("+", OnAddPortClicked);
            newPortButton.style.width = 20f;
            newPortButton.style.height = 20f;
            newPortButton.style.marginTop = 0f;
            newPortButton.style.marginBottom = 0f;
            newPortButton.style.marginLeft = 3f;
            newPortButton.style.paddingLeft = 0f;
            newPortButton.style.paddingBottom = 0f;
            newPortButton.style.paddingRight = 0f;
            newPortButton.style.paddingTop = 0f;
            newPortButton.style.width = 14;
            newPortButton.style.height = 14;
            newPortButton.style.alignSelf = new StyleEnum<Align>(Align.Center);
            _headerContainer.Add(newPortButton);
        }

        private void OnAddPortClicked() => onAddPortClick?.Invoke();

        public void AddPortView(OerDynamicPortView dynamicPortView, Action onDeleteButtonClick)
        {
            AddPortViewInternal(dynamicPortView, onDeleteButtonClick);
            
            EditorUtility.SetDirty(_graphView.Asset);
        }

        private void AddPortViewInternal(OerDynamicPortView view, Action onDeleteButtonClick)
        {
            Ports.Add(view);
            _portsContainer.Add(view);

            var deletePortButton = new Button() { text = "X" };
            deletePortButton.style.width = 16f;

            deletePortButton.clicked += onDeleteButtonClick;
            
            if (_portType == OerPortType.Input)
                view.Add(deletePortButton);
            else
                view.Insert(1, deletePortButton);
            
            AdjustPortLabelSizes();
        }

        public void RemovePortView(OerDynamicPortView view)
        {
            Ports.Remove(view);
            _portsContainer.Remove(view);
            AdjustPortLabelSizes();
        }

        private void AdjustPortLabelSizes()
        {
            if (Ports.Count == 0) return;

            var maxLength = Ports[0].ConnectorTextWidth;

            if (maxLength != maxLength) // this is true if maxLenght is NaN
            {
                this.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
                return;
            }
            
            foreach (var port in Ports)
            {
                if (port.ConnectorTextWidth > maxLength)
                    maxLength = port.ConnectorTextWidth;
            }
            
            foreach (var port in Ports)
            {
                port.ConnectorTextWidth = maxLength;
            }
        }

        /// <summary>
        /// This is called through callback of this element.
        /// This callback is being invoked at the moment geometry of an element is recalculated and all the world sizes are correct
        /// </summary>
        /// <param name="evt"></param>
        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            AdjustPortLabelSizes();
        }

        public event Action onAddPortClick;
    }
}