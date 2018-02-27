using System;

namespace Address
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+++NEO address utilities.+++\n");

            CommandHint();

            while (true)
            {
                var cmd = Console.ReadLine();
                OnCommand(cmd);
            }
        }

        static void CommandHint()
        {
            Console.WriteLine("help[h] - Show help info.");
            Console.WriteLine("exit - Exit program.");
            Console.WriteLine("wif - Generate address from wif.");
            Console.WriteLine("addrsc - Get address script hash in uint160 format.");
        }

        static void OnCommand(string cmd)
        {
            switch (cmd)
            {
                case"":
                    return;
                case "help":
                    CommandHint();
                    return;
                case "h":
                    CommandHint();
                    return;
                case "wif":
                    Address.ParseWIF();
                    return;
                case "exit":
                    Environment.Exit(0);
                    return;
                case "addrsc":
                    Address.GetScriptHashOfAddress();
                    return;
                default:
                    Console.WriteLine("error");
                    return;
            }
        }
    }
}
