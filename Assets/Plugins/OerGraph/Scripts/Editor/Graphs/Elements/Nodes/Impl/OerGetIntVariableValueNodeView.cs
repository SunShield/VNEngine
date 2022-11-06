using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OerGraph.Editor.Graphs.Elements.Nodes.Impl
{
    public class OerGetIntVariableValueNodeView : OerGetVariableValueNodeView
    {
        public OerGetIntVariableValueNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            titleContainer.style.backgroundColor = new Color(1f, 0.6f, 0.4f, 0.4f);
        }
        
        protected override List<string> GetVariableNames() => View.Graph.IntVariables.Keys.ToList();
    }
}