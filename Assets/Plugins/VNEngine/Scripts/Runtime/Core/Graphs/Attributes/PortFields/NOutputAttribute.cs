using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;

namespace VNEngine.Runtime.Core.Graphs.Attributes.PortFields
{
    public class NOutputAttribute : NPortAttribute
    {
        public override NPortType Type => NPortType.Output;
        
        public NOutputAttribute()
        { }
    }
}