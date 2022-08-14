using System;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;

namespace VNEngine.Runtime.Core.Graphs.Attributes.PortFields
{
    public abstract class NPortAttribute : Attribute
    {
        public abstract NPortType Type { get; }
    }
}