using UnityEngine;
using VNEngine.Plugins.VNEngine.Editor.Windows.GraphEditor;

namespace VNEngine.Plugins.VNEngine.Scripts.Editor.Configuration
{
    /// <summary>
    /// Use this class to do any configurations after window and its components were initialized
    /// </summary>
    public abstract class VNEngineConfigurator : ScriptableObject
    {
        public abstract void Configure(NDialogueEditorWindow window);
    }
}