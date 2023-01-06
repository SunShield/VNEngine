using System;
using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.SubInspectors
{
    public abstract class OerVariableView : VisualElement
    {
        private Label _nameLabel;
        private Action<string> _removeViewAction;
        private Button _removeButton;
        
        protected VisualElement ColorLabel { get; private set; }
        protected OerResolvableGraph Graph { get; private set; }

        public string Name => _nameLabel.text;

        public void Initialize(string variableName, OerResolvableGraph graph, Action<string> removeViewAction, object value = null)
        {
            style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            Graph = graph;
            _removeViewAction = removeViewAction;
            BuildGeometry(variableName, value);
        }

        private void BuildGeometry(string variableName, object value = null)
        {
            ColorLabel = new VisualElement();
            Add(ColorLabel);
            _nameLabel = new Label()
            {
                text = variableName
            };
            Add(_nameLabel);
            BuildValueField(value);
            BuildRemoveButton();
        }

        private void BuildRemoveButton()
        {
            _removeButton = new(RemoveSelf)
            {
                text = "-",
                style = { minWidth = 10f }
            };
            Add(_removeButton);
        }
        
        protected abstract void BuildValueField(object value = null);

        protected void RemoveSelf()
        {
            PreRemoveSelf();
            _removeViewAction(Name);
        }

        protected abstract void PreRemoveSelf();
    }
}