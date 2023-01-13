using OerGraph.Editor.Service.Utilities;
using OerGraph.Runtime.Unity.Data;
using OerNovel.Editor.Service.Utilities;
using OerNovel.Editor.StoryAssets;
using OerNovel.Editor.Windows.Elements.Dialogues;
using OerNovel.Runtime.Stories.Structure;
using OerNovel.Runtime.Unity;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerNovel.Editor.Windows.Elements.Story
{
    public class OerStoryInspector : VisualElement
    {
        private Label _currentStoryName;

        private string NewStoryName => this.Q<TextField>("new-object-name-field").value;
        protected OerStoryEditorWindow ParentWindows { get; private set; }
        protected OerStoryDialoguesInspector DialoguesInspector { get; private set; }

        public OerStoryAsset Asset { get; private set; }
        public OerStory Story => Asset.Story;
        public OerGraphAsset CurrentDialogueAsset => DialoguesInspector.SelectedDialogueAsset;
        
        public OerStoryInspector(OerStoryEditorWindow parentWindow)
        {
            ParentWindows = parentWindow;
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
            BuildCreateNewContainer();
            AddDivider();
            BuildOpenContainer();
            AddDivider();
            BuildStoryDialoguesInspector();
        }

        private void BuildCreateNewContainer()
        {
            var createNewContainer = CreateCreateNewContainer();
            Add(createNewContainer);
            
            BuildNewStoryButtonContainer(createNewContainer);
            BuildStoryNameInputField(createNewContainer);
        }

        private VisualElement CreateCreateNewContainer()
        {
            var createNewContainer = new VisualElement();
            createNewContainer.style.marginTop = 2f;
            createNewContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            createNewContainer.style.height = 20;
            return createNewContainer;
        }

        private void BuildNewStoryButtonContainer(VisualElement createNewContainer)
        {
            var newGraphButton = OerUiElementsUtility.CreateButton("New Story", () =>
            {
                var buildLocation = OerStoryAssetCreator.GetCreateLocation(NewStoryName);
                if (buildLocation == null) return;

                var storyAsset = OerStoryAssetCreator.Create(buildLocation, NewStoryName);
                SetStory(storyAsset);
            });
            createNewContainer.Add(newGraphButton);
        }
        
        private void SetStory(OerStoryAsset storyAsset)
        {
            _currentStoryName.text = storyAsset.name;
            Asset = storyAsset;
            DialoguesInspector.Update();
        }

        private void BuildStoryNameInputField(VisualElement createNewContainer)
        {
            var graphNameField = OerUiElementsUtility.CreateTextField(name: "new-object-name-field", label: "Story Name");
            var label = graphNameField.Q<Label>();
            label.style.minWidth = 70;
            graphNameField.style.flexGrow = 1;
            createNewContainer.Add(graphNameField);
        }
        
        private void AddDivider() => Add(OerUiElementsUtility.CreateDivider(1f));

        private void BuildOpenContainer()
        {
            var openContainer = ConstructOpenContainer();
            Add(openContainer);
            
            ConstructOpenStoryButton(openContainer);
            ConstructDropStoryButton(openContainer);
            ConstructCurrentStoryNameLabel(openContainer);
        }
        
        private VisualElement ConstructOpenContainer()
        {
            var openContainer = new VisualElement();
            openContainer.style.marginTop = 2f;
            openContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            openContainer.style.height = 20;
            Add(openContainer);
            return openContainer;
        }
        
        private void ConstructOpenStoryButton(VisualElement openContainer)
        {
            var openGraphButton = OerUiElementsUtility.CreateButton("Open", LoadStory);
            openContainer.Add(openGraphButton);
        }

        private void ConstructDropStoryButton(VisualElement openContainer)
        {
            var dropCurrentGraphButton = OerUiElementsUtility.CreateButton("Drop", DropCurrentStory);
            openContainer.Add(dropCurrentGraphButton);
        }

        private void ConstructCurrentStoryNameLabel(VisualElement openContainer)
        {
            _currentStoryName = new Label();
            _currentStoryName.style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0f, 1f));
            _currentStoryName.style.minWidth = 100;
            _currentStoryName.style.flexGrow = 1;
            _currentStoryName.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
            openContainer.Add(_currentStoryName);
        }

        private void LoadStory()
        {
            var story = OerStoryAssetUtility.LoadStory();
            if (story == null) return;
            
            SetStory(story);
        }

        private void DropCurrentStory()
        {
            _currentStoryName.text = "";
            Asset = null;
            DialoguesInspector.Update();
        }

        private void BuildStoryDialoguesInspector()
        {
            DialoguesInspector = CreateStoryDialogueContainer();
            Add(DialoguesInspector);
        }

        private OerStoryDialoguesInspector CreateStoryDialogueContainer() => new (this);
    }
}