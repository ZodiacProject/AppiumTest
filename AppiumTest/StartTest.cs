using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumTest
{
    public class StartTest
    {
        private const int _onclick_SuiteID = 65;
        private const int _pushup_SuiteID = 89;
        private string _alreadyTest = "";
        private string _run = "";
        public string TypeTest { get; private set; }
        public StartTest()
        {
            TestRail testrail = new TestRail();
            testrail.GetRunsProject();
            Console.WriteLine("\nEnter the type of test: <onclick>, <pushup>, <interstitial>:");
            Console.Write("$ ");
            TypeTest = Console.ReadLine();
                while (!CorrectNameTypeTest(TypeTest))
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect type of test, plese try again: ");
                    TypeTest = Console.ReadLine();
                    if (TypeTest == "exit")
                        return;
                }
            Console.WriteLine("You have selected [ " + TypeTest + " ]");           
            Console.Write("Do you want create a test (y/n): ");
            _alreadyTest = Console.ReadLine();

            AppiumDriver driver = new AppiumDriver(TypeTest, testrail);
            if (_alreadyTest == "y")
            {                
                Console.WriteLine("\nPlease, enter name of run-test:");
                Console.Write("Name: ");
                string nameSuite = Console.ReadLine();
                driver.Setup();
                switch (TypeTest)
                {
                    case "onclick":
                        testrail.GetSuiteID = _onclick_SuiteID;
                        testrail.CreateRun(nameSuite);
                        driver.StartOnclick();
                        break;
                    case "pushup":
                        testrail.GetSuiteID = _pushup_SuiteID;
                        testrail.CreateRun(nameSuite);
                        driver.StartPushup();
                        break;
                    default: break;
                }
            }
            else
            {
                Console.Write("Input run ID: ");
                _run = Console.ReadLine();
                Console.WriteLine("Test is running...");
                driver.Setup();
                switch (TypeTest)
                {
                    case "onclick":
                        testrail.RunID = _run;
                        testrail.GetSuiteID = _onclick_SuiteID;
                        driver.StartOnclick();
                        break;
                    case "pushup":
                        testrail.RunID = _run;
                        testrail.GetSuiteID = _pushup_SuiteID;
                        driver.StartPushup();
                        break;
                    default: break;
                }
            }

        }
        private bool CorrectNameTypeTest (string str)
        {
            switch (str)
            {
                case "onclick": return true;
           
                case "pushup": return true;

                default:return false;
            }                
        }
    }
}
