using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes
{
    /// <summary>
    /// The main idea of a 'flow' node is it's ability to be 'resolved'.
    /// Resolving means specific action needed to be complete until node can be left.
    ///
    /// Also, a Flow node has a special port type - Flow ports. They do nothing, does not return any value, but are used by graph resolved
    /// to determine resolvable nodes geometry
    ///
    /// Sometimes, results of a node resolvation my affect which node is considered "next" by flow.
    /// </summary>
    [Serializable]
    public abstract class OerFlowNode : OerResolvableGraphNode
    {
        /// <summary>
        /// If this string is not null, ResolvableGraph marks this node as a starting node and adds it to the special
        /// dictionary using this string as a key
        /// </summary>
        public abstract string StartingNodeKey { get; }
        
        /// <summary>
        /// This method is called whenever GraphResolver enters this node
        ///
        /// May return nothing and may return some resolvation payload
        /// This resolvation payload is aslo passed into GetNextNode method in case it requires information about resolvation outcome
        /// </summary>
        public abstract object Resolve();

        /// <summary>
        /// Returns next node (may vary depending on resolvation results)
        /// May return null in case, the resolvation path is finished
        /// Getting null from this says GraphResolver that resolvation of graph is finished
        /// </summary>
        /// <param name="resolvationPayload"></param>
        /// <returns></returns>
        public abstract OerFlowNode GetNextNode(object resolvationPayload);
    }
}