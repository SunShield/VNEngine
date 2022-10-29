using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using UnityEditor.Experimental.GraphView;

namespace OerGraph.Editor.Graphs.Systems.Connecting
{
    public static class OerCompatiblePortsGettter
    {
        private static List<Port> _compatiblePorts = new();

        public static List<Port> GetCompatiblePorts(OerPortView portView, OerGraphView graphView)
        {
            _compatiblePorts.Clear();
            if (portView.direction == Direction.Input)
            {
                foreach (var outputPort in graphView.OutputPorts.Values)
                {
                    if (!portView.IsCompatibleWith(outputPort)) continue;
                    
                    _compatiblePorts.Add(outputPort);
                }
            }
            else
            {
                foreach (var inputPort in graphView.InputPorts.Values)
                {
                    if (!portView.IsCompatibleWith(inputPort)) continue;
                    
                    _compatiblePorts.Add(inputPort);
                }
            }

            return _compatiblePorts;
        }
    }
}