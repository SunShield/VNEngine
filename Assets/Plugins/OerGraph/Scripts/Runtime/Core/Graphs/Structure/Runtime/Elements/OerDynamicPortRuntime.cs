using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports
{
    public abstract partial class OerDynamicPort<TType>
    {
        /// <summary>
        /// Master graph of the port. Runtime-only field
        /// </summary>
        public OerMainGraph MainGraph { get; private set; }
        
        /// <summary>
        /// Node port belongs to. Runtime-only field
        /// </summary>
        public OerNode Node { get; private set; }

        public List<OerPort<TType>> RuntimePorts { get; private set; } = new();
        
        public void InitializeRuntime(OerMainGraph mainGraph, OerNode ownerNode, List<IOerPort> ports)
        {
            MainGraph = mainGraph;
            Node = ownerNode;

            foreach (var port in ports)
            {
                RuntimePorts.Add(port as OerPort<TType>);
            }
        }

        public IOerDynamicPort CreateOwnNonInitializedCopy()
        {
            var copy = CreateInstance();
            copy.id = id;
            copy.type = type;
            copy.name = name;
            copy.nodeId = nodeId;
            copy.portIds = new List<int>(portIds);
            return copy;
        }

        protected abstract OerDynamicPort<TType> CreateInstance();
    }
}