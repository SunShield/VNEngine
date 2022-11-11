using OerGraph.Editor.Graphs;
using OerGraph.Editor.Graphs.Elements.Nodes;
using OerGraph_FlowGraph.Runtime.Graphs;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Nodes
{
    public class OerResolvableGraphNodeView : OerNodeView
    {
        public OerResolvableGraph Graph => View.Graph as OerResolvableGraph;
        
        public OerResolvableGraphNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
        }
    }
}