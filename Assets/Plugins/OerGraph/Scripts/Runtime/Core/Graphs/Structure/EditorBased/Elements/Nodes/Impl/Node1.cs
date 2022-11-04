using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl
{
    public class Node1 : OerNode
    {
        public override object GetValue(string portName)
        {
            return 0;
        }

        protected override OerNode CreateInstance() => new Node1();

        public override OerNodePortsData GetPortsData()
        {
            return new(new () 
            {
                ("Int",    "Number0", OerPortType.Input,  false),
                ("Float",  "Number1", OerPortType.Input,  true),
                ("Int",    "Out1",    OerPortType.Output, false),
                ("String", "OutDyn",  OerPortType.Output, true),
                ("String", "OutDyn2", OerPortType.Output, true),
                ("Int",    "Out1",    OerPortType.Output, false),
                ("Float",  "Out2",    OerPortType.Output, false),
                ("String", "Out3",    OerPortType.Output, false),
            });
        }
    }
}