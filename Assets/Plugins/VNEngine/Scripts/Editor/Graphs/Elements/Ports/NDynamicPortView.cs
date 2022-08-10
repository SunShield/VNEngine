using System;
using VNEngine.Editor.Service.Utilities;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Elements.Ports
{
    public class NDynamicPortView : NPortView
    {
        public NDynamicPortView(NGraphView graphView, string fieldName, INPort runtimePort, NPortType portType, Type type, Capacity portCapacity = Capacity.Single) 
            : base(graphView, fieldName, runtimePort, portType, type, portCapacity)
        {
            var newPortButton = UiElementsUtility.CreateButton("X", RemoveSelf);
            Insert(0, newPortButton);
        }

        private void RemoveSelf()
        {
            var parentTyped = parent as NDynamicPortsView;
            parentTyped.RemovePort(this);
        }
    }
}