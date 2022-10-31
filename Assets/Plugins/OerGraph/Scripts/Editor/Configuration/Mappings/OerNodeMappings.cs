using System;
using System.Collections.Generic;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings
{
    public abstract class OerNodeMappings : ScriptableObject
    {
        public abstract Dictionary<Type, Type> GetRuntimeNodeToViewMappings();
        public abstract Dictionary<string, Type> GetRuntimeNodeKeys();
    }
}