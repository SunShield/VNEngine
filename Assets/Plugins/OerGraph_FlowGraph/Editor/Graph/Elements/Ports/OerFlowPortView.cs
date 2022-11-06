using System;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs;
using UnityEditor.Experimental.GraphView;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Ports
{
    public class OerFlowPortView : OerPortView
    {
        public OerFlowPortView(OerGraphView view, int runtimePortId, Direction portDirection, Type type) : base(view, runtimePortId, portDirection, type)
        {
            m_ConnectorBox.style.borderTopLeftRadius = 0f;
            m_ConnectorBox.style.borderBottomLeftRadius = 0f;
            m_ConnectorBox.style.borderTopWidth = 2f;
            m_ConnectorBox.style.borderLeftWidth = 2f;
            m_ConnectorBox.style.borderBottomWidth = 2f;
            m_ConnectorBox.style.borderRightWidth = 2f;
        }
    }
}