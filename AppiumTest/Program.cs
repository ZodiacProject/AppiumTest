﻿using System;
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
            AppiumDriver driver = new AppiumDriver();
            driver.Setup();
            Console.WriteLine("Тест выполняется!");
            driver.OpenHofHomePage();
         //   driver.CloseDriver();
        }
    }
}