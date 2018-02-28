using System;
using System.Collections;
using System.Linq;
using Utilities;
using Utilities.Cryptography;

namespace MerkleTreeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] byte1 = "0xf0b91028bfd28c0c0fd535d3b6815061e9664cf1598e5aa09f6cdbf94f9d8653".HexToBytes();
            byte[] byte2 = "0xaab91028bfd28c0c0fd535d3b6815061e9664cf1598e5aa09f6cdbf94f9d8653".HexToBytes();
            //byte[] byte3 = "0xa0b9a028bfd28c0c0fd535d3b6815061e9664cf1598e5aa09f6cdbf94f9d8653".HexToBytes();

            UInt256 u1 = new UInt256(byte1);
            UInt256 u2 = new UInt256(byte2);
            //UInt256 u3 = new UInt256(byte3);
            UInt256[] us = new UInt256[] { u1, u2 };

            MerkleTree tree = new MerkleTree(us.ToArray());
            UInt256 u4 = new UInt256(Crypto.Default.Hash256(u1.ToArray().Concat(u2.ToArray()).ToArray()));
            //UInt256 u5 = new UInt256(Crypto.Default.Hash256(u3.ToArray().Concat(u3.ToArray()).ToArray()));
			//UInt256 u6 = new UInt256(Crypto.Default.Hash256(u4.ToArray().Concat(u5.ToArray()).ToArray()));

            Console.WriteLine("u4:{0}", u4);
            //Console.WriteLine("u5:" + u5);
            //Console.WriteLine("u6:" + u6);
            Console.WriteLine("Depth:{0}", tree.Depth);
        }
    }
}
