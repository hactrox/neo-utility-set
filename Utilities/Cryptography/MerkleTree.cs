using System;
using System.Linq;

namespace Utilities.Cryptography
{
    public class MerkleTree
    {
        /// <summary>
        /// The root.
        /// </summary>
        private MerkleTreeNode root;
        public int Depth { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Utilities.Cryptography.MerkleTree"/> class.
        /// </summary>
        /// <param name="hashes">Hashes.</param>
        public MerkleTree(UInt256[] hashes)
        {
            if (hashes.Length == 0) throw new ArgumentException();
            root = Build(hashes.Select(p => new MerkleTreeNode() { Hash = p }).ToArray());
            for (MerkleTreeNode i = root; i != null; i = i.LeftNode)
            {
                Depth++;
            }
            this.Depth = Depth;
            Console.WriteLine("MerkleTree root hash:{0}", root.Hash);
        }

        /// <summary>
        /// Build the specified leaves.
        /// </summary>
        /// <returns>The build.</returns>
        /// <param name="leaves">Leaves.</param>
        private MerkleTreeNode Build(MerkleTreeNode[] leaves)
        {
            if (leaves.Length == 0) throw new ArgumentException();
            if (leaves.Length == 1) return leaves[0];
            MerkleTreeNode[] parent = new MerkleTreeNode[(leaves.Length + 1) / 2];
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = new MerkleTreeNode();
                parent[i].LeftNode = leaves[i * 2];
                leaves[i * 2].Parent = parent[i];
                if (i * 2 + 1 == leaves.Length)
                {
                    parent[i].RightNode = parent[i].LeftNode;
                }
                else
                {
                    parent[i].RightNode = leaves[i * 2 + 1];
                    leaves[i * 2 + 1].Parent = parent[i];
                }
                parent[i].Hash = new UInt256(Crypto.Default.Hash256(parent[i].LeftNode.Hash.ToArray().Concat(parent[i].RightNode.Hash.ToArray()).ToArray()));
            }
            return Build(parent);
        }
    }
}
