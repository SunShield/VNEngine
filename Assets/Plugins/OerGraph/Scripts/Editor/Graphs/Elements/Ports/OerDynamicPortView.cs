using System;
using OerGraph.Editor.Graphs;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGrap.Editor.Graphs.Elements.Ports
{
    public class OerDynamicPortView : OerPortView
    {
        public OerDynamicPortView(OerGraphView view, int runtimePortId, Direction portDirection, Type type) : base(view, runtimePortId, portDirection, type)
        {
            if (portDirection == Direction.Output)
                m_ConnectorText.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
            m_ConnectorText.text = "";
            style.height = 16f;
        }
    }
}