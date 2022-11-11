using OerGraph.Editor.Configuration.Mappings.Graphs;
using OerGraph.Editor.Configuration.Mappings.Nodes;
using OerGraph.Editor.Configuration.Mappings.Ports;
using OerGraph.Editor.Graphs.Factories;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement;
using OerGraph.Runtime.Core.Graphs.Tools.EditorBased;
using UnityEngine;

namespace OerGraph.Editor.Configuration
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Configurator")]
    public class OerConfigurator : ScriptableObject
    {
        [SerializeField] private OerGraphMappings[] graphMappings;
        [SerializeField] private OerNodeMappings[] nodeMappings;
        [SerializeField] private OerPortMappings[] portMappings;

        public void ApplyConfiguration()
        {
            OerNodeFactory.DropMappings();
            OerPortFactory.DropMappings();
            OerGraphCreator.DropMappings();
            OerNodeViewFactory.DropMappings();
            OerPortViewFactory.DropMappings();

            foreach (var graphMapping in graphMappings)
            {
                var graphMpngs = graphMapping.GetGraphTypes();
                if (graphMpngs != null) OerGraphCreator.AddMappings(graphMpngs);
            }
            
            foreach (var nodeMapping in nodeMappings)
            {
                var runtimeViewMappings = nodeMapping.GetRuntimeNodeToViewMappings();
                if (runtimeViewMappings != null) OerNodeViewFactory.AddNodeViewMappings(runtimeViewMappings);

                var runtimeNodeKeys = nodeMapping.GetRuntimeNodeKeys();
                if (runtimeNodeKeys.nodeKeys != null) OerNodeFactory.AddNodeKeyMappings(runtimeNodeKeys.nodeKeys, runtimeNodeKeys.graphTypes);
            }
            
            foreach (var portMapping in portMappings)
            {
                var runtimePortKeys = portMapping.GetRuntimePortKeys();
                if (runtimePortKeys != null) OerPortFactory.AddPortKeyMappings(runtimePortKeys);
                
                var runtimeDynamicPortKeys = portMapping.GetRuntimePortDynamicKeys();
                if (runtimeDynamicPortKeys != null) OerPortFactory.AddDynamicPortKeyMappings(runtimeDynamicPortKeys);

                var runtimePortViewMappings = portMapping.GetRuntimePortToViewMappings();
                if (runtimePortViewMappings != null) OerPortViewFactory.AddPortViewMappings(runtimePortViewMappings);

                var runtimeDynamicPortViewMappings = portMapping.GetRuntimeDynamicPortToViewMappings();
                if (runtimeDynamicPortViewMappings != null) OerPortViewFactory.AddDynamicViewMappings(runtimeDynamicPortViewMappings);
            }
        }
    }
}