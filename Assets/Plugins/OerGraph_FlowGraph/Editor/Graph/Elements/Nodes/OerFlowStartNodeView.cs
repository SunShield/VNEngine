using OerGraph.Editor.Graphs;
using OerGraph.Editor.Graphs.Elements.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Nodes
{
    public class OerFlowStartNodeView : OerNodeView
    {
        public OerFlowStartNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            titleContainer.style.height = 22f;
            var nodeBorder = this.Q("node-border", (string) null);
            nodeBorder.style.borderLeftColor = new Color(32/255f, 217/255f, 45/255f);
            nodeBorder.style.borderTopColor = new Color(32/255f, 217/255f, 45/255f);
            nodeBorder.style.borderRightColor = new Color(32/255f, 217/255f, 45/255f);
            nodeBorder.style.borderBottomColor = new Color(32/255f, 217/255f, 45/255f);
            nodeBorder.style.borderTopWidth = 2f;
            nodeBorder.style.borderLeftWidth = 2f;
            nodeBorder.style.borderRightWidth = 2f;
            nodeBorder.style.borderBottomWidth = 2f;
            
            inputContainer.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
    }
}