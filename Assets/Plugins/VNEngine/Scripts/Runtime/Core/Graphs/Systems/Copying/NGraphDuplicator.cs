using System.Linq;
using VNEngine.Runtime.Core.Graphs.Data;
using VNEngine.Runtime.Core.Graphs.Systems.Reflection;

namespace VNEngine.Runtime.Core.Graphs.Systems.Copying
{
    public static class NGraphDuplicator
    {
        public static NGraph DuplicateGraph(NGraph graphToCopy)
        {
            var newGraph = new NGraph(graphToCopy.Name);

            foreach (var nodeId in graphToCopy.Nodes.Keys)
            {
                var copyingNode = graphToCopy.Nodes[nodeId];
                var newNode = copyingNode.Copy(newGraph);
                newGraph.AddNode(newNode);
            }

            foreach (var nodeId in graphToCopy.Nodes.Keys)
            {
                var copyingNode = graphToCopy.Nodes[nodeId];
                var newNode = newGraph.Nodes[nodeId];

                var nodeFields = copyingNode.GetType().GetFields().ToDictionary(x => x.Name);
                var copyingNodePorts = NNodePortsGetter.GetNodePortsByFields(copyingNode);
                foreach (var copyingNodePort in copyingNodePorts)
                {
                    
                    
                    /*if (copyingNodePort is INPort port)
                    {
                        var portFieldName = port.StaticName;
                        var newPort = Activator.CreateInstance(port.GetType(), port.Id);
                        nodeFields[portFieldName].SetValue(newNode, newPort);
                    }*/
                }
            }

            return newGraph;
        }
    }
}