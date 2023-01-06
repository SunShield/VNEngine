using System.Collections.Generic;
using OerGraph.Runtime.Unity.Data;
using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEditor;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.SubInspectors
{
    public abstract class OerResolvableGraphSubInspectorVariablesBlock<TVariableViewType> : VisualElement
        where TVariableViewType : OerVariableView, new()
    {
        protected OerGraphAsset Asset { get; private set; }
        protected OerResolvableGraph Graph { get; private set; }
        
        protected abstract string Title { get; }
        private VisualElement _contentContainer;
        protected Label TitleLabel { get; private set; }
        private Foldout _foldout;
        protected TextField VariableNameField { get; private set; }
        private Button _addVariableButton;

        protected Dictionary<string, TVariableViewType> VariableViews = new();

        protected OerResolvableGraphSubInspectorVariablesBlock(OerGraphAsset asset, OerResolvableGraph graph)
        {
            Asset = asset;
            Graph = graph;
            BuildGeometry();
        }

        private void BuildGeometry()
        {
            TitleLabel = new Label
            {
                text = Title
            };
            Add(TitleLabel);
            _contentContainer = new VisualElement
            {
                style = { flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row) }
            };
            Add(_contentContainer);

            VariableNameField = new TextField();
            VariableNameField.style.flexGrow = 1;
            _contentContainer.Add(VariableNameField);
            _addVariableButton = new Button(() => AddVariable(VariableNameField.value))
            {
                text = "+",
            };
            _contentContainer.Add(_addVariableButton);
            
            _foldout = new Foldout
            {
                text = "Variables"
            };
            Add(_foldout);
        }

        public abstract void AddExistingVariables();

        private void AddVariable(string variableName)
        {
            if (string.IsNullOrEmpty(variableName)) return;
            if (VariableViews.ContainsKey(variableName)) return;
            var varView = AddVariableView(variableName);
            AddVariableInternal(varView);
            EditorUtility.SetDirty(Asset);
        }

        protected TVariableViewType AddVariableView(string variableName, object varValue = null)
        {
            var varView = new TVariableViewType();
            varView.Initialize(variableName, Graph, RemoveVariable, varValue);
            VariableViews.Add(variableName, varView);
            _foldout.contentContainer.Add(varView);
            return varView;
        }

        private void RemoveVariable(string variableName)
        {
            RemoveVariableView(variableName);
            RemoveVariableInternal(variableName);
        }

        private void RemoveVariableView(string variableName)
        {
            var view = VariableViews[variableName];
            VariableViews.Remove(variableName);
            _foldout.contentContainer.Remove(view);
        }

        protected abstract void AddVariableInternal(TVariableViewType variableView);
        protected abstract void RemoveVariableInternal(string variableName);
    }
}