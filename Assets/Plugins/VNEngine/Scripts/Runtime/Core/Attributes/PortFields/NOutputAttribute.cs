using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.PortFields
{
    public class NOutputAttribute : NPortAttribute
    {
        public override NPortType Type => NPortType.Output;
        
        public NOutputAttribute()
        { }
    }
}