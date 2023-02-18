using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph_FlowGraph.Runtime.Graphs;

namespace OerGraph.Runtime.Core.Graphs.Tools.EditorBased
{
    public static class OerGraphCreator
    {
        private static readonly Dictionary<string, Type> _graphKeys = new();
        public static Dictionary<string, Type> GraphKeyToTypeMappings => _graphKeys;
        
        public static void AddMappings(Dictionary<string, Type> mappings)
        {
            foreach (var graphKey in mappings.Keys)
            {
                if (!typeof(OerMainGraph).IsAssignableFrom(mappings[graphKey])) throw new ArgumentException($"$Type {graphKey} is not inherited from OerMainGraph!");

                if (_graphKeys.ContainsKey(graphKey)) _graphKeys[graphKey] = mappings[graphKey];
                else                                  _graphKeys.Add(graphKey, mappings[graphKey]);
            }
        }

        public static void DropMappings() => _graphKeys.Clear();
        
        public static OerMainGraph CreateGraph(string graphKey)
        {
            var graph = (OerMainGraph)Activator.CreateInstance(_graphKeys[graphKey]);
            
            // TODO: remove later
            if (graph is OerResolvableGraph rg)
                rg.Initialize();
            return graph;
        }
    }
}