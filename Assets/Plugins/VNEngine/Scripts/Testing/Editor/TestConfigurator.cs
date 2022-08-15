using System;
using System.Collections.Generic;
using UnityEngine;
using VNEngine.Editor.Graphs.Elements.Nodes;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Plugins.VNEngine.Editor.Windows.GraphEditor;
using VNEngine.Plugins.VNEngine.Scripts.Editor.Configuration;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes.Implementations;

namespace VNEngine.Testing.Editor
{
    [CreateAssetMenu(fileName = "Test Configurator", menuName = "Test Configurator", order = 0)]
    public class TestConfigurator : VNEngineConfigurator
    {
        public override void Configure(NDialogueEditorWindow window)
        {
            var mappings = new Dictionary<Type, Func<NNode, NNodeView>>();
            mappings.Add(typeof(NBasicTestNode), (node) => new NTestNodeView(node));
            
            NNodeViewFactory.AddNodeViewMappings(mappings);
        }
    }
}