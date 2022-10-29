namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased
{
    public partial class OerMainGraph
    {
        public void InitializeRuntime() => OerGraphRuntimeInitializer.InitializeGraphRuntime(this);
        public OerMainGraph Copy() => OerGraphRintimeDuplicator.DuplicateGraph(this);
    }
}