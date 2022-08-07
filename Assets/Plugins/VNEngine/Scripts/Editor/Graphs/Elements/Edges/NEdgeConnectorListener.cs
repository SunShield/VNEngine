using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
        }
        
        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
        }
    }
}