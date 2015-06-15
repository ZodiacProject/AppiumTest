using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumTest
{
    class Program
    {
        static void Main(string[] args)
        {         
            StartTest startTest = new StartTest();
            if (startTest.TypeTest == "exit")
                Console.WriteLine("Test canceled!");
            else
                Console.WriteLine("Тест выполнился!");

        }
    }
}
