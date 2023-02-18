using System;
using System.Collections.Generic;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings.Graphs
{
    public abstract class OerGraphMappings : ScriptableObject
    {
        public abstract Dictionary<string, Type> GetGraphTypes();
        public virtual Dictionary<string, Type> GetAssetGraphBuilderTypes() => null;
        public virtual Dictionary<Type, Type> GetGraphSubInspectorTypes() => null;
    }
}