using OerNovel.Runtime.Stories.Structure;

namespace OerNovel.Runtime.Graphs.Structure
{
    public partial class OerStoryGraph
    {
        /// <summary>
        /// The story Graph belongs to
        ///
        /// Runtime-only field
        /// </summary>
        public OerStory Story { get; private set; }
    }
}