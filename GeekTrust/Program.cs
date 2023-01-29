using System;
using System.IO;

namespace GeekTrust
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //args[0] = "sample_input/input2.txt";
                string[] inputData = File.ReadAllLines(args[0]);
                //Add your code here to process the input commands
                Ledger ledger = new Ledger();
                foreach (var data in inputData)
                {
                    Command cmd = CommandFactory.GetCommandType(data);
                    string output = ledger.Process(cmd);
                    if(output != string.Empty)
                        Console.WriteLine(output);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
