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
            //Console.Write("Windows score ");
            //int WindowScore = int.Parse(Console.ReadLine());
            //Console.Write("The duration of the show ");
            //int TimeShow = 1000 * (int.Parse(Console.ReadLine()));
            Console.WriteLine("Запуск теста...");
            AppiumDriver driver = new AppiumDriver();
            driver.Setup();
            driver.StartOnclick();
            Console.WriteLine("Тест выполнился!");

        }
    }
}
