using System;
using UnityEditor.Experimental.GraphView;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    /// <summary>
    /// Editor port class
    /// </summary>
    public class NPortView : Port
    {
        protected NPortView(Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type) : base(portOrientation, portDirection, portCapacity, type)
        {
        }
    }
}