using System.Collections.Generic;
using System.Linq;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Ports;

namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes
{
    public abstract partial class OerNode
    {
        /// <summary>
        /// Master graph of the node. Runtime-only field.
        /// </summary>
        protected OerGraph Graph { get; private set; }

        // <summary>
        /// Ports of the node. Runtime-only field.
        /// </summary>
        protected Dictionary<string, IOerPort> Ports { get; private set; }

        /// <summary>
        /// Called in runtime, before graph usage
        /// This method links some essential data into runtime nodes
        /// The core point of it is avoiding serialization of references to ports/graph/etc
        /// </summary>
        /// <param name="graph"></param>
        public void InitializeRuntime(OerGraph graph, List<IOerPort> ports)
        {
            Graph = graph;
            Ports = ports.ToDictionary(p => p.Name);
        }

        public abstract object GetValue(string portName);

        protected TPort GetPort<TPort>(string name)
            where TPort : IOerPort
        {
            return (TPort)Ports[name];
        }

        public OerNode CreateOwnNonInitializedCopy()
        {
            var copy = CreateInstance();
            copy.Id = Id;
            copy.InPortIds = new List<int>(InPortIds);
            copy.OutPortIds = new List<int>(OutPortIds);
            return copy;
        }

        protected abstract OerNode CreateInstance();
    }
}