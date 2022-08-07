using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public class NPortViewConstructor
    {
        public NPortView Construct(INPort port, NNodeView nodeView, string name, NPortType type, NGraphView graphView)
        {
            var portView = new NPortView(graphView, port, name, type, port.Type);
            nodeView.AddPort(portView);
            graphView.AddPort(portView);
            return portView;
        }
    }
}