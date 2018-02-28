using System;
namespace Utilities.Cryptography
{
    internal class MerkleTreeNode
    {
        public UInt256 Hash;

        public MerkleTreeNode LeftNode;
        public MerkleTreeNode RightNode;
        public MerkleTreeNode Parent;

        public bool IsLeaf => LeftNode == null && RightNode == null;

        public bool IsRoot => Parent == null;
    }
}
