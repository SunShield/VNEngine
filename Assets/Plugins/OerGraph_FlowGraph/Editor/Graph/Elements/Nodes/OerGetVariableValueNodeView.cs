using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Nodes
{
    public abstract class OerGetVariableValueNodeView : OerResolvableGraphNodeView
    {
        protected DropdownField VariableNameDropdown { get; private set; }
        
        protected OerGetVariableValueNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            AddDropdown();
        }

        private void AddDropdown()
        {
            VariableNameDropdown = new()
            {
                choices = GetVariableNames(),
                index = 0
            };
            VariableNameDropdown.RegisterValueChangedCallback(OnValueChanged);
            inputContainer.Add(VariableNameDropdown);
        }

        protected abstract List<string> GetVariableNames();

        private void OnValueChanged(ChangeEvent<string> changeEvent)
        {
            var runtimeNode = View.Graph.GetNode(RuntimeNodeId);
            var runtimeNodeTyped = runtimeNode as OerGetVariableValueNode;
            runtimeNodeTyped.SetVariableName(changeEvent.newValue);
        }

        protected override void OnPortAdded(OerPortView port)
        {
            port.SetBackingFieldHidden(true);
        }
    }
}