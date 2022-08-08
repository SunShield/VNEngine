using System;

namespace VNEngine.Runtime.Core.Data.Elements.Ports.Implementations
{
    [Serializable]
    public class NIntPort : NPort<int>
    {
        public NIntPort(int id) : base(id)
        {
        }
    }
}