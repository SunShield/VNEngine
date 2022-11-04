using UnityEditor;
using OerGraph.Runtime.Core.Service.Classes.Dicts;
using OerGraph.Runtime.Unity.Data.EditorRelated;

namespace OerGraph.Editor.SerializedDictionaryPropertyDrawer
{
    [CustomPropertyDrawer(typeof(IntToOerNodeDictionary))]
    [CustomPropertyDrawer(typeof(IntToOerPortDictionary))]
    [CustomPropertyDrawer(typeof(IntToIntListDictionary))]
    [CustomPropertyDrawer(typeof(OerNodeEditorDataDictionary))]
    [CustomPropertyDrawer(typeof(IntToOerDynamicPortDictionary))]
    [CustomPropertyDrawer(typeof(StringToIntDictionary))]
    [CustomPropertyDrawer(typeof(StringToFloatDictionary))]
    [CustomPropertyDrawer(typeof(StringToStringDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}