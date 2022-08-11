using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Scripts.Runtime.Core.Attributes.Ports
{
    public class NOutputAttribute : NPortAttribute
    {
        public override NPortType Type => NPortType.Output;
        
        public NOutputAttribute()
        { }
    }
}