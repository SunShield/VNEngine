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
                OerNodeViewFactory.AddNodeViewMappings(nodeMapping.GetRuntimeNodeToViewMappings());
                OerGraphNodeCreator.AddNodeKeyMappings(nodeMapping.GetRuntimeNodeKeys());
            }
        }
    }
}