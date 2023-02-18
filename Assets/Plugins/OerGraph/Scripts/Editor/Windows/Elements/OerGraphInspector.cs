using System.Linq;
using OerGraph.Editor.GraphAssets;
using OerGraph.Editor.Service.Utilities;
using OerGraph.Editor.Windows.Elements.GraphDatas;
using OerGraph.Editor.Windows.Elements.SubInspectors;
using OerGraph.Runtime.Core.Graphs.Tools.EditorBased;
using OerGraph.Runtime.Unity.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements
{
    public class OerGraphInspector : VisualElement
    {
        private Label _currentAssetName;
        private DropdownField _graphTypeDropdown;
        private OerGraphDatasInspector _datasInspector;
        private OerGraphSubInspector _subInspector;
        
        private string NewAssetName => this.Q<TextField>("new-object-name-field").value;
        private string CurrentGraphKey => _graphTypeDropdown.value;
        
        public OerGraphEditorWindow ParentWindow { get; private set; }
        
        public OerGraphInspector(OerGraphEditorWindow parentWindow)
        {
            ParentWindow = parentWindow;
            ConfigureRoot();
            AddCreateNewContainer();
            AddDivider();
            AddOpenContainer();
            AddSaveContainer();
            AddDivider();
            AddDatasInspector();
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
            CreateGraphTypeDropdown(createNewContainer);
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
                var createLocation = OerGraphAssetCreator.GetCreateLocation(NewAssetName, CurrentGraphKey);
                if (createLocation == null) return;
                
                var asset = OerGraphAssetCreator.CreateGraphAsset(createLocation, NewAssetName, CurrentGraphKey);
                SetAsset(asset);
            });
            createNewContainer.Add(newGraphButton);
        }

        private void ConstructGraphNameField(VisualElement createNewContainer)
        {
            var graphNameField = OerUiElementsUtility.CreateTextField(name: "new-object-name-field", label: "Graph Name");
            var label = graphNameField.Q<Label>();
            label.style.minWidth = 70;
            graphNameField.style.flexGrow = 1;
            createNewContainer.Add(graphNameField);
        }

        private void CreateGraphTypeDropdown(VisualElement createNewContainer)
        {
            _graphTypeDropdown = new DropdownField();
            _graphTypeDropdown.choices = OerGraphCreator.GraphKeyToTypeMappings.Keys.ToList();
            _graphTypeDropdown.index = 0;
            createNewContainer.Add(_graphTypeDropdown);
        }

        private void AddDivider() => Add(OerUiElementsUtility.CreateDivider(1f));

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
            var openGraphButton = OerUiElementsUtility.CreateButton("Open", TryLoadAsset);
            openContainer.Add(openGraphButton);
        }

        private void ConstructDropGraphButton(VisualElement openContainer)
        {
            var dropCurrentGraphButton = OerUiElementsUtility.CreateButton("Drop", DropCurrentAsset);
            openContainer.Add(dropCurrentGraphButton);
        }

        private void ConstructCurrentGraphNameLabel(VisualElement openContainer)
        {
            _currentAssetName = new Label();
            _currentAssetName.style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0f, 1f));
            _currentAssetName.style.minWidth = 100;
            _currentAssetName.style.flexGrow = 1;
            _currentAssetName.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
            openContainer.Add(_currentAssetName);
        }

        private void TryLoadAsset()
        {
            var asset = OerGraphAssetUtility.LoadGraph();
            if (asset == null) return;

            SetAsset(asset);
        }

        private void SetAsset(OerGraphAsset asset)
        {
            _currentAssetName.text = asset.name;
            ParentWindow.GraphEditor.SetAsset(asset);
            _datasInspector.SetAsset(asset);
        }

        private void DropCurrentAsset()
        {
            _currentAssetName.text = "";
            ParentWindow.GraphEditor.SetAsset(null);
            _datasInspector.SetAsset(null);
            RemoveSubInspector();
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

        private void AddDatasInspector()
        {
            _datasInspector = new(this);
            _datasInspector.onGraphDataSelected += (data) =>
            {
                if (ParentWindow.GraphEditor == null) return;
                
                ParentWindow.GraphEditor.SetGraph(data);
                if (_subInspector != null) RemoveSubInspector();
                AddSubInspectorIfNeeded();
            };
            Add(_datasInspector);
        }

        private void AddSubInspectorIfNeeded()
        {
            var data = ParentWindow.GraphEditor.GraphView.GraphData;
            var asset = ParentWindow.GraphEditor.GraphView.Asset;
            _subInspector = OerGraphSubInspectorCreator.CreateSubInspector(asset, data);
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