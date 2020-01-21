using System;

namespace DatabaseScriptTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            RsmTool.AddStationEng(500000, 1000);
            Console.WriteLine("Stop");
        }
    }
}
