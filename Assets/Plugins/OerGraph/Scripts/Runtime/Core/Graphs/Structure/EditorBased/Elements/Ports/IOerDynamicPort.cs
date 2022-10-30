using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports
{
    public interface IOerDynamicPort
    {
        int Id { get; }
        int NodeId { get; }
        OerPortType Type { get; }
        string Name { get; }
        string PortKey { get; }
        List<int> PortIds { get; }

        void Initialize(int id, OerPortType type, string name, int nodeId);
        void InitializeRuntime(OerMainGraph mainGraph, OerNode ownerNode, List<IOerPort> ports);
        IOerDynamicPort CreateOwnNonInitializedCopy();
        void RegisterPort(int id);
        void UnregisterPort(int id);
    }
}