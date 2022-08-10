using System;
using System.Collections.Generic;
using VNEngine.Runtime.Core.Data.Elements.Ports.Implementations;

namespace VNEngine.Runtime.Core.Data.Elements.Ports
{
    /// <summary>
    /// The only reason of this bullshit existing is Unity unable to serialize fields like Port<int> event with SerializedReference
    /// </summary>
    public static class PortByTypeFactory
    {
        public static Dictionary<Type, Func<int, INPort>> PortTypesMatrix = new()
        {
            { typeof(int), id => new NIntPort(id) }
        };

        public static INPort CreatePort(Type type, int id) => PortTypesMatrix[type](id);
    }
}