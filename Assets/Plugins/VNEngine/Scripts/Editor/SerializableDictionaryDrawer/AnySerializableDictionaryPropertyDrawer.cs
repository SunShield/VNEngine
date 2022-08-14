using UnityEditor;
using VNEngine.Runtime.Core.Graphs.Data;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;
using VNEngine.Runtime.Unity.Data.EditorRelated;

namespace VNEngine.Editor.SerializableDictionaryDrawer
{
    [CustomPropertyDrawer(typeof(NodeDictionary))]
    [CustomPropertyDrawer(typeof(NodeEditorDataDictionary))]
    [CustomPropertyDrawer(typeof(IntToIntListDictionary))]
    [CustomPropertyDrawer(typeof(PortsDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}