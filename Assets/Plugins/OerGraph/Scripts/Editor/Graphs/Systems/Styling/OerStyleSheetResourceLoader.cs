using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Graphs.Systems.Styling
{
    public static class OerStyleSheetResourceLoader
    {
        public static void TryAddStyleSheetFromPath(string sheetPath, VisualElement element, bool editorResource = false)
        {
            if (string.IsNullOrEmpty(sheetPath))
            {
                Debug.LogError($"StyleSheet path is null or empty!");
                return;
            }

            var styleSheet = editorResource 
                ? EditorResources.Load<StyleSheet>($"{sheetPath}.uss") 
                : Resources.Load<StyleSheet>(sheetPath);
            
            if (styleSheet == null)
            {
                Debug.LogError($"There is no stylesheet by address {sheetPath}");
                return;
            }
            
            element.styleSheets.Add(styleSheet);
        }
    }
}