using System;
using System.Security.Cryptography;
using Utilities;
using Utilities.Cryptography;

namespace SmartContract
{
    public static class SmartContract
    {
        static readonly SHA256 sha256 = SHA256.Create();
        static readonly RIPEMD160Managed ripemd160 = new RIPEMD160Managed();

        public static void GetScriptHash(string avmPath)
        {
            if (string.IsNullOrEmpty(avmPath))
            {
                Console.WriteLine("Incorrect avm path.");
                return;
            }

            byte[] avmBytes = System.IO.File.ReadAllBytes(avmPath);
            //string str = System.Text.Encoding.Default.GetString(bytes);

            var codeHex = avmBytes.BytesToHex();
            Console.WriteLine("code:\n{0}", codeHex);

            var scriptHashBytes = sha256.ComputeHash(avmBytes);
            scriptHashBytes = ripemd160.ComputeHash(scriptHashBytes);
            var scriptHash = new UInt160(scriptHashBytes);
            Console.WriteLine("scripthash:\n{0}", scriptHash);
        }
    }
}
