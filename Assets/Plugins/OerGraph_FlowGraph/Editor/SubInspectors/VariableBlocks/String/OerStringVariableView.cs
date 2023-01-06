using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.String
{
    public class OerStringVariableView: OerVariableView
    {
        private TextField _inputField;
        
        protected override void BuildValueField(object value = null)
        {
            _inputField = new();
            _inputField.RegisterValueChangedCallback(change =>
            {
                Graph.StringVariables[Name].DefaultValue = change.newValue;
            });

            if (value is string stringValue)
                _inputField.value = stringValue;
            
            Add(_inputField);
        }

        protected override void PreRemoveSelf()
        {
            Graph.StringVariables.Remove(Name);
        }
    }
}