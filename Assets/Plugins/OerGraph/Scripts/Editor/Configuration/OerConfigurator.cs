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
            OerNodeViewFactory.DropMappings();
            OerNodeFactory.DropMappings();
            OerPortFactory.DropMappings();
            OerGraphCreator.DropMappings();

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
                if (runtimeNodeKeys != null) OerNodeFactory.AddNodeKeyMappings(runtimeNodeKeys);
            }
            
            foreach (var portMapping in portMappings)
            {
                var runtimePortKeys = portMapping.GetRuntimePortKeys();
                if (runtimePortKeys != null) OerPortFactory.AddPortKeyMappings(runtimePortKeys);
                
                var runtimeDynamicPortKeys = portMapping.GetRuntimePortDynamicKeys();
                if (runtimeDynamicPortKeys != null) OerPortFactory.AddDynamicPortKeyMappings(runtimeDynamicPortKeys);
            }
        }
    }
}