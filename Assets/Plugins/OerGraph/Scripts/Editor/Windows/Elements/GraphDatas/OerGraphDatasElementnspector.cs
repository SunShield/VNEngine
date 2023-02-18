using System;
using OerGraph.Runtime.Unity.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements.GraphDatas
{
    public class OerGraphDatasElementnspector : VisualElement
    {
        private const string EditorUxmlPath = @"Assets/Plugins/OerGraph/Uxml/GraphDataInspector.uxml";
        private static Color SelectedColor = new (0.3f, 0.6f, 0.75f, 1f);
        
        public OerGraphData Data { get; private set; }
        private VisualElement _mainContainer;
        private Label _dataName;
        private Button _removeButton;

        private Color _unselectedColor;

        public OerGraphDatasElementnspector(OerGraphData data)
        {
            Data = data;
            BuildGeometry();
            AddEvents();
            this.AddManipulator(new Clickable(() => onElementClicked?.Invoke(Data.Name)));
        }

        private void BuildGeometry()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EditorUxmlPath);
            _mainContainer = visualTree.CloneTree().Q<VisualElement>("Root");
            Add(_mainContainer);
            _unselectedColor = _mainContainer.style.backgroundColor.value;

            _dataName = _mainContainer.Q<Label>("DataName");
            _removeButton = _mainContainer.Q<Button>("RemoveDataButton");

            _dataName.text = $"({Data.Key}) {Data.Name}";
        }

        private void AddEvents()
        {
            _removeButton.clicked += () =>
            {
                onRemoveClicked?.Invoke(Data.Name);
            };
        }

        public void SetSelected(bool selected)
        {
            _mainContainer.style.backgroundColor = selected ? SelectedColor : _unselectedColor;
        }

        public event Action<string> onRemoveClicked;
        public event Action<string> onElementClicked;
    }
}