using System;

namespace ExcelTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RsmExcelTool tool = new RsmExcelTool(@"D:\Watt\Projects_temp\Logs\excels", @"D:\Watt\Projects_temp\Logs\excels\result.xlsx");
            tool.MergeExcelFiles();
        }
    }
}
