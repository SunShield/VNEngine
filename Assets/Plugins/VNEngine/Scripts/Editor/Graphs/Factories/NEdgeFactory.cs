using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Elements.Ports;

namespace VNEngine.Editor.Graphs.Factories
{
    public static class NEdgeFactory
    {
        public static Edge CreateEdge(NPortView port1, NPortView port2)
        {
            var edge = new Edge
            {
                output = port1.direction == Direction.Output ? port1 : port2,
                input = port1.direction == Direction.Input ? port1 : port2
            };

            return edge;
        }
    }
}