using OerGraph.Editor.Service.Utilities;
using OerGraph.Editor.Windows.Elements.SubInspectors;
using OerNovel.Editor.Windows.Elements.Story;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerNovel.Editor.Windows.Elements.Dialogues
{
    public class OerStoryDialogueInspector : VisualElement
    {
        private Label _currentDialogueName;
        private OerGraphSubInspector _subInspector;
        
        protected OerStoryInspector StoryInspector { get; private set; }

        public OerStoryDialogueInspector(OerStoryInspector storyInspector)
        {
            StoryInspector = storyInspector;
            ConfigureRoot();
            BuildGeometry();
        }
        
        private void ConfigureRoot()
        {
            style.width = 350;
            style.backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.2f));
            style.borderRightWidth = 2f;
            style.borderRightColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
        }

        private void BuildGeometry()
        {
            ConstructCurrentStoryNameLabel();
            AddDivider();
            AddSubInspectorIfNeeded();
        }
        
        private void ConstructCurrentStoryNameLabel()
        {
            _currentDialogueName = new Label();
            _currentDialogueName.style.backgroundColor = new StyleColor(new Color(0.6f, 0.4f, 0f, 1f));
            _currentDialogueName.style.minWidth = 100;
            _currentDialogueName.style.maxHeight = 20;
            _currentDialogueName.style.flexGrow = 1;
            _currentDialogueName.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
            Add(_currentDialogueName);
        }
        
        private void AddDivider() => Add(OerUiElementsUtility.CreateDivider(1f));
        
        public void AddSubInspectorIfNeeded()
        {
            var asset = StoryInspector.CurrentDialogueAsset;
            if (asset == null || asset.Graph == null) return;
            
            _subInspector = OerGraphSubInspectorCreator.CreateSubInspector(asset, asset.Graph);
            if (_subInspector == null) return;
            
            Add(_subInspector);
        }

        private void RemoveSubInspector()
        {
            Remove(_subInspector);
            _subInspector = null;
        }
    }
}