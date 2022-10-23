using OerGraph.Core.Data.Graph.EditorBased.Elements.Ports.Impl;

namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Ports.Creation
{
    public static class OerPortCreator
    {
        public static IOerPort CreatePort(string portKey, int id, string name, OerPortType type, int nodeId)
        {
            // TODO: later can be changed to something more efficient,
            // but this approach is not that bad, after all
            IOerPort port = null;
            if (portKey == "Int") 
                port = new IntOerPort();
            port.Initialize(id, type, name, nodeId);
            return port;
        }
    }
}