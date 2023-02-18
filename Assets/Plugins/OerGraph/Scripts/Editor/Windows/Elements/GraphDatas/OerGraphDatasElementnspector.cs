using OerGraph.Runtime.Unity.Data;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements.GraphDatas
{
    public class OerGraphDatasElementnspector : VisualElement
    {
        private OerGraphData _data;
        private Label _dataName;
        private Button _removeButton;

        public OerGraphDatasElementnspector(OerGraphData data)
        {
            _data = data;
            BuildGeometry();
        }

        private void BuildGeometry()
        {
            
        }
    }
}