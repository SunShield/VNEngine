using System;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;

namespace VNEngine.Runtime.Core.Graphs.Data.Elements.Ports.Implementations
{
    [Serializable]
    public class NBoolPort : NPort<bool>
    {
        public NBoolPort(int id) : base(id)
        {
        }
    }
}