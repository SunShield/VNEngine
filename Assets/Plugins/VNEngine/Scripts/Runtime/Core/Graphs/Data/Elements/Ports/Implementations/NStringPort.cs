using System;

namespace VNEngine.Runtime.Core.Graphs.Data.Elements.Ports.Implementations
{
    [Serializable]
    public class NStringPort : NPort<string>
    {
        public NStringPort(int id) : base(id)
        {
        }
    }
}