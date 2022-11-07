using System;
using System.Collections.Generic;
using System.Reflection;
using OerGraph.Editor.Graphs;
using OerGraph.Editor.Graphs.Elements.EdgeConnectors;
using OerGraph.Editor.Graphs.Factories;
using OerGraph.Editor.Graphs.Systems.PortCompatibility;
using OerGraph.Editor.Graphs.Systems.Styling;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace OerGrap.Editor.Graphs.Elements.Ports
{
    public class OerPortView : Port
    {
        private VisualElement _backingField;
        private DisplayStyle _backingFieldDisplayStyle;
        
        public int RuntimePortId { get; private set; }
        public OerGraphView View { get; private set; }
        public HashSet<Edge> ConnectedEdges { get; } = new();

        private OerMainGraph Graph => View.Graph;
        public IOerPort RuntimePort => Graph.GetPort(RuntimePortId);
        public string Name => RuntimePort.Name;

        public float ConnectorTextWidth
        {
            get => m_ConnectorText.worldBound.width;
            set => m_ConnectorText.style.width = value;
        }
        
        public OerPortView(OerGraphView view, int runtimePortId, Direction portDirection, Type type) 
            : base(Orientation.Horizontal, portDirection, Capacity.Single, type)
        {
            RuntimePortId = runtimePortId;
            View = view;

            AddEdgeConnector();
            AddToClassList(OerViewConsts.ViewClasses.OerPortView);
            AddDefaultStyleSheet();
            ConstructBackingFieldElement(view);
            SetPortName(view, runtimePortId);
            
            style.height = 16f;
        }

        private void AddEdgeConnector()
        {
            m_EdgeConnector = new EdgeConnector<Edge>(new OerEdgeConnectorListener(View));
            this.AddManipulator(m_EdgeConnector);
        }
        
        private void AddDefaultStyleSheet() => OerStyleSheetResourceLoader.TryAddStyleSheetFromPath(OerViewConsts.StyleSheets.DefaultPortStyleSheetPath, this);

        private void ConstructBackingFieldElement(OerGraphView view)
        {
            var type = RuntimePort.GetType();
            var fieldInfo = type.GetField("DefaultValue", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            _backingField = OerFieldsFactory.CreateControl(fieldInfo, this);
            
            // If we're unable to create a backing field, just do nothing for now
            if (_backingField != null)
            {
                _backingField.AddToClassList("backingField");
                m_ConnectorBox.parent.Add(_backingField);                
            }
        }

        private void SetPortName(OerGraphView view, int runtimePortId)
        {
            contentContainer.Q<Label>().text = view.Graph.GetPort(runtimePortId).Name;
        }

        public bool IsCompatibleWith(OerPortView portView) =>
            OerPortCompatibilityChecker.CheckPortsCompatible(this, portView);

        public void ConnectToEdge(Edge edge)
        {
            Connect(edge);
            ConnectedEdges.Add(edge);
            UpdateBackingFieldVisibility();
        }

        public void UpdateBackingFieldVisibility()
        {
            if (_backingField == null) return;
            _backingField.visible = ConnectedEdges.Count == 0;
        }

        public void SetBackingFieldHidden(bool hidden)
        {
            if (_backingField == null) return;
            
            if (hidden)
            {
                _backingFieldDisplayStyle = _backingField.style.display.value;
                _backingField.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            }
            else
            {
                _backingField.style.display = _backingFieldDisplayStyle;
            }
        }
    }
}