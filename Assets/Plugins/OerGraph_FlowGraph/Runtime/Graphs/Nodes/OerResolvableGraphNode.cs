using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes
{
    public abstract class OerResolvableGraphNode : OerNode
    {
        public new OerResolvableGraph MainGraph => base.MainGraph as OerResolvableGraph;
    }
}