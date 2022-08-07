using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VNEngine.Editor.Graphs.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Edges
{
    public class NEdgeConnectorListener : IEdgeConnectorListener
    {
        private NGraphView _graphView;
        
        public NEdgeConnectorListener(NGraphView graphView)
        {
            _graphView = graphView;
        }
        
        public void OnDrop(GraphView graphView, Edge edge)
        {
            var input = edge.input as NPortView;
            var output = edge.output as NPortView;
            input.ConnectTo(output);
            
            graphView.AddElement(edge);
        }
        
        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
        }
    }
}