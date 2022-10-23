namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Ports.Impl
{
    public class FloatOerPort : OerPort<float>
    {
        protected override OerPort<float> CreateInstance() => new FloatOerPort();
    }
}