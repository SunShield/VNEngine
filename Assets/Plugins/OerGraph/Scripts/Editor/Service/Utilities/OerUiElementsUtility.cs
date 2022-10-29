using System;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Service.Utilities
{
    public static class OerUiElementsUtility
    {
        public static TextField CreateTextField(string name, string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            var field = new TextField() { name = name, value = value, label = label };
            if (onValueChanged != null) field.RegisterValueChangedCallback(onValueChanged);
            
            return field;
        }

        public static Button CreateButton(string text = null, Action onClick = null) => new (onClick) { text = text };
    }
}