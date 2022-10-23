using OerGraph.Core.Service.Classes.Dicts;
using UnityEditor;

namespace OerGraph.Editor.SerializedDictionaryPropertyDrawer
{
    [CustomPropertyDrawer(typeof(IntToOerNodeDictionary))]
    [CustomPropertyDrawer(typeof(IntToOerPortDictionary))]
    [CustomPropertyDrawer(typeof(IntToIntListDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}