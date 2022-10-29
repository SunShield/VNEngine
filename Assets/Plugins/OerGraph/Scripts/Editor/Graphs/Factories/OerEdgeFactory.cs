using OerGrap.Editor.Graphs.Elements.Ports;
using UnityEditor.Experimental.GraphView;

namespace OerGraph.Editor.Graphs.Factories
{
    public class OerEdgeFactory
    {
        public static Edge CreateEdge(OerPortView port1, OerPortView port2)
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