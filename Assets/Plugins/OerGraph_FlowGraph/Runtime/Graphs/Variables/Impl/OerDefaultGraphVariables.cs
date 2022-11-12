using System;
using System.Collections.Generic;
using System.IO;
using OerGraph.Runtime.Core.Service.Classes.Dicts;
using Sirenix.Serialization;
using UnityEngine;
using File = UnityEngine.Windows.File;

namespace OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl
{
    [Serializable]
    public class OerDefaultGraphVariables : OerGraphVariables
    {
        public OerDefaultGraphVariables(string graphPath) : base(graphPath)
        {
            var graphDirectory = Path.GetDirectoryName(graphPath);
            BoolVariablesKey   = $"{graphDirectory}/bool_variables";
            IntVariablesKey    = $"{graphDirectory}/int_variables";
            FloatVariablesKey  = $"{graphDirectory}/float_variables";
            StringVariablesKey = $"{graphDirectory}/string_variables";
        }
        
        protected override (bool loadResult, Dictionary<string, TType> variables) TryLoadVariables<TType>(string key)
        {
            if (!File.Exists(key)) return (false, null);

            var serializedData = File.ReadAllBytes(key);
            var data = SerializationUtility.DeserializeValue<Dictionary<string, TType>>(serializedData, DataFormat.Binary);
            return (true, data);
        }

        protected override Dictionary<string, TType> CreateVariables<TType>(string key)
        {
            var data = new Dictionary<string, TType>();
            var serializedData = SerializationUtility.SerializeValue(data, DataFormat.Binary);
            File.WriteAllBytes(key, serializedData);
            return data;
        }

        protected override void SaveVariables<TType>(Dictionary<string, TType> variables, string key)
        {
            var serializedData = SerializationUtility.SerializeValue(variables, DataFormat.Binary);
            File.WriteAllBytes(key, serializedData);
        }
    }
}