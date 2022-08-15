using UnityEngine;
using VNEngine.Plugins.VNEngine.Scripts.Editor.Configuration;

namespace VNEngine.Plugins.VNEngine.Scripts.Editor.Initialization
{
    [CreateAssetMenu(fileName = "VNEngine Data", menuName = "VNEngine Data", order = 0)]
    public class VNEngineData : ScriptableObject
    {
        public VNEngineConfigurator Configurator;
    }
}