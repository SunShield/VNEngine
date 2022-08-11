using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VNEngine.Editor.Graphs.Systems.Connecting;

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
            NConnectionSetupper.SetupNewConnection(edge, _graphView);
        }
        
        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
        }
    }
}