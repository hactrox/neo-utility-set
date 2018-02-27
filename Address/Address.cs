using System;
using System.Linq;
using Utilities;
using Utilities.Cryptography;
using Utilities.Cryptography.ECC;

namespace Address
{
    public static class Address
    {
        static System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create();
        static readonly RIPEMD160Managed ripemd160 = new RIPEMD160Managed();

        public static void ParseWIF()
        {
            Console.WriteLine("Input WIF:");
            string wif = Console.ReadLine();

            var wifBytes = Base58.Decode(wif);
            var wifHex = wifBytes.BytesToHex();
            Console.WriteLine("wif hex(len={0}):\n{1}",wifHex.Length/2 ,wifHex);

            var head = wifHex.Substring(0, 2);
            var privateKey = wifHex.Substring(2, wifHex.Length - 12);
            var tail = wifHex.Substring(wifHex.Length - 10, 2);
            var wifHash = wifHex.Substring(wifHex.Length - 8, 8);

            Console.WriteLine("Split wif hex:");
            Console.WriteLine("{0} {1} {2} {3}", head, privateKey, tail, wifHash);

            Console.WriteLine("private key(len={0}):\n{1}", privateKey.Length/2 ,privateKey);

            var currWifHashBytes = sha256.ComputeHash(sha256.ComputeHash((head + privateKey + tail).HexToBytes()));
            var currentWifhashShort = currWifHashBytes.BytesToHex().Substring(0, 8);

            Console.WriteLine("Hash of leading 34 bytes:{0}, expected:{1}", currentWifhashShort, wifHash);

            var publicKey = GetPublicKeyFromPrivatekey(privateKey);
            Console.WriteLine("public key hex:\n{0}", publicKey);

            var addrScript = "21" + publicKey + "ac";
            Console.WriteLine("addrscript:\n{0}", addrScript);

            var scriptHash = GetScriptHash(addrScript);
            Console.WriteLine("scripthash:\n{0}", scriptHash);

            var scriptHashHashBytes = sha256.ComputeHash(sha256.ComputeHash(("17" + scriptHash).HexToBytes()));
            var scriptHashHash = scriptHashHashBytes.BytesToHex();
            var scriptHashDecode = "17" + scriptHash + scriptHashHash.Substring(0, 8);
            Console.WriteLine("scriptHashDecode:\n{0}", scriptHashDecode);

            var addr = Base58.Encode(scriptHashDecode.HexToBytes());
            Console.WriteLine("addr:\n{0}", addr);
        }

        public static void GetScriptHashOfAddress()
        {
            Console.Write("Input address: ");
            var address = Console.ReadLine();
            byte[] data = address.Base58CheckDecode();
            if (data.Length != 21)
                throw new FormatException();
            if (data[0] != 23)
                throw new FormatException();
            var scriptHash = new UInt160(data.Skip(1).ToArray());
            Console.WriteLine("scriptHash: " + scriptHash);
        }
        static string GetPublicKeyFromPrivatekey(string privateKeyHexStr)
        {
            var privateKey = privateKeyHexStr.HexToBytes();
            var PublicKey = ECCurve.Secp256r1.G * privateKey;
            var publicKeyBytes = PublicKey.EncodePoint(true);
            return publicKeyBytes.BytesToHex();
        }

        static string GetScriptHash(string addrScriptHash)
        {
            var scriptHashBytes = addrScriptHash.HexToBytes();
            scriptHashBytes = sha256.ComputeHash(scriptHashBytes);
            scriptHashBytes = ripemd160.ComputeHash(scriptHashBytes);
            var scriptHashStr = scriptHashBytes.BytesToHex();
            return scriptHashStr;
        }
    }
}
