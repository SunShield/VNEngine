using System;
using System.Collections.Generic;
using System.Linq;
using OerGraph.Runtime.Core.Graphs.Tools.EditorBased;
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

        private Dictionary<string, OerGraphDatasElementnspector> _dataInspectors = new();
        private OerGraphDatasElementnspector _currentDataInspector;

        private string NewGraphName => _newGraphDataName.value;
        private string CurrentGraphKey => _graphTypeDropdown.value;

        public OerGraphDatasInspector(OerGraphInspector inspector)
        {
            _inspector = inspector;
            BuildGeometry();
            AddEvents();
        }

        public void SetAsset(OerGraphAsset asset)
        {
            ClearGraphDatas();
            _asset = asset;
            if (asset == null) return;
            
            AddExistingGraphDataInspectors();
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
            
            _graphTypeDropdown.choices.AddRange(OerGraphCreator.GraphKeyToTypeMappings.Keys.ToList());;
        }

        private void AddEvents()
        {
            _addNewGraphDataButton.clicked += () =>
            {
                if (string.IsNullOrEmpty(NewGraphName)) return;

                var graph = OerGraphCreator.CreateGraph(CurrentGraphKey);
                var data = new OerGraphData()
                {
                    Name = NewGraphName,
                    Key = CurrentGraphKey,
                    Graph = graph,
                    EditorData = new()
                    {
                        Nodes = new()
                    }
                };
                _asset.Graphs.Add(NewGraphName, data);
                EditorUtility.SetDirty(_asset);
                AddGraphDataInspector(data);

                _newGraphDataName.value = "";
            };
        }

        private void AddExistingGraphDataInspectors()
        {
            var firstSelected = false;
            foreach (var graphData in _asset.Graphs.Values)
            {
                AddGraphDataInspector(graphData);
                if (firstSelected) continue;
                
                SelectElement(graphData.Name);
                firstSelected = true;
            }
        }

        private OerGraphDatasElementnspector AddGraphDataInspector(OerGraphData data)
        {
            var inspector = new OerGraphDatasElementnspector(data);
            inspector.onRemoveClicked += RemoveGraphData;
            inspector.onElementClicked += SelectElement;
            _dataInspectors.Add(data.Name, inspector);
            _graphDatas.Add(inspector);

            return inspector;
        }

        private void RemoveGraphData(string dataName)
        {
            _asset.Graphs.Remove(dataName);
            RemoveGraphDataInspector(dataName);
        }

        private void RemoveGraphDataInspector(string dataName)
        {
            _graphDatas.Remove(_dataInspectors[dataName]);
            _dataInspectors.Remove(dataName);
        }

        private void ClearGraphDatas()
        {
            foreach (var key in _dataInspectors.Keys)
            {
                RemoveGraphDataInspector(key);
            }
            _currentDataInspector = null;
        }

        private void SelectElement(string elementName)
        {
            if (_currentDataInspector == _dataInspectors[elementName]) return;
            
            if (_currentDataInspector != null)
                _currentDataInspector.SetSelected(false);

            _currentDataInspector = _dataInspectors[elementName];
            _currentDataInspector.SetSelected(true);
            onGraphDataSelected?.Invoke(_currentDataInspector.Data);
        }

        public event Action<OerGraphData> onGraphDataSelected;
    }
}