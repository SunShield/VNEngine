using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using VNEngine.Editor.Graphs.Elements.Edges;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    /// <summary>
    /// Editor port class
    /// </summary>
    public class NPortView : Port
    {
        private VisualEffect _backingField;
        
        public INPort Port { get; private set; }
        
        public NPortView(NGraphView graphView, INPort runtiumePort, string name, NPortType portType, Type type, Capacity portCapacity = Capacity.Single) 
            : base(Orientation.Horizontal, portType == NPortType.Input ? Direction.Input : Direction.Output, portCapacity, type)
        {
            Port = runtiumePort;
            portName = name;

            m_EdgeConnector = new EdgeConnector<Edge>(new NEdgeConnectorListener(graphView));
            this.AddManipulator(m_EdgeConnector);
        }

        public bool IsCompatibleWith(NPortView portView) => Port.IsCompatibleWith(portView.Port);

        public override void Connect(Edge edge)
        {
            base.Connect(edge);
            var b = 1;
        }
    }
}