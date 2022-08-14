using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VNEngine.Plugins.VNEngine.Editor.Windows.GraphEditor.Elements;

namespace VNEngine.Plugins.VNEngine.Editor.Windows.GraphEditor
{
    public class NDialogueEditorWindow : EditorWindow
    {
        public NGraphInspector GraphInspector { get; private set; }
        public NGraphEditor GraphEditor { get; private set; }
        
        [MenuItem("Window/VNEngine/DialogueEditor")]
        public static void ShowExample() => GetWindow<NDialogueEditorWindow>("DialogueEditor");

        private void OnEnable()
        {
            ConfigureRoot();
            
            AddGraphInspector();
            AddGraphEditor();
            AddStyles();
        }

        private void ConfigureRoot()
        {
            rootVisualElement.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
        }

        private void AddGraphEditor()
        {
            GraphEditor = new NGraphEditor(this);
            rootVisualElement.Add(GraphEditor);
        }

        private void AddGraphInspector()
        {
            GraphInspector = new NGraphInspector(this);
            rootVisualElement.Add(GraphInspector);
        }

        private void AddStyles()
        {
            var styles = (StyleSheet) Resources.Load("Styles/NVariables");
            rootVisualElement.styleSheets.Add(styles);
        }
    }
}

