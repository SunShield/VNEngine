using OerNovel.Runtime.Service.Classes;
using UnityEditor;

namespace OerGraph.Editor.SerializedDictionaryPropertyDrawer
{
    [CustomPropertyDrawer(typeof(StringToOerCharacterDataDictionary))]
    [CustomPropertyDrawer(typeof(StringToOerDialogueGraphDictionary))]
    [CustomPropertyDrawer(typeof(StringToOerGraphAssetDictionary))]
    public partial class AnySerializableDictionaryPropertyDrawer
    {
    }
}