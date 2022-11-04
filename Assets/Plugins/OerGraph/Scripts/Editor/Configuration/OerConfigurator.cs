using OerGraph.Editor.Configuration.Mappings;
using OerGraph.Editor.Graphs.Factories;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement;
using UnityEngine;

namespace OerGraph.Editor.Configuration
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Configurator")]
    public class OerConfigurator : ScriptableObject
    {
        [SerializeField] private OerNodeMappings[] nodeMappings;

        public void ApplyConfiguration()
        {
            foreach (var nodeMapping in nodeMappings)
            {
                var runtimeViewMappings = nodeMapping.GetRuntimeNodeToViewMappings();
                if (runtimeViewMappings != null) OerNodeViewFactory.AddNodeViewMappings(runtimeViewMappings);

                var runtimeNodeKeys = nodeMapping.GetRuntimeNodeKeys();
                if (runtimeNodeKeys != null) OerGraphNodeCreator.AddNodeKeyMappings(runtimeNodeKeys);
                
                var runtimePortKeys = nodeMapping.GetRuntimePortKeys();
                if (runtimePortKeys != null) OerPortCreator.AddPortKeyMappings(runtimePortKeys);
                
                var runtimeDynamicPortKeys = nodeMapping.GetRuntimePortDynamicKeys();
                if (runtimeDynamicPortKeys != null) OerPortCreator.AddDynamicPortKeyMappings(runtimeDynamicPortKeys);
            }
        }
    }
}