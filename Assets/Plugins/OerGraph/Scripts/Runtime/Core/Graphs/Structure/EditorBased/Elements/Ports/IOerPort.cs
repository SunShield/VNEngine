using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports
{
    public interface IOerPort
    {
        int Id { get; }
        int NodeId { get; }
        OerPortType Type { get; }
        string Name { get; }
        
        void Initialize(int id, OerPortType type, string name, int nodeId);
        void InitializeRuntime(OerMainGraph mainGraph, OerNode ownerNode, IOerPort valueRetrievingPort, OerNode valueRetrievingNode);
        IOerPort CreateOwnNonInitializedCopy();
        Type GetUnderlyingType();
    }
}