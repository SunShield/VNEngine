using OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes;

namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Ports
{
    public interface IOerPort
    {
        int Id { get; }
        int NodeId { get; }
        OerPortType Type { get; }
        
        string Name { get; }
        void Initialize(int id, OerPortType type, string name, int nodeId);
        void InitializeRuntime(OerGraph graph, OerNode ownerNode, IOerPort valueRetrievingPort, OerNode valueRetrievingNode);
        IOerPort CreateOwnNonInitializedCopy();
    }
}