using UnityEngine;
using UnityEngine.UIElements;

namespace OerNovel.Editor.Windows.Elements.Story
{
    public class OerStoryDialogueItem : VisualElement
    {
        private Label _name;
        
        public OerStoryDialogueItem(string name)
        {
            _name.text = name;
            ConfigureRoot();
            BuildGeometry();
            AddManipulators();
        }

        private void ConfigureRoot()
        {
            style.marginTop = 2f;
            style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            style.height = 20;
        }

        private void BuildGeometry()
        {
            _name = new Label();
            _name.style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0f, 1f));
            _name.style.minWidth = 100;
            _name.style.flexGrow = 1;
            _name.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        }

        private void AddManipulators()
        {
            
        }
    }
}