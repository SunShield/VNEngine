using OerGraph_FlowGraph.Runtime.Graphs.Variables.Service.Classes;
using OerGraph_FlowGraph.Runtime.Service.Classes;
using UnityEditor;

namespace OerGraph.Editor.SerializedDictionaryPropertyDrawer
{
    [CustomPropertyDrawer(typeof(StringToFlowNodeDictionary))]
    [CustomPropertyDrawer(typeof(StringToBoolVariableDictionary))]
    [CustomPropertyDrawer(typeof(StringToIntVariableDictionary))]
    [CustomPropertyDrawer(typeof(StringToFloatVariableDictionary))]
    [CustomPropertyDrawer(typeof(StringToStringVariableDictionary))]
    public partial class AnySerializableDictionaryPropertyDrawer
    {
    }
}