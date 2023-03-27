using System;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports
{
    [Serializable]
    public abstract partial class OerPort<TType> : IOerPort
    {
        public int id;
        public OerPortType type;
        public string name;
        public int nodeId;

        public TType DefaultValue;

        public int Id => id;
        public int NodeId => nodeId;
        public OerPortType Type => type;
        public string Name => name;

        /// <summary>
        /// This method is called on port creation through editor window when node is created.
        /// It's never called in runtime
        /// </summary>
        /// <param name="id"></param>
        public void Initialize(int id, OerPortType type, string name, int nodeId)
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.nodeId = nodeId;
        }

        public Type GetUnderlyingType() => typeof(TType);
    }
}