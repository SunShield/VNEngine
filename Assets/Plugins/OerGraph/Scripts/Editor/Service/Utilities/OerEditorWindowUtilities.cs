using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Service.Utilities
{
    public static class OerEditorWindowUtilities
    {
        public static Vector2 GetLocalMousePosition(EditorWindow editorWindow, VisualElement contentViewContainer, Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;

            if (isSearchWindow)
            {
                worldMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, mousePosition - editorWindow.position.position);
            }

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

            return localMousePosition;
        }
    }
}