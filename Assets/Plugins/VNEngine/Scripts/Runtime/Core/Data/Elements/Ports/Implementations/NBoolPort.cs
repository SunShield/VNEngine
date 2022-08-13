using System;

namespace VNEngine.Runtime.Core.Data.Elements.Ports.Implementations
{
    [Serializable]
    public class NBoolPort : NPort<bool>
    {
        public NBoolPort(int id) : base(id)
        {
        }
    }
}