using System;
using UnityEngine;

namespace OerNovel.Runtime.Stories.Structure
{
    /// <summary>
    /// Story is a topmost entity in the OerNovel environment
    /// It contains Characters, Variables and Dialogues.
    /// The whole narrative part of the game, ideally, should be implemented as a single Story with various elements inside it.
    /// </summary>
    [Serializable]
    public partial class OerStory
    {
        [SerializeField] [HideInInspector] private bool _isInitialized;
        
        [field: SerializeField] public string Name { get; private set; }

        /// <summary>
        /// Called once after the very story creation
        /// </summary>
        public void Initialize(string storyName)
        {
            Name = storyName;
            _isInitialized = true;
        }
    }
}