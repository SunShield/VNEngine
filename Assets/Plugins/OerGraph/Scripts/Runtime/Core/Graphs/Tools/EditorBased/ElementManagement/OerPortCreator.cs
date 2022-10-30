using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement
{
    public static class OerPortCreator
    {
        // TODO: add some kind of binding of regular and dynamic ports of the same type
        
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

        public static IOerDynamicPort CreateDynamicPort(string portKey, int id, string name, OerPortType type,
            int nodeId)
        {
            IOerDynamicPort port = null;
            if (portKey == "Int") 
                port = new IntOerDynamicPort();
            port.Initialize(id, type, name, nodeId);
            return port;
        }
    }
}