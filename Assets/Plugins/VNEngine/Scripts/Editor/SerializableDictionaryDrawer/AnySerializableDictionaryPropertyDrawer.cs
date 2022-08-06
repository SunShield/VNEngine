using UnityEditor;
using VNEngine.Runtime.Core.Data;
using VNEngine.Runtime.Unity.Data.EditorRelated;

namespace VNEngine.Editor.SerializableDictionaryDrawer
{
    [CustomPropertyDrawer(typeof(NodeDictionary))]
    [CustomPropertyDrawer(typeof(NodeEditorDataDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}