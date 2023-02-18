using OerGraph.Runtime.Unity.Data;
using UnityEditor;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements.GraphDatas
{
    public class OerGraphDatasInspector : VisualElement
    {
        private const string EditorUxmlPath = @"Assets/Plugins/OerGraph/Uxml/GraphDatasInspector.uxml";
        
        private OerGraphAsset _asset;
        private OerGraphInspector _inspector;
        private VisualElement _mainContainer;
        private TextField _newGraphDataName;
        private DropdownField _graphTypeDropdown;
        private Button _addNewGraphDataButton;
        private ScrollView _graphDatas;
        
        private string NewGraphName => this.Q<TextField>("NewGraphNameField").value;
        private string CurrentGraphKey => _graphTypeDropdown.value;

        public OerGraphDatasInspector(OerGraphInspector inspector, OerGraphAsset asset)
        {
            _inspector = inspector;
            _asset = asset;
            BuildGeometry();
            AddEvents();
        }

        private void BuildGeometry()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EditorUxmlPath);
            _mainContainer = visualTree.CloneTree().Q<VisualElement>("Root");
            Add(_mainContainer);

            _newGraphDataName = _mainContainer.Q<TextField>("NewGraphName");
            _graphTypeDropdown = _mainContainer.Q<DropdownField>("GraphTypeDropdown");
            _addNewGraphDataButton = _mainContainer.Q<Button>("AddNewButton");
            _graphDatas = _mainContainer.Q<ScrollView>("GraphDatasView");
        }

        private void AddEvents()
        {
            
        }
    }
}