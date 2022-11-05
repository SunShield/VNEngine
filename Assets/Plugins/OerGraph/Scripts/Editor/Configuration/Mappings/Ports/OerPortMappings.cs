﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings.Ports
{
    public abstract class OerPortMappings : ScriptableObject
    {
        public virtual Dictionary<string, Type> GetRuntimePortKeys() => null;
        public virtual Dictionary<string, Type> GetRuntimePortDynamicKeys() => null;
    }
}