using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Elements.Edges;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    /// <summary>
    /// Editor port class
    /// </summary>
    public class NPortView : Port
    {
        private VisualElement _backingField;
        
        public NGraphView GraphView { get; private set; }
        public INPort RuntimePort { get; private set; }
        
        public NPortView(NGraphView graphView, string fieldName, INPort runtimePort, NPortType portType, Type type, Capacity portCapacity = Capacity.Single) 
            : base(Orientation.Horizontal, portType == NPortType.Input ? Direction.Input : Direction.Output, portCapacity, type)
        {
            GraphView = graphView;
            RuntimePort = runtimePort;
            portName = fieldName;

            m_EdgeConnector = new EdgeConnector<Edge>(new NEdgeConnectorListener(GraphView));
            this.AddManipulator(m_EdgeConnector);

            if (!RuntimePort.HasBackingField) return;

            ConstructBackingFieldElement();
        }

        private void ConstructBackingFieldElement()
        {
            var type = RuntimePort.GetType();
            var fieldInfo = type.GetField("BackingValue", BindingFlags.NonPublic | BindingFlags.Instance);
            _backingField = FieldElementsFactory.CreateControl(fieldInfo, this);
            m_ConnectorBox.parent.Add(_backingField);
        }

        public bool IsCompatibleWith(NPortView portView) => RuntimePort.IsCompatibleWith(portView.RuntimePort);
    }
}