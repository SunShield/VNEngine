using OerGrap.Editor.Graphs.Elements.Ports;

namespace OerGraph.Editor.Graphs.Systems.PortCompatibility
{
    public static class OerPortCompatibilityChecker
    {
        public static bool CheckPortsCompatible(OerPortView port1, OerPortView port2)
        {
            var runtimePort1 = port1.RuntimePort;
            var runtimePort2 = port2.RuntimePort;

            return true;
        }
    }
}