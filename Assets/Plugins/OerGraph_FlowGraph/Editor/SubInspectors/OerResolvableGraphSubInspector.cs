using OerGraph.Editor.Service.Utilities;
using OerGraph.Editor.Windows.Elements.SubInspectors;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Unity.Data;
using OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Bool;
using OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Float;
using OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Int;
using OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.String;
using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEngine.UIElements;

namespace OerGraph_FlowGraph.Editor.SubInspectors
{
    public class OerResolvableGraphSubInspector : OerGraphSubInspector
    {
        private ScrollView _scrollView;
        private OerResolvableGraphSubInspectorBoolVariablesBlock   _boolVariablesBlock;
        private OerResolvableGraphSubInspectorIntVariablesBlock    _intVariablesBlock;
        private OerResolvableGraphSubInspectorFloatVariablesBlock  _floatVariablesBlock;
        private OerResolvableGraphSubInspectorStringVariablesBlock _stringVariablesBlock;
        
        public OerResolvableGraphSubInspector(OerGraphAsset asset, OerGraphData data) : base(asset, data)
        {
            AddScroll();
            AddVariableBlocks();
        }

        private void AddScroll()
        {
            _scrollView = new ScrollView();
            Add(_scrollView);
        }

        private void AddVariableBlocks()
        {
            _boolVariablesBlock = new(Asset, Graph as OerResolvableGraph);
            Add(_boolVariablesBlock);
            _boolVariablesBlock.AddExistingVariables();
            
            Add(OerUiElementsUtility.CreateDivider(1f));
            
            _intVariablesBlock = new(Asset, Graph as OerResolvableGraph);
            Add(_intVariablesBlock);
            _intVariablesBlock.AddExistingVariables();
            
            Add(OerUiElementsUtility.CreateDivider(1f));
            
            _floatVariablesBlock = new(Asset, Graph as OerResolvableGraph);
            Add(_floatVariablesBlock);
            _floatVariablesBlock.AddExistingVariables();
            
            Add(OerUiElementsUtility.CreateDivider(1f));
            
            _stringVariablesBlock = new(Asset, Graph as OerResolvableGraph);
            Add(_stringVariablesBlock);
            _stringVariablesBlock.AddExistingVariables();
        }
    }
}