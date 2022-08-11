using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Scripts.Runtime.Core.Attributes.Ports
{
    public class NInputAttribute : NPortAttribute
    {
        public override NPortType Type => NPortType.Input;
        
        public NInputAttribute()
        { }
    }
}