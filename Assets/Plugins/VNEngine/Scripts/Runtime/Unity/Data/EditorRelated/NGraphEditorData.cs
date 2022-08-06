using System;
using UnityEngine;

namespace VNEngine.Runtime.Unity.Data.EditorRelated
{
    /// <summary>
    /// All the data needed to visualize graph and other non-runtime data goes here
    /// </summary>
    [Serializable]
    public class NGraphEditorData
    {
        public NodeEditorDataDictionary Nodes;
    }
}