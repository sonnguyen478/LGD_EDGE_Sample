using System;
using System.Collections;
//?/using Cvb;

namespace Interop.Common.CVB
{
    /// <summary>
    /// Represents a CV GenApi NodeMap
    /// </summary>
    public class NodeMap
    {
        //- iCVGenApi  ,  iCVCDriver ÂüÁ¶

        Cvb.GenApi.NODEMAP _nodeMap = 0;
        Hashtable _nodes;
        public NodeMap(int CvbImage)
        {
            _nodes = new Hashtable();
            // Get Noadmap from Camera
            if (Cvb.Driver.INodeMapHandle.CanNodeMapHandle((Cvb.Image.IMG)CvbImage))
            {
                Cvb.Driver.INodeMapHandle.NMHGetNodeMap((Cvb.Image.IMG)CvbImage, out _nodeMap);
            }

        }
        public NodeMap(long CvbImage)
        {
            _nodes = new Hashtable();
            // Get Noadmap from Camera
            if ( Cvb.Driver.INodeMapHandle.CanNodeMapHandle( (Cvb.Image.IMG)CvbImage ) )
            {
                Cvb.Driver.INodeMapHandle.NMHGetNodeMap( (Cvb.Image.IMG)CvbImage, out _nodeMap );
            }

        }
        ~NodeMap()
        {
            // Clear the saved Nodes
            _nodes.Clear();

            // Release the Nodemap
            Cvb.Image.OBJ nodemapObject = _nodeMap.ToIntPtr();
            Cvb.Image.ReleaseObject(ref nodemapObject);
        }

        /// <summary>
        /// Get a GenApi Node
        /// </summary>
        public Node GetNode(string NodeName)
        {
            if (!_nodes.ContainsKey(NodeName))
            {
                _nodes.Add(NodeName, new Node(_nodeMap, NodeName));
            }
            return (Node)_nodes[NodeName];
        }

        /// <summary>
        /// Get a GenApi Node
        /// </summary>
        public Node this[string NodeName]
        {
            get
            {
                return GetNode(NodeName);
            }
        }

        /// <summary>
        /// Check if this Node is available
        /// </summary>
        public bool IsNode(string NodeName)
        {
            GetNode(NodeName);
            return Cvb.GenApi.IsNode(this[NodeName]);
        }

        public static implicit operator Cvb.GenApi.NODEMAP(NodeMap nodemap)
        {
            return nodemap._nodeMap;
        }

        public static implicit operator int(NodeMap nodemap)
        {
            return nodemap._nodeMap;
        }
    }
}
