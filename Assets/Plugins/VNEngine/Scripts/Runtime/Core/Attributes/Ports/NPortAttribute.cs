using System;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Scripts.Runtime.Core.Attributes.Ports
{
    public abstract class NPortAttribute : Attribute
    {
        public abstract NPortType Type { get; }
    }
}