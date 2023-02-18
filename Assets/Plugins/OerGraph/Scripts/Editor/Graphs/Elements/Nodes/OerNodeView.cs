using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Systems.Styling;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Graphs.Elements.Nodes
{
    public class OerNodeView : Node
    {
        public int RuntimeNodeId { get; private set; }
        public OerGraphView View { get; private set; }
        public Dictionary<int, OerPortView> Inputs { get; private set; }
        public Dictionary<int, OerPortView> Outputs { get; private set; }

        private OerMainGraph RuntimeGraph => View.Graph;

        public OerNodeView(OerGraphView view, int runtimeNodeId)
        {
            RuntimeNodeId = runtimeNodeId;
            View = view;
            Inputs = new();
            Outputs = new();
            title = view.Graph.GetNode(runtimeNodeId).Name;
            AddToClassList(OerViewConsts.ViewClasses.OerNodeView);
            AddDefaultStyleSheet();
        }
        
        private void AddDefaultStyleSheet() => OerStyleSheetResourceLoader.TryAddStyleSheetFromPath(OerViewConsts.StyleSheets.DefaultNodeStyleSheetPath, this);
        
        public void AddPort(OerPortView port)
        {
            if (port.direction == Direction.Input)
            {
                Inputs.Add(port.RuntimePortId, port);
                inputContainer.Add(port);
            }
            else
            {
                Outputs.Add(port.RuntimePortId, port);
                outputContainer.Add(port);
            }

            OnPortAdded(port);
        }
        
        protected virtual void OnPortAdded(OerPortView port) { }
        
        public void AddDynamicPortsView(OerDynamicPortsView dynamicPortsView)
        {
            if (dynamicPortsView.Type == OerPortType.Input)
            {
                inputContainer.Add(dynamicPortsView);
            }
            else
            {
                outputContainer.Add(dynamicPortsView);
            }
        }

        public void AdjustPortSizes()
        {
            RegisterCallback<GeometryChangedEvent>(AdjustPortSizesInternal);
        }

        protected void AdjustPortSizesInternal(GeometryChangedEvent evt)
        {
            if (Inputs.Count != 0)
            {
                var maxLength = 0f;
            
                foreach (var port in Inputs.Values)
                {
                    if (port.ConnectorTextWidth > maxLength)
                        maxLength = port.ConnectorTextWidth;
                }
            
                foreach (var port in Inputs.Values)
                {
                    port.ConnectorTextWidth = maxLength;
                }
            }

            if (Outputs.Count != 0)
            {
                var maxLength = 0f;
            
                foreach (var port in Outputs.Values)
                {
                    if (port.ConnectorTextWidth > maxLength)
                        maxLength = port.ConnectorTextWidth;
                }
            
                foreach (var port in Outputs.Values)
                {
                    port.ConnectorTextWidth = maxLength;
                }
            }
            
            UnregisterCallback<GeometryChangedEvent>(AdjustPortSizesInternal);
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            var editorDatas = View.GraphData.EditorData.Nodes;
            var containsEditorData = editorDatas.TryGetValue(RuntimeNodeId, out var data);
            if (!containsEditorData) return;

            data.Position = newPos.center;
            EditorUtility.SetDirty(View.Asset);
        }
    }
}