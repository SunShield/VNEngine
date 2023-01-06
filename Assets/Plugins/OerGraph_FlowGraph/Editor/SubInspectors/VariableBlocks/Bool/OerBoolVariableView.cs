using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Bool
{
    public class OerBoolVariableView : OerVariableView
    {
        private Toggle _toggle;
        
        protected override void BuildValueField(object value = null)
        {
            _toggle = new();
            _toggle.RegisterValueChangedCallback(change =>
            {
                Graph.BoolVariables[Name].DefaultValue = change.newValue;
            });

            if (value is bool boolValue)
                _toggle.value = boolValue;
            
            Add(_toggle);
        }

        protected override void PreRemoveSelf()
        {
            Graph.BoolVariables.Remove(Name);
        }
    }
}