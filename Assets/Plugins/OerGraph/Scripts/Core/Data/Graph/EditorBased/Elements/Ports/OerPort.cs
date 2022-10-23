using System;

namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Ports
{
    [Serializable]
    public abstract partial class OerPort<TType> : IOerPort
    {
        public int Id { get; private set; }
        public OerPortType Type { get; private set; }
        public string Name { get; private set; }
        public int NodeId { get; private set; }
        
        public TType DefaultValue { get; private set; }
        
        /// <summary>
        /// This method is called on port creation through editor window when node is created.
        /// It's never called in runtime
        /// </summary>
        /// <param name="id"></param>
        public void Initialize(int id, OerPortType type, string name, int nodeId)
        {
            Id = id;
            Type = type;
            Name = name;
            NodeId = nodeId;
        }
    }
}