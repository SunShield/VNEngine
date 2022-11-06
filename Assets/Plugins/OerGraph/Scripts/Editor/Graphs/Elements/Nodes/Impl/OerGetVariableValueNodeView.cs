using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Graphs.Elements.Nodes.Impl
{
    public abstract class OerGetVariableValueNodeView : OerNodeView
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