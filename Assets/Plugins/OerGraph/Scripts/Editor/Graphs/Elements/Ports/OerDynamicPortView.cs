using System;
using OerGraph.Editor.Graphs;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace OerGrap.Editor.Graphs.Elements.Ports
{
    public class OerDynamicPortView : OerPortView
    {
        private Label _connectorTextLabel;

        public float ConnectorTextWidth
        {
            get => _connectorTextLabel.worldBound.width;
            set => _connectorTextLabel.style.width = value;
        }
        
        public OerDynamicPortView(OerGraphView view, int runtimePortId, Direction portDirection, Type type) : base(view, runtimePortId, portDirection, type)
        {
            _connectorTextLabel = this.Q<Label>("type");
        }
    }
}