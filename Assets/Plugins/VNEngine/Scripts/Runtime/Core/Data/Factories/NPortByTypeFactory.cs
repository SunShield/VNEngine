using System;
using System.Collections.Generic;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Runtime.Core.Data.Elements.Ports.Implementations;

namespace VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Factories
{
    /// <summary>
    /// The only reason of this bullshit existing is Unity unable to serialize fields like Port<int> event with SerializedReference
    /// So we need to have a special implementation class for each port type: like NIntPort : Port<int> to get these serialized
    /// And when we need to create ports for dynamic ports, we need some matrix of port type (got through reflection) to exact port class for that type
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