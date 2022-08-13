using UnityEngine;
using VNEngine.Editor.Graphs.Systems.Styling;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Nodes;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public class NNodeViewFactory
    {
        public NNodeView ConstructNodeView(NGraphView graphView, NNode runtimeNode, Vector2 position, NNodeParamsAttribute @params = null)
        {
            var view = new NNodeView(runtimeNode);
            if (@params != null) ApplyParams(view, @params);
            view.SetPosition(new (position, Vector2.zero));
            graphView.AddNode(view);
            return view;
        }

        private void ApplyParams(NNodeView nodeView, NNodeParamsAttribute @params)
        {
            nodeView.AddToClassList(@params.NodeClassName);
            
            if (!string.IsNullOrEmpty(@params.NodeStyleSheetPath))
                NStyleSheetResourceLoader.TryAddStyleSheetFromPath(@params.NodeStyleSheetPath, nodeView);
        }
    }
}