using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDInjector
{
    class Program
    {
        static void Main(string[] args)
        {
            LDR lDR = new LDR("csgo", "test.dll", "DLLMain");
            Console.WriteLine(lDR.Injecting().ToString());
            Console.ReadLine();
        }
    }
}