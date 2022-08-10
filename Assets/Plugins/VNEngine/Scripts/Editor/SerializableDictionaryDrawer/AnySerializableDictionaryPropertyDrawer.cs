using UnityEditor;
using VNEngine.Runtime.Core.Data;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Runtime.Unity.Data.EditorRelated;

namespace VNEngine.Editor.SerializableDictionaryDrawer
{
    [CustomPropertyDrawer(typeof(NodeDictionary))]
    [CustomPropertyDrawer(typeof(NodeEditorDataDictionary))]
    [CustomPropertyDrawer(typeof(IntToIntListDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}