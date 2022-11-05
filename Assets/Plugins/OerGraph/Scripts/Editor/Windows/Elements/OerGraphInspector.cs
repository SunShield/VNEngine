using OerGraph.Editor.Service.Prefs;
using OerGraph.Editor.Service.Utilities;
using OerGraph.Runtime.Unity.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements
{
    public class OerGraphInspector : VisualElement
    {
        private Label _currentGraphName;
        
        private string NewGraphName => this.Q<TextField>("new-object-name-field").value;
        
        protected OerDialogueEditorWindow ParentWindow { get; private set; }
        
        public OerGraphInspector(OerDialogueEditorWindow parentWindow)
        {
            ParentWindow = parentWindow;
            ConfigureRoot();
            AddCreateNewContainer();
            AddDivider();
            AddOpenContainer();
            AddSaveContainer();
            AddDivider();
        }

        private void ConfigureRoot()
        {
            style.width = 350;
            style.backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.2f));
            style.borderRightWidth = 2f;
            style.borderRightColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
        }

        private void AddCreateNewContainer()
        {
            var createNewContainer = ConstructCreateNewContainer();
            Add(createNewContainer);
            
            ConstructCreateNewButton(createNewContainer);
            ConstructGraphNameField(createNewContainer);
        }

        private VisualElement ConstructCreateNewContainer()
        {
            var createNewContainer = new VisualElement();
            createNewContainer.style.marginTop = 2f;
            createNewContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            createNewContainer.style.height = 20;
            return createNewContainer;
        }

        private void ConstructCreateNewButton(VisualElement createNewContainer)
        {
            var newGraphButton = OerUiElementsUtility.CreateButton("New", () =>
            {
                OerGraphAssetUtility.CreateGraph(NewGraphName, "Default");
                var recentlyCreatedGraphPath = OerPlayerPrefs.GetRecentlyCreatedGraphName();
                if (string.IsNullOrEmpty(recentlyCreatedGraphPath)) return;
                var asset = OerGraphAssetUtility.LoadGraph(recentlyCreatedGraphPath);
                SetGraph(asset);
                OerPlayerPrefs.ClearRecentlyCreatedGraphName();
            });
            createNewContainer.Add(newGraphButton);
        }

        private void ConstructGraphNameField(VisualElement createNewContainer)
        {
            var graphNameField = OerUiElementsUtility.CreateTextField(name: "new-object-name-field", label: "New Graph Name");
            var label = graphNameField.Q<Label>();
            label.style.minWidth = 100;
            graphNameField.style.flexGrow = 1;
            createNewContainer.Add(graphNameField);
        }

        private void AddDivider()
        {
            var divider = new VisualElement();
            divider.style.height = 0.01f;
            divider.style.backgroundColor = new StyleColor(Color.black);
            divider.style.borderBottomWidth = 1;
            divider.style.marginBottom = 3f;
            divider.style.marginTop = 3f;
            Add(divider);
        }

        private void AddOpenContainer()
        {
            var openContainer = ConstructOpenContainer();

            ConstructOpenGraphButton(openContainer);
            ConstructDropGraphButton(openContainer);
            ConstructCurrentGraphNameLabel(openContainer);
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

        private void ConstructOpenGraphButton(VisualElement openContainer)
        {
            var openGraphButton = OerUiElementsUtility.CreateButton("Open", TryLoadGraph);
            openContainer.Add(openGraphButton);
        }

        private void ConstructDropGraphButton(VisualElement openContainer)
        {
            var dropCurrentGraphButton = OerUiElementsUtility.CreateButton("Drop", DropCurrentGraph);
            openContainer.Add(dropCurrentGraphButton);
        }

        private void ConstructCurrentGraphNameLabel(VisualElement openContainer)
        {
            _currentGraphName = new Label();
            _currentGraphName.style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0f, 1f));
            _currentGraphName.style.minWidth = 100;
            _currentGraphName.style.flexGrow = 1;
            _currentGraphName.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
            openContainer.Add(_currentGraphName);
        }

        private void TryLoadGraph()
        {
            var asset = OerGraphAssetUtility.LoadGraph();
            if (asset == null) return;

            SetGraph(asset);
        }

        private void SetGraph(OerGraphAsset asset)
        {
            _currentGraphName.text = asset.name;
            ParentWindow.GraphEditor.SetGraph(asset);
        }

        private void DropCurrentGraph()
        {
            _currentGraphName.text = "";
            ParentWindow.GraphEditor.SetGraph(null);
        }

        private void AddSaveContainer()
        {
            var saveContainer = ConstructSaveContainer();
            ConstructSaveGraphButton(saveContainer);
        }

        private VisualElement ConstructSaveContainer()
        {
            var saveContainer = new VisualElement();
            saveContainer.style.marginTop = 2f;
            saveContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            saveContainer.style.height = 20;
            Add(saveContainer);
            return saveContainer;
        }

        private void ConstructSaveGraphButton(VisualElement saveContainer)
        {
            var saveGraphButton = OerUiElementsUtility.CreateButton("Save", SaveGraph);
            saveContainer.Add(saveGraphButton);
        }

        private void SaveGraph() => ParentWindow.GraphEditor.SaveGraph();

        private void AddGraphVariablesView()
        {
            
        }
    }
}