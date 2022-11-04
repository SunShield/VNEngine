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
        private VisualElement _portsContainer;
        private VisualElement _portBodysContainer;
        
        private OerGraphView _graphView;
        private List<int> _runtimePortIds;
        private OerPortType _portType;
        
        public List<OerDynamicPortView> Ports = new();
        public OerPortType Type => _portType;

        public OerDynamicPortsView(OerGraphView graphView, List<int> runtimePortIds, OerPortType portType)
        {
            _graphView = graphView;
            _runtimePortIds = runtimePortIds;
            _portType = portType;

            AddToClassList(OerViewConsts.ViewClasses.OerDynPortView);
            ConfigureStyle();
            AddAddNewPortButton();
            ConfigureElementGeometry();
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

        private void ConfigureElementGeometry()
        {
            _portsContainer = new VisualElement();
            _portsContainer.style.flexDirection = FlexDirection.Row;
            Add(_portsContainer);

            _portBodysContainer = new VisualElement();
            _portsContainer.Add(_portBodysContainer);
        }

        private void OnAddPortClicked() => onAddPortClick?.Invoke();

        public void AddPortView(OerDynamicPortView dynamicPortView, Action onDeleteButtonClick)
        {
            AddPortViewInternal(dynamicPortView, onDeleteButtonClick);
            
            EditorUtility.SetDirty(_graphView.GraphAsset);
        }

        private void AddPortViewInternal(OerDynamicPortView view, Action onDeleteButtonClick)
        {
            Ports.Add(view);
            _portBodysContainer.Add(view);

            var newPortButton = new Button() { text = "X" };

            newPortButton.clicked += onDeleteButtonClick;
            
            if (_portType == OerPortType.Input)
                view.Add(newPortButton);
            else
                view.Insert(0, newPortButton);
            
            AdjustPortLabelSizes();
        }

        public void RemovePortView(OerDynamicPortView view)
        {
            Ports.Remove(view);
            _portBodysContainer.Remove(view);
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

            this.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        /// <summary>
        /// This is called through callback of this element.
        /// This callback is being invoked at the moment geometry of an element is recalculated and all the world sizes are correct
        /// </summary>
        /// <param name="evt"></param>
        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            AdjustPortLabelSizes();
        }

        public event Action onAddPortClick;
    }
}