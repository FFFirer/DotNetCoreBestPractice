using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDemo.DemoServices
{
    internal interface ICommonService
    {
        void TestConsole();
        void TestConsole(string msg);
        void TestSnapshot();
    }
}
