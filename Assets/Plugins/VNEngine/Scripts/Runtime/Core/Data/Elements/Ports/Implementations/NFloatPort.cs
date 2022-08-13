using System;

namespace VNEngine.Runtime.Core.Data.Elements.Ports.Implementations
{
    [Serializable]
    public class NFloatPort : NPort<float>
    {
        public NFloatPort(int id) : base(id)
        {
        }
    }
}