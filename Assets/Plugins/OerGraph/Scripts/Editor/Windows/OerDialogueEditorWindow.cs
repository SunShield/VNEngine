﻿using OerGraph.Editor.Windows.Elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows
{
    public class OerDialogueEditorWindow : EditorWindow
    {
        public OerGraphInspector GraphInspector { get; private set; }
        public OerGraphEditor GraphEditor { get; private set; }
        
        [MenuItem("Window/OerGraph/DialogueEditor")]
        public static void Show() => GetWindow<OerDialogueEditorWindow>("DialogueEditor");
        
        private void OnEnable()
        {
            ConfigureRoot();

            AddGraphInspector();
            AddGraphEditor();
            AddStyles();
        }
        
        private void ConfigureRoot()
        {
            rootVisualElement.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
        }

        private void AddGraphEditor()
        {
            GraphEditor = new OerGraphEditor(this);
            rootVisualElement.Add(GraphEditor);
        }

        private void AddGraphInspector()
        {
            GraphInspector = new OerGraphInspector(this);
            rootVisualElement.Add(GraphInspector);
        }

        private void AddStyles()
        {
            var styles = (StyleSheet) Resources.Load("Styles/NVariables");
            rootVisualElement.styleSheets.Add(styles);
        }
    }
}