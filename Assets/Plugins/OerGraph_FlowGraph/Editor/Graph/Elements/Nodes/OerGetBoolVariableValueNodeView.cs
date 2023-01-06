using System.Collections.Generic;
using System.Linq;
using OerGraph.Editor.Graphs;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Nodes
{
    public class OerGetBoolVariableValueNodeView : OerGetVariableValueNodeView
    {
        public OerGetBoolVariableValueNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            titleContainer.style.backgroundColor = new Color(0.6f, 0.6f, 0.2f, 0.4f);
        }
        
        protected override List<string> GetVariableNames() => Graph.BoolVariables.Keys.ToList();
    }
}