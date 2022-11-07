using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings.Ports.Impl
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/PortMappings", fileName = "Port Mappings")]
    public class DefaultOerPortMappings : OerPortMappings
    {
        public override Dictionary<string, Type> GetRuntimePortKeys()
        {
            return new()
            {
                { "Bool",  typeof(OerBoolPort) },
                { "Int",   typeof(OerIntPort) },
                { "Float", typeof(OerFloatPort) },
                { "String",typeof(OerStringPort) },
            };
        }

        public override Dictionary<string, Type> GetRuntimePortDynamicKeys()
        {
            return new()
            {
                { "Bool",  typeof(OerBoolDynamicPort) },
                { "Int",   typeof(OerIntDynamicPort) },
                { "Float", typeof(OerFloatDynamicPort) },
                { "String",typeof(OerStringDynamicPort) },
            };
        }
    }
}