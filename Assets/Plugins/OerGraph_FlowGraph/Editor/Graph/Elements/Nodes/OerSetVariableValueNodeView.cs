using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs;
using OerGraph.Editor.Graphs.Elements.Nodes;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Nodes
{
    public abstract class OerSetVariableValueNodeView : OerResolvableGraphNodeView
    {
        protected DropdownField VariableNameDropdown { get; private set; }
        
        protected OerSetVariableValueNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
        }
        
        private void AddDropdown()
        {
            VariableNameDropdown = new()
            {
                choices = GetVariableNames(),
                index = 0
            };
            VariableNameDropdown.RegisterValueChangedCallback(OnValueChanged);
            inputContainer.Insert(1, VariableNameDropdown);
        }
        
        protected abstract List<string> GetVariableNames();
        
        private void OnValueChanged(ChangeEvent<string> changeEvent)
        {
            var runtimeNode = View.Graph.GetNode(RuntimeNodeId);
            var runtimeNodeTyped = runtimeNode as OerSetVariableValueNode;
            runtimeNodeTyped.SetVariableName(changeEvent.newValue);
        }

        protected override void OnPortAdded(OerPortView port)
        {
            // It means we added both In and Value input ports
            if (Inputs.Count == 2 && VariableNameDropdown == null)
            {
                AddDropdown();
            }
        }
    }
}