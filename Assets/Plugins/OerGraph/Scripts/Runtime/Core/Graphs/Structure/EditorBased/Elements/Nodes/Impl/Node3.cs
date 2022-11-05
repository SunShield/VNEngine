using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl
{
    public class Node3 : OerNode
    {
        public override string Name { get; } = "Node3"; 

        public override object GetValue(string portName)
        {
            return 0;
        }

        protected override OerNode CreateInstance() => new Node3();

        public override OerNodePortsData GetPortsData()
        {
            return new(new () 
            {
                ("Int", "Number3", OerPortType.Input, false),
                ("Int", "Out3", OerPortType.Output, false),
            });
        }
    }
}