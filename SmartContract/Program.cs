using System;

namespace SmartContract
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input fullpath of avm file: ");
            var avmPath = Console.ReadLine();

            SmartContract.GetScriptHash(avmPath);
        }
    }
}
