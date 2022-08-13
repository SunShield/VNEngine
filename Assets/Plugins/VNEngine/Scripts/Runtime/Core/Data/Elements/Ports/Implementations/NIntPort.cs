using System;
using UnityEngine.UIElements;

namespace VNEngine.Runtime.Core.Data.Elements.Ports.Implementations
{
    [Serializable]
    [NPortParams(portClassName: "portView-intPort", portStylesheetPath: "Styles/Ports/NIntPortStyleSheet")]
    public class NIntPort : NPort<int>
    {
        public NIntPort(int id) : base(id) { }
    }
}