﻿using System;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.PortFields
{
    public abstract class NPortAttribute : Attribute
    {
        public abstract NPortType Type { get; }
    }
}