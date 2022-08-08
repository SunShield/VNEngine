using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Elements.Edges;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    /// <summary>
    /// Editor port class
    /// </summary>
    public class NPortView : Port
    {
        private VisualElement _backingField;
        
        public INPort RuntimePort { get; private set; }
        
        public NPortView(NGraphView graphView, string fieldName, INPort runtiumeRuntimePort, NPortType portType, Type type, Capacity portCapacity = Capacity.Single) 
            : base(Orientation.Horizontal, portType == NPortType.Input ? Direction.Input : Direction.Output, portCapacity, type)
        {
            RuntimePort = runtiumeRuntimePort;
            portName = fieldName;

            m_EdgeConnector = new EdgeConnector<Edge>(new NEdgeConnectorListener(graphView));
            this.AddManipulator(m_EdgeConnector);
        }

        public bool IsCompatibleWith(NPortView portView) => RuntimePort.IsCompatibleWith(portView.RuntimePort);
    }
}