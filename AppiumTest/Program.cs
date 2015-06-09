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
            Console.WriteLine("Enter the type of test: <onclick>, pushup>, <interstitial>:");
            Console.Write("$- ");
            string TypeTest = Console.ReadLine();
            AppiumDriver driver = new AppiumDriver(TypeTest);
            Console.WriteLine("You have selected [ " + TypeTest + " ]");
            Console.WriteLine("Test is running...");
            driver.Setup();
            switch (TypeTest)
            {
                case "onclick": driver.StartOnclick();
                    break;
                case "pushup": driver.StartPushup();
                    break;
                default: Console.WriteLine("[ERROR] Incorrect type test. Try again");
                    break;
            }  
            Console.WriteLine("Тест выполнился!");

        }
    }
}
