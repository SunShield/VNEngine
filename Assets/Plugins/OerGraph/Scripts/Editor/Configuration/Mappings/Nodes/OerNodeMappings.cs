using System;
using System.Collections.Generic;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings.Nodes
{
    public abstract class OerNodeMappings : ScriptableObject
    {
        public virtual (Dictionary<string, Type> nodeKeys, HashSet<Type> graphTypes) GetRuntimeNodeKeys() => (null, null);
        public virtual Dictionary<Type, Type> GetRuntimeNodeToViewMappings() => null;
    }
}