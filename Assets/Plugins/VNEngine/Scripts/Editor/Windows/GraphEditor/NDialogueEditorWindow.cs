using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VNEngine.Plugins.VNEngine.Editor.Windows.GraphEditor.Elements;
using VNEngine.Plugins.VNEngine.Scripts.Editor.Initialization;

namespace VNEngine.Plugins.VNEngine.Editor.Windows.GraphEditor
{
    public class NDialogueEditorWindow : EditorWindow
    {
        private VNEngineData _engineData;
        
        public NGraphInspector GraphInspector { get; private set; }
        public NGraphEditor GraphEditor { get; private set; }
        
        [MenuItem("Window/VNEngine/DialogueEditor")]
        public static void ShowExample() => GetWindow<NDialogueEditorWindow>("DialogueEditor");

        private void OnEnable()
        {
            ConfigureRoot();

            GetVNData();
            AddGraphInspector();
            AddGraphEditor();
            AddStyles();
            
            ConfigureVN();
        }

        private void ConfigureRoot()
        {
            rootVisualElement.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
        }

        private void GetVNData() => _engineData = Resources.Load<VNEngineData>("VNEngine Data");

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

        private void ConfigureVN()
        {
            if (_engineData.Configurator == null) return;
            
            _engineData.Configurator.Configure(this);
        }
    }
}

