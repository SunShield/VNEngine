using UnityEngine;
using VNEngine.Editor.Graphs.Systems.Styling;
using VNEngine.Runtime.Core.Graphs.Attributes.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;
using VNEngine.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public class NNodeViewFactory
    {
        // TODO: think on adding possibility to add custom NodeViews to nodes (using attribute or, maybe, type dictionary for performance)
        
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