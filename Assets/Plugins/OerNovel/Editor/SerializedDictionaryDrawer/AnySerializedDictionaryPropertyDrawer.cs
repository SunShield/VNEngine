using OerNovel.Runtime.Service.Classes;
using UnityEditor;

namespace OerGraph.Editor.SerializedDictionaryPropertyDrawer
{
    [CustomPropertyDrawer(typeof(StringToOerCharacterDataDictionary))]
    [CustomPropertyDrawer(typeof(StringToOerStoryGraphDictionary))]
    public partial class AnySerializableDictionaryPropertyDrawer
    {
    }
}