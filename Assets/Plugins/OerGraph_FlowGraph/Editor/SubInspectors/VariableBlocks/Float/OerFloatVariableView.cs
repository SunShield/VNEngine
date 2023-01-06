using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Float
{
    public class OerFloatVariableView : OerVariableView
    {
        private FloatField _inputField;
        
        protected override void BuildValueField(object value = null)
        {
            _inputField = new();
            _inputField.RegisterValueChangedCallback(change =>
            {
                Graph.FloatVariables[Name].DefaultValue = change.newValue;
            });

            if (value is float floatValue)
                _inputField.value = floatValue;
            
            Add(_inputField);
        }

        protected override void PreRemoveSelf()
        {
            Graph.FloatVariables.Remove(Name);
        }
    }
}