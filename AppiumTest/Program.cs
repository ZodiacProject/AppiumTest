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
            Console.WriteLine("Запуск теста...");
            AppiumDriver driver = new AppiumDriver();
            driver.Setup();
            driver.OpenHofHomePage();
            Console.WriteLine("Тест выполнился!");

        }
    }
}
