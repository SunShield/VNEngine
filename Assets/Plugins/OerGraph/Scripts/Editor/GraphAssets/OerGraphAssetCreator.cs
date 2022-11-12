using System;
using System.Collections.Generic;
using OerGraph.Editor.GraphAssets.Builders;
using OerGraph.Editor.GraphAssets.Builders.Impl;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Core.Graphs.Tools.EditorBased;
using OerGraph.Runtime.Core.Service.Extensions;
using OerGraph.Runtime.Unity.Data;
using UnityEngine;

namespace OerGraph.Editor.GraphAssets
{
    public static class OerGraphAssetCreator
    {
        private static readonly Dictionary<Type, OerGraphAssetBuilder> _builders = new();
        private static DefaultGraphAssetBuilder _defaultBuilder = new();

        public static void DropMappings() => _builders.Clear();

        public static void AddMappings(Dictionary<Type, Type> builderTypeMappings)
        {
            foreach (var graphType in builderTypeMappings.Keys)
            {
                if (!typeof(OerMainGraph).IsAssignableFrom(graphType)) throw new ArgumentException($"Type {graphType} is not assignable from OerMainGraph!");
                if (!typeof(OerGraphAssetBuilder).IsAssignableFrom(builderTypeMappings[graphType])) throw new ArgumentException($"Type {builderTypeMappings[graphType]} is not assignable from OerGraphAssetBuilder!");

                var graphTypeAlreadyIntroduced = _builders.ContainsKey(graphType);
                if (graphTypeAlreadyIntroduced) Debug.LogWarning($"Type {graphType} already has a builder of type ({_builders[graphType]}) assigned to it!" + 
                                                                 "Not it will be overriden by builder of type ({builderTypeMappings[graphType]})");
                
                if (!graphTypeAlreadyIntroduced) _builders.Add(graphType, null);
                
                _builders[graphType] = Activator.CreateInstance(builderTypeMappings[graphType]) as OerGraphAssetBuilder;
            }
        }

        public static string GetCreateLocation(string graphName, string graphKey)
        {
            var graphType = OerGraphCreator.GraphKeyToTypeMappings[graphKey];
            var builder = GetBuilderForGraphType(graphType);
            return builder.GetBuildLocation(graphName);
        }

        public static OerGraphAsset CreateGraphAsset(string createLocation, string graphName, string graphKey)
        {
            var graph = OerGraphCreator.CreateGraph(graphKey);
            var builder = GetBuilderForGraphType(graph.GetType());
            return builder.BuildAsset(createLocation, graphName, graph);
        }

        private static OerGraphAssetBuilder GetBuilderForGraphType(Type graphType)
        {
            var typeHierarchy = graphType.GetTypeHierarchy(typeof(OerMainGraph));
            foreach (var type in typeHierarchy)
            {
                if (_builders.TryGetValue(type, out var builder)) return builder;
            }

            return _defaultBuilder;
        }
    }
}