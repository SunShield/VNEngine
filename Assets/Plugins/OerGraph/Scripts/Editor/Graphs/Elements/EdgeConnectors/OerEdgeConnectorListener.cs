using OerGraph.Editor.Graphs.Systems.Connecting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace OerGraph.Editor.Graphs.Elements.EdgeConnectors
{
    public class OerEdgeConnectorListener : IEdgeConnectorListener
    {
        private OerGraphView _graphView;
        
        public OerEdgeConnectorListener(OerGraphView graphView)
        {
            _graphView = graphView;
        }

        public void OnDrop(GraphView graphView, Edge edge)
        {
            OerConnectionSetupper.SetupNewConnection(edge, _graphView);
        }
        
        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
        }
    }
}