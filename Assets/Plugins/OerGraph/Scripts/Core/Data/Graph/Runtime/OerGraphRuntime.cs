namespace OerGraph.Core.Data.Graph
{
    public partial class OerGraph
    {
        public void InitializeRuntime() => OerGraphRuntimeInitializer.InitializeGraphRuntime(this);
        public OerGraph Copy() => OerGraphRintimeDuplicator.DuplicateGraph(this);
    }
}