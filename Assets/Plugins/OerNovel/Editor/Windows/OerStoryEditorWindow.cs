using OerGraph.Editor.Configuration;
using OerGraph.Editor.Graphs;
using OerGraph.Editor.Windows.Elements;
using OerNovel.Editor.Windows.Elements;
using OerNovel.Editor.Windows.Elements.Dialogues;
using OerNovel.Editor.Windows.Elements.Story;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerNovel.Editor.Windows
{
    public class OerStoryEditorWindow : EditorWindow
    {
        public OerStoryInspector StoryInspector { get; private set; }
        public OerStoryDialogueInspector DialogueInspector { get; private set; }
        public OerGraphEditor Editor { get; private set; }
        
        [MenuItem("Window/OerGraph/Story Editor")]
        public static void Show() => GetWindow<OerStoryEditorWindow>("Story Editor");
        
        public void OnEnable()
        {
            ConfigureRoot();
            ApplyConfiguration();
            
            CreateStoryInspector();
            CreateDialogueInspector();
            CreateGraphView();
        }
        
        private void ConfigureRoot()
        {
            rootVisualElement.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
        }
        
        private void ApplyConfiguration()
        {
            var configurator = (OerConfigurator) Resources.Load("OerConfigurator");
            configurator.ApplyConfiguration();
        }

        private void CreateStoryInspector()
        {
            StoryInspector = new OerStoryInspector(this);
            rootVisualElement.Add(StoryInspector);
        }

        private void CreateDialogueInspector()
        {
            DialogueInspector = new(StoryInspector);
            rootVisualElement.Add(DialogueInspector);
        }

        private void CreateGraphView()
        {
            Editor = new OerGraphEditor(this);
            rootVisualElement.Add(Editor);
        }
    }
}