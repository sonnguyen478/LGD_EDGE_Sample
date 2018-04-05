using System;


namespace Interop.Common.CVB
{
    /// <summary>
    /// Represents a Node from the CV GenApi. 
    /// </summary>
    public class Node
    {

        //- iCVGenApi ÂüÁ¶
        private Cvb.GenApi.NODE _node = 0;

        public Node(Cvb.GenApi.NODEMAP NodeMap, string NodeName)
        {
           Cvb. GenApi.NMGetNode(NodeMap, NodeName, out _node);
        }

        ~Node()
        {
            // Release the Nodemap
            Cvb.Image.OBJ nodeObject = _node.ToIntPtr();
            Cvb.Image.ReleaseObject(ref nodeObject);
        }

        public static implicit operator Cvb.GenApi.NODE(Node node)
        {
            return node._node;
        }
    }
}
