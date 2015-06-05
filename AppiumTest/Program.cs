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
            Console.WriteLine("Enter the type of test <onclick> or <pushup> or <interstitial>:");
            Console.Write("$ ");
            string TypeTest = Console.ReadLine();
            AppiumDriver driver = new AppiumDriver(TypeTest);
            Console.WriteLine("You have selected " + TypeTest);
            Console.WriteLine("Test is running...");
            driver.Setup();
            driver.StartOnclick();
            Console.WriteLine("Тест выполнился!");

        }
    }
}
