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
        private static readonly Dictionary<string, OerGraphAssetBuilder> _builders = new();
        private static DefaultGraphAssetBuilder _defaultBuilder = new();

        public static void DropMappings() => _builders.Clear();

        public static void AddMappings(Dictionary<string, Type> builderTypeMappings)
        {
            foreach (var assetKey in builderTypeMappings.Keys)
            {
                if (!typeof(OerGraphAssetBuilder).IsAssignableFrom(builderTypeMappings[assetKey])) throw new ArgumentException($"Type {builderTypeMappings[assetKey]} is not assignable from OerGraphAssetBuilder!");

                var graphTypeAlreadyIntroduced = _builders.ContainsKey(assetKey);
                if (graphTypeAlreadyIntroduced) Debug.LogWarning($"Type {assetKey} already has a builder of type ({_builders[assetKey]}) assigned to it!" + 
                                                                 "Not it will be overriden by builder of type ({builderTypeMappings[graphType]})");
                
                if (!graphTypeAlreadyIntroduced) _builders.Add(assetKey, null);
                
                _builders[assetKey] = Activator.CreateInstance(builderTypeMappings[assetKey]) as OerGraphAssetBuilder;
            }
        }

        public static string GetCreateLocation(string graphName, string assetKey)
        {
            var builder = GetBuilderForAssetType(assetKey);
            return builder.GetBuildLocation(graphName);
        }

        public static OerGraphAsset CreateGraphAsset(string createLocation, string graphName, string currentAssetKey)
        {
            var builder = GetBuilderForAssetType(currentAssetKey);
            return builder.BuildAsset(createLocation, graphName);
        }

        private static OerGraphAssetBuilder GetBuilderForAssetType(string assetKey)
            => _builders.ContainsKey(assetKey) ? _builders[assetKey] : _defaultBuilder;
    }
}