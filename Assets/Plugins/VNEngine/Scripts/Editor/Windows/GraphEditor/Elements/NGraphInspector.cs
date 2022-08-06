using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Editor.Service.Utilities;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Plugins.VNEngine.Scripts.Editor.Windows.GraphEditor.Elements
{
    public class NGraphInspector : VisualElement
    {
        private NGraphAsset _graph;
        private Label _currentGraphName;
        
        protected NDialogueEditorWindow ParentWindow { get; private set; }
        
        public NGraphInspector(NDialogueEditorWindow parentWindow)
        {
            ParentWindow = parentWindow;
            ConfigureRoot();
            AddCreateNewContainer();
            AddOpenContainer();
            AddSaveContainer();
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
            var createNewContainer = new VisualElement();
            createNewContainer.style.marginTop = 2f;
            createNewContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            createNewContainer.style.height = 20;
            Add(createNewContainer);
            
            var newGraphButton = UiElementsUtility.CreateButton("New", TryAddNewObject);
            var graphNameField = UiElementsUtility.CreateTextField(name: "new-object-name-field", label: "New Graph Name");
            var label = graphNameField.Q<Label>();
            label.style.minWidth = 100;
            graphNameField.style.flexGrow = 1;
            createNewContainer.Add(newGraphButton);
            createNewContainer.Add(graphNameField);
        }
        
        private void TryAddNewObject()
        {
            var newObjectName = this.Q<TextField>("new-object-name-field").value;
            
            if (string.IsNullOrEmpty(newObjectName)) return;
            var path = EditorUtility.SaveFilePanel("Create new graph asset", "", $"{newObjectName}.asset", "");
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

            if (string.IsNullOrEmpty(path)) return;

            var so = ScriptableObject.CreateInstance<NGraphAsset>();
            so.RuntimeGraph = new(newObjectName);
            AssetDatabase.CreateAsset(so, $"{pathFormatted}");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void AddOpenContainer()
        {
            var openContainer = new VisualElement();
            openContainer.style.marginTop = 2f;
            openContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            openContainer.style.height = 20;
            Add(openContainer);
            
            var openGraphButton = UiElementsUtility.CreateButton("Open", TryLoadGraph);
            openContainer.Add(openGraphButton);
            
            var dropCurrentGraphButton = UiElementsUtility.CreateButton("Drop", DropCurrentGraph);
            openContainer.Add(dropCurrentGraphButton);
            
            _currentGraphName = new Label();
            _currentGraphName.style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0f, 1f));
            _currentGraphName.style.minWidth = 100;
            _currentGraphName.style.flexGrow = 1;
            _currentGraphName.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
            openContainer.Add(_currentGraphName);
        }
        
        private void TryLoadGraph()
        {
            var path = EditorUtility.OpenFilePanel("Choose a graph to open", "", "asset");

            if (string.IsNullOrEmpty(path)) return;
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

            var asset = AssetDatabase.LoadAssetAtPath<NGraphAsset>(pathFormatted);
            _graph = asset;
            _currentGraphName.text = asset.RuntimeGraph.Name;
            ParentWindow.GraphEditor.SetGraph(asset);
        }

        private void DropCurrentGraph()
        {
            _graph = null;
            _currentGraphName.text = "";
            ParentWindow.GraphEditor.SetGraph(null);
        }

        private void AddSaveContainer()
        {
            var saveContainer = new VisualElement();
            saveContainer.style.marginTop = 2f;
            saveContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            saveContainer.style.height = 20;
            Add(saveContainer);
            
            var saveGraphButton = UiElementsUtility.CreateButton("Save", TrySaveGraph);
            saveContainer.Add(saveGraphButton);
        }

        private void TrySaveGraph()
        {
            foreach (var node in ParentWindow.GraphEditor.GraphView.Nodes.Values)
            {
                NodeEditorDataUpdater.UpdateNodeEditorData(_graph, node);
            }
            
            EditorUtility.SetDirty(_graph);
        }
    }
}