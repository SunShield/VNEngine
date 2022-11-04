﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings
{
    public abstract class OerNodeMappings : ScriptableObject
    {
        public virtual Dictionary<Type, Type> GetRuntimeNodeToViewMappings() => null;
        public virtual Dictionary<string, Type> GetRuntimeNodeKeys() => null;
        public virtual Dictionary<string, Type> GetRuntimePortKeys() => null;
        public virtual Dictionary<string, Type> GetRuntimePortDynamicKeys() => null;
    }
}