using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Elements.Edges;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Editor.Graphs.Systems.Styling;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    /// <summary>
    /// Editor port class
    /// </summary>
    public class NPortView : Port
    {
        private VisualElement _backingField;
        
        public NGraphView GraphView { get; }
        public INPort RuntimePort { get; }
        public HashSet<Edge> ConnectedEdges { get; } = new();

        public NPortView(NGraphView graphView, string fieldName, INPort runtimePort, NPortType portType, Type type, Capacity portCapacity = Capacity.Single) 
            : base(Orientation.Horizontal, portType == NPortType.Input ? Direction.Input : Direction.Output, portCapacity, type)
        {
            GraphView = graphView;
            RuntimePort = runtimePort;
            portName = fieldName;

            AddEdgeConnector();
            AddToClassList("n-portView");
            AddDefaultStyleSheet();

            if (!RuntimePort.HasBackingField) return;

            ConstructBackingFieldElement();
        }

        private void AddEdgeConnector()
        {
            m_EdgeConnector = new EdgeConnector<Edge>(new NEdgeConnectorListener(GraphView));
            this.AddManipulator(m_EdgeConnector);
        }
        
        private void AddDefaultStyleSheet() => NStyleSheetResourceLoader.TryAddStyleSheetFromPath(NViewConstants.DefaultPortStyleSheetPath, this);

        private void ConstructBackingFieldElement()
        {
            var type = RuntimePort.GetType();
            var fieldInfo = type.GetField("BackingValue", BindingFlags.NonPublic | BindingFlags.Instance);
            _backingField = NFieldsFactory.CreateControl(fieldInfo, this);
            _backingField.AddToClassList("backingField");
            m_ConnectorBox.parent.Add(_backingField);
        }

        public bool IsCompatibleWith(NPortView portView) => RuntimePort.IsCompatibleWith(portView.RuntimePort);

        public void ConnectToEdge(Edge edge)
        {
            Connect(edge);
            ConnectedEdges.Add(edge);
            UpdateBackingFieldVisibility();
        }

        public void UpdateBackingFieldVisibility()
        {
            _backingField.visible = ConnectedEdges.Count == 0;
        }
    }
}