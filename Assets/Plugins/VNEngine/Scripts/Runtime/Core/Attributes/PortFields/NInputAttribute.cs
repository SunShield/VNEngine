using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.PortFields
{
    public class NInputAttribute : NPortAttribute
    {
        public override NPortType Type => NPortType.Input;
        
        public NInputAttribute()
        { }
    }
}