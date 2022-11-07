using OerGraph.Editor.Graphs;
using OerGraph.Editor.Graphs.Elements.Nodes;
using UnityEngine;

namespace OerGraph_NodePacks.Math.Editor.Graphs.Nodes
{
    public class MathNodeView : OerNodeView
    {
        public MathNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            titleContainer.style.height = 16f;
            titleContainer.style.backgroundColor = new Color(0.2f, 0.4f, 0.4f);
        }
    }
}