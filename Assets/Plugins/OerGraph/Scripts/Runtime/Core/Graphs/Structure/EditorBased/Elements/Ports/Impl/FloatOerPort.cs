namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class FloatOerPort : OerPort<float>
    {
        protected override OerPort<float> CreateInstance() => new FloatOerPort();
    }
}