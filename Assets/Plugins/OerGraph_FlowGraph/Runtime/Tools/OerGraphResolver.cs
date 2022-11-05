using OerGraph_FlowGraph.Runtime.Graphs;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes;

namespace OerGraph_FlowGraph.Runtime.Tools
{
    public class OerGraphResolver
    {
        private OerFlowNode _currentResolvingNode;

        public void ResolveGraph(OerResolvableGraph graph, string startingNodeName)
        {
            _currentResolvingNode = graph.StartingNodes[startingNodeName];

            while (_currentResolvingNode != null)
            {
                var resolveResult = _currentResolvingNode.Resolve();
                _currentResolvingNode = _currentResolvingNode.GetNextNode(resolveResult);
            }
        }
    }
}