using OerGraph.Editor.Service.Utilities;
using OerGraph.Runtime.Unity.Data;
using OerNovel.Editor.DialogueAssets;
using UnityEditor;
using UnityEngine.UIElements;

namespace OerNovel.Editor.Windows.Elements.Story
{
    public class OerStoryDialoguesInspector : VisualElement
    {
        private ListView _dialoguesList;
        private TextField _newDialogueNameField;
        
        protected OerStoryInspector StoryInspector { get; private set; }
        private string NewDialogueName => this.Q<TextField>("new-dialogue-name-field").value;
        
        public OerGraphAsset SelectedDialogueAsset { get; private set; }
        
        public OerStoryDialoguesInspector(OerStoryInspector storyInspector)
        {
            StoryInspector = storyInspector;
            ConfigureRoot();
            BuildGeometry();
        }

        private void ConfigureRoot()
        {
            style.marginTop = 2f;
            style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Column);
            style.flexGrow = 1f;
            style.height = 20;
        }
        
        private void BuildGeometry()
        {
            BuildCreateNewContainer();
            BuildDialoguesList();
        }

        private void BuildCreateNewContainer()
        {
            var createNewContainer = CreateCreateNewContainer();
            Add(createNewContainer);
            
            BuildNewDialogueButtonContainer(createNewContainer);
            BuildDialogueNameInputField(createNewContainer);
        }

        private VisualElement CreateCreateNewContainer()
        {
            var createNewContainer = new VisualElement();
            createNewContainer.style.marginTop = 2f;
            createNewContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            createNewContainer.style.height = 20;
            return createNewContainer;
        }

        private void BuildNewDialogueButtonContainer(VisualElement createNewContainer)
        {
            var newGraphButton = OerUiElementsUtility.CreateButton("+", () =>
            {
                if (StoryInspector.Asset == null) return;
                if (StoryInspector.Asset.DialogueAssets.ContainsKey(NewDialogueName)) return;
                
                var asset = OerDialogueAssetGraphBuilder.Build(StoryInspector.Asset, NewDialogueName);
                StoryInspector.Asset.DialogueAssets.Add(NewDialogueName, asset);
                
                EditorUtility.SetDirty(StoryInspector.Asset);
                SelectedDialogueAsset = asset;

                /*var dialogueItem = new OerStoryDialogueItem(NewDialogueName);
                _dialoguesList.Add(dialogueItem);*/
            });
            createNewContainer.Add(newGraphButton);
        }

        private void BuildDialogueNameInputField(VisualElement createNewContainer)
        {
            _newDialogueNameField = OerUiElementsUtility.CreateTextField(name: "new-dialogue-name-field", label: "Dialogue Name");
            var label = _newDialogueNameField.Q<Label>();
            label.style.minWidth = 70;
            _newDialogueNameField.style.flexGrow = 1;
            createNewContainer.Add(_newDialogueNameField);
        }
        
        private void AddDivider() => Add(OerUiElementsUtility.CreateDivider(1f));

        private void BuildDialoguesList()
        {
            _dialoguesList = new ListView();
            Add(_dialoguesList);
        }

        public void Update()
        {
            Clear();
            if (StoryInspector.Asset == null) return;
            UpdateDialogues();
        }

        private void Clear()
        {
            _newDialogueNameField.SetValueWithoutNotify("");
        }

        private void UpdateDialogues()
        {
            
        }
    }
}