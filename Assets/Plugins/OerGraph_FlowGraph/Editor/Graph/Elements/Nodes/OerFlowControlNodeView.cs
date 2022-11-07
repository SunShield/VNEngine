using OerGraph.Editor.Graphs;
using OerGraph.Editor.Graphs.Elements.Nodes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Nodes
{
    public class OerFlowControlNodeView : OerNodeView
    {
        public OerFlowControlNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            titleContainer.style.height = 22f;
            titleContainer.style.backgroundColor = new Color(0.9f, 0.9f, 0.9f);
            
            var titleLabel = contentContainer.Q<Label>("title-label");
            titleLabel.style.color = Color.black;

            var image = new Image();
            image.style.backgroundColor = Color.black;
            image.image = EditorGUIUtility.FindTexture("d_UnityEditor.Graphs.AnimatorControllerTool@2x");
            titleContainer.Add(image);
        }
    }
}