using System;
using VNEngine.Runtime.Core.Graphs.Attributes.Ports;

namespace VNEngine.Runtime.Core.Graphs.Data.Elements.Ports.Implementations
{
    [Serializable]
    [NPortParams(portClassName: "portView-intPort", portStylesheetPath: "Styles/Ports/NIntPortStyleSheet")]
    public class NIntPort : NPort<int>
    {
        public NIntPort(int id) : base(id) { }
    }
}