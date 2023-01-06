using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Int
{
    public class OerIntVariableView: OerVariableView
    {
        private IntegerField _inputField;
        
        protected override void BuildValueField(object value = null)
        {
            _inputField = new();
            _inputField.RegisterValueChangedCallback(change =>
            {
                Graph.IntVariables[Name].DefaultValue = change.newValue;
            });

            if (value is int intValue)
                _inputField.value = intValue;
            
            Add(_inputField);
        }

        protected override void PreRemoveSelf()
        {
            Graph.IntVariables.Remove(Name);
        }
    }
}