using System.Collections.Generic;
using System.Linq;
using OerGraph.Editor.Graphs;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Nodes
{
    public class OerSetFloatVariableValueNodeView : OerSetVariableValueNodeView
    {
        public OerSetFloatVariableValueNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            titleContainer.style.backgroundColor = new Color(1f, 0.2f, 0.2f, 0.4f);
        }

        protected override List<string> GetVariableNames() => Graph.FloatVariables.Keys.ToList();
    }
}