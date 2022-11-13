using System;
using UnityEngine;
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

        public static VisualElement CreateDivider(float thickness)
        {
            var divider = new VisualElement();
            divider.style.height = 0.01f;
            divider.style.backgroundColor = new StyleColor(Color.black);
            divider.style.borderBottomWidth = thickness;
            divider.style.marginBottom = 2f;
            divider.style.marginTop = 3f;
            return divider;
        }
    }
}