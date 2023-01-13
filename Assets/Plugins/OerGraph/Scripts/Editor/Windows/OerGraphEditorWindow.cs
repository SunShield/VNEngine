using OerGraph.Editor.Configuration;
using OerGraph.Editor.Windows.Elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows
{
    public class OerGraphEditorWindow : EditorWindow
    {
        public OerGraphInspector GraphInspector { get; private set; }
        public OerGraphEditor GraphEditor { get; private set; }
        
        [MenuItem("Window/OerGraph/Graph Editor")]
        public static void Show() => GetWindow<OerGraphEditorWindow>("Graph Editor");
        
        private void OnEnable()
        {
            ConfigureRoot();
            ApplyConfiguration();

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
            GraphEditor = new OerGraphEditor(this);
            rootVisualElement.Add(GraphEditor);
        }

        private void AddGraphInspector()
        {
            GraphInspector = new OerGraphInspector(this);
            rootVisualElement.Add(GraphInspector);
        }

        private void AddStyles()
        {
            var styles = (StyleSheet) Resources.Load("Styles/OerVariables");
            rootVisualElement.styleSheets.Add(styles);
        }

        private void ApplyConfiguration()
        {
            var configurator = (OerConfigurator) Resources.Load("OerConfigurator");
            configurator.ApplyConfiguration();
        }
    }
}