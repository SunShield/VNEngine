using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;

namespace VNEngine.Runtime.Core.Graphs.Attributes.PortFields
{
    public class NInputAttribute : NPortAttribute
    {
        public override NPortType Type => NPortType.Input;
        
        public NInputAttribute()
        { }
    }
}