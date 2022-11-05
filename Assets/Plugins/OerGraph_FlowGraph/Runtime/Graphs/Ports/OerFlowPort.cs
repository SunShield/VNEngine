using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes;
using OerGraph_FlowGraph.Runtime.Graphs.Ports;

namespace Plugins.OerGraph_FlowGraph.Runtime.Graphs.Ports
{
    public class OerFlowPort : OerPort<Flow>
    {
        protected override OerPort<Flow> CreateInstance() => new OerFlowPort();
        
        /// <summary>
        /// Runtime-only field. Contains a flow node connected to this port.
        /// Unlike regular OerPorts, Ouput Flow Ports can connect to exactly ONE Input Flow port (but not vice versa)
        ///
        /// This can be null - this means, this is a last node to execute on this path
        /// </summary>
        public OerFlowNode NextNode { get; private set; }

        public void InitializeFlowPortRuntime(OerFlowNode nextNode)
        {
            NextNode = nextNode;
        }
    }
}