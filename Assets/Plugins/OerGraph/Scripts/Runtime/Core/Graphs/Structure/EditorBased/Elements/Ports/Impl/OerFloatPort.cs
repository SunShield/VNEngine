namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class OerFloatPort : OerPort<float>
    {
        protected override OerPort<float> CreateInstance() => new OerFloatPort();
    }
}