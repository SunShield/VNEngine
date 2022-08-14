using System;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    public class NDynamicPortView : NPortView
    {
        public NDynamicPortView(NGraphView graphView, string fieldName, INPort runtimePort, NPortType portType, Type type, Capacity portCapacity = Capacity.Single) 
            : base(graphView, fieldName, runtimePort, portType, type, portCapacity)
        {
            AddToClassList("n-dynamicPortView");
        }
    }
}