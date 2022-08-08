using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Elements.Ports;

namespace VNEngine.Editor.Graphs.Systems.PortCompatibility
{
    public class PortCompatibilityChecker
    {
        private List<Port> _compatiblePorts = new();

        public List<Port> GetCompatiblePorts(NPortView portView, NGraphView graphView)
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