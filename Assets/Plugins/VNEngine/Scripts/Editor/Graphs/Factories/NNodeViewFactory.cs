using UnityEngine;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public class NNodeViewFactory
    {
        public NNodeView ConstructNodeView(NGraphView graphView, NNode runtimeNode, Vector2 position)
        {
            var view = new NNodeView(runtimeNode);
            view.SetPosition(new (position, Vector2.zero));
            graphView.AddNode(view);
            return view;
        }
    }
}