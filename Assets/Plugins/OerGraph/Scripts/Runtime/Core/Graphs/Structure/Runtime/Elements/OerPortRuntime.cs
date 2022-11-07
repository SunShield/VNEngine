using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using UnityEditor;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports
{
    public abstract partial class OerPort<TType>
    {
        /// <summary>
        /// Master graph of the port. Runtime-only field
        /// </summary>
        public OerMainGraph MainGraph { get; private set; }
        
        /// <summary>
        /// Node port belongs to. Runtime-only field
        /// </summary>
        public OerNode Node { get; private set; }
        
        /// <summary>
        /// Port from where the value is taken
        /// 
        /// Actually, more than one ports can be connected to the node
        /// But this exists only in case you are NOT going to retrieve values from such ports
        /// (and use them, for example, like Flow in dialogue systems)
        /// But in case you accidentally do this, only the first found port is used
        /// (Used bu Input ports only)
        /// </summary>
        public IOerPort ValueRetrievingPort { get; private set; }
        
        /// <summary>
        /// Node from where the output value is taken
        /// (for essential tips look also at ValueRetrievingPort field desc)
        /// </summary>
        public OerNode ValueRetrievingNode { get; private set; }
        
        public TType GetValue()
        {
            if (ValueRetrievingNode == null) return DefaultValue;
            var value = ValueRetrievingNode.GetValue(ValueRetrievingPort.Name);
            return (TType)value;
        }

        public void InitializeRuntime(OerMainGraph mainGraph, OerNode ownerNode, IOerPort valueRetrievingPort, OerNode valueRetrievingNode)
        {
            MainGraph = mainGraph;
            Node = ownerNode;
            ValueRetrievingPort = valueRetrievingPort;
            ValueRetrievingNode = valueRetrievingNode;

            PostInitializeRuntime();
        }
        
        protected virtual void PostInitializeRuntime() { }

        public IOerPort CreateOwnNonInitializedCopy()
        {
            var copy = CreateInstance();
            copy.Id = Id;
            copy.Type = Type;
            copy.Name = Name;
            copy.NodeId = NodeId;
            copy.DefaultValue = CopyDefaultValue();
            return copy;
        }

        protected abstract OerPort<TType> CreateInstance();

        /// <summary>
        /// Makes a copy of port's default value
        /// Override this if a default value has a reference type
        /// </summary>
        /// <returns></returns>
        protected virtual TType CopyDefaultValue()
        {
            return DefaultValue;
        }
    }
}