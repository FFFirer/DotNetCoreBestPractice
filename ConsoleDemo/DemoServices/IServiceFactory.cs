using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDemo.DemoServices
{
    public interface IServiceFactory
    {
        internal ICommonService Create(string className);
    }
}
