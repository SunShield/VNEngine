using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl
{
    public class Node3 : OerNode
    {
        public override object GetValue(string portName)
        {
            return 0;
        }

        protected override OerNode CreateInstance() => new Node3();

        public override OerNodePortsData GetPortsData()
        {
            return new(new () 
            {
                ("Int", "Number2", OerPortType.Input, false),
                ("Int", "Out1", OerPortType.Output, false),
            });
        }
    }
}