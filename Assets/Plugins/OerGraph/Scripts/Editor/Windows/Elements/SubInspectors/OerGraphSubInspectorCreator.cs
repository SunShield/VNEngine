using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Core.Service.Extensions;
using OerGraph.Runtime.Unity.Data;
using UnityEngine;

namespace OerGraph.Editor.Windows.Elements.SubInspectors
{
    public static class OerGraphSubInspectorCreator
    {
        public static readonly Dictionary<Type, Type> _subInspectorMappings = new();
        
        public static void DropMappings() => _subInspectorMappings.Clear();
        
        public static void AddMappings(Dictionary<Type, Type> subInspectorMappings)
        {
            foreach (var graphType in subInspectorMappings.Keys)
            {
                if (!typeof(OerMainGraph).IsAssignableFrom(graphType)) throw new ArgumentException($"Type {graphType} is not assignable from OerMainGraph!");
                if (!typeof(OerGraphSubInspector).IsAssignableFrom(subInspectorMappings[graphType])) throw new ArgumentException($"Type {subInspectorMappings[graphType]} is not assignable from OerGraphSubInspector!");

                var graphTypeAlreadyIntroduced = _subInspectorMappings.ContainsKey(graphType);
                if (graphTypeAlreadyIntroduced) Debug.LogWarning($"Type {graphType} already has a builder of type ({_subInspectorMappings[graphType]}) assigned to it!" + 
                                                                 "Not it will be overriden by builder of type ({builderTypeMappings[graphType]})");
                
                if (!graphTypeAlreadyIntroduced) _subInspectorMappings.Add(graphType, subInspectorMappings[graphType]);
            }
        }

        public static OerGraphSubInspector CreateSubInspector(OerGraphAsset asset, OerGraphData data)
        {
            var typeHierarchy = data.Graph.GetType().GetTypeHierarchy(typeof(OerMainGraph));
            foreach (var type in typeHierarchy)
            {
                if (!_subInspectorMappings.ContainsKey(type)) continue;
                var subInspector = Activator.CreateInstance(_subInspectorMappings[type], asset, data) as OerGraphSubInspector;
                return subInspector;
            }

            return null;
        }
    }
}