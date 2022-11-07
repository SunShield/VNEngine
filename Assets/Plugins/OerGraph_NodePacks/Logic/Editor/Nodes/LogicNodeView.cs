using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs;
using OerGraph.Editor.Graphs.Elements.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace OerGraph_NodePacks.Logic.Editor.Nodes
{
    public class LogicNodeView : OerNodeView
    {
        public LogicNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            titleContainer.style.height = 16f;
            titleContainer.style.backgroundColor = new Color(0.1f, 0.4f, 0.3f);
        }

        protected override void OnPortAdded(OerPortView port)
        {
            if (port.direction == Direction.Input) return;
            
            port.SetBackingFieldHidden(true);
        }
    }
}