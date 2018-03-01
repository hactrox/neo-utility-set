using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace MerkleTree
{
    class Program
    {
        private static readonly System.Security.Cryptography.SHA256 Sha256 = System.Security.Cryptography.SHA256.Create();

        static void Main(string[] args)
        {
            Console.WriteLine("Input txid line by line, end with string 'end':");

            var txidArray = new List<UInt256>();
            var txid = string.Empty;

            var index = 1;
            Console.Write("txid{0}: ", index++);
            while ((txid = Console.ReadLine()) != "end")
            {
                Console.Write("txid{0}: ", index++);
                txidArray.Add(new UInt256(txid.HexToBytes().Reverse().ToArray()));
            }
            Console.WriteLine(Environment.NewLine);

            var rootHash = CalculateMerkleRootHash(txidArray.Select(x=>x.ToArray()).ToList());
            Console.WriteLine("Calculated merkle root hash:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new UInt256(rootHash));
            Console.ResetColor();
            var merkleTree = new Utilities.Cryptography.MerkleTree(txidArray.ToArray());
        }

        static byte[] CalculateMerkleRootHash(IList<byte[]> txidArray)
        {
            if (txidArray.Count == 0) return new byte[]{};
            if (txidArray.Count == 1) return txidArray[0];

            var parentHashArray = new List<byte[]>();
            for (var i = 0; i < (txidArray.Count+1)/2; i++)
            {
                var leftChild = txidArray[i * 2];
                var rightIndex = i * 2 + 1;
                var rightChild = rightIndex == txidArray.Count ? txidArray[i * 2] : txidArray[rightIndex];
                var data = leftChild.ToArray().Concat(rightChild.ToArray()).ToArray();

                parentHashArray.Add(CalculateHash(data));
            }

            return CalculateMerkleRootHash(parentHashArray);
        }

        static byte[] CalculateHash(byte[] data)
        {
            var dataHash = Sha256.ComputeHash(data);
            dataHash = Sha256.ComputeHash(dataHash);
            return dataHash;
        }
    }
}
