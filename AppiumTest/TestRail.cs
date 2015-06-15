using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gurock.TestRail;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Internal;

namespace AppiumTest
{
    public class TestRail
    {
        private APIClient client = new APIClient("https://propeller.testrail.net");
        private const string _login = "stepanov.guap@gmail.com";
        private const string _password = "302bis";
        private List<string> _createCases = new List<string>();
        private string _runID = null;
        private int _numberCase;
        private int _suiteId;
        private string _alreadyRun = "";
        public string RunID {set { _runID = value; } }
        public int GetSuiteID { get { return _suiteId; } set { _suiteId = value; } }

        private Dictionary<string, List<string>> _testRun;
        public void StartTestRail()
        {
            int created_on = 0;
            client.User = _login;
            client.Password = _password;
            _testRun = new Dictionary<string, List<string>>();
            JArray Sections = (JArray)client.SendGet("get_sections/3&suite_id=" + _suiteId);
            //foreach (var s in Sections)
            //{
            //    Console.WriteLine(s.ToString());
            //}
            JArray Runs = (JArray)client.SendGet("get_runs/3&suite_id=" + _suiteId);
            if (String.IsNullOrEmpty(_runID))
            {
                foreach (var run in Runs)
                {
                    if (Convert.ToBoolean(run["is_completed"]) == false)
                        if (Convert.ToInt32(run["created_on"]) > created_on)
                        {
                            created_on = Convert.ToInt32(run["created_on"]);
                            _runID = run["id"].ToString();
                        }
                }
            }
            JArray TestCases = (JArray)client.SendGet("get_tests/" + _runID);
            _numberCase = TestCases.Count / Sections.Count;
            foreach (var section in Sections)
            {
                _testRun.Add(section["name"].ToString(), new List<string>());
             //   Console.WriteLine(TestCases.Count);
                //return;
                foreach (var testCase in TestCases.Take(_numberCase))
                {
                    //Console.WriteLine(test["id"] + " " + test["title"]);
                    try
                    {
                        _testRun[section["name"].ToString()].Add(testCase["id"].ToString());
                     //   (_testRun["name"].ToString()).Add(testCase["id"].ToString());
                      //       Console.WriteLine(section["name"] + " " + testCase["title"]);
                    }
                    catch (ArgumentException) { }
                }
                for (int i = _numberCase - 1; i >= 0; i--)
                    TestCases.RemoveAt(i);

            }
            //  foreach (KeyValuePair<string, List<string>> testrun in TestRun)
            //    foreach (string value in testrun.Value)
            //Console.WriteLine("name = {0}, id = {1}", testrun.Key, value);
        }

        public void CreateRun(string nameSuite)
        {
                client.User = _login;
                client.Password = _password;
                JArray caseData = (JArray)client.SendGet("get_cases/3/&suite_id=" + _suiteId);
                foreach (var c in caseData)
                    _createCases.Add(c["id"].ToString());
                var runData = new Dictionary<string, object>
                {
                    {"suite_id", _suiteId},
                    {"name", nameSuite},
                    {"include_all", true},
                    {"description", "Автоматическое тестирование OnClick/Pushup для мобильных браузеров: Chrome, FireFox, Opera, IE, Safari"},
                    {"case_ids", _createCases.ToArray()},
                };

                JObject runCreate = (JObject)client.SendPost("add_run/3", runData);
                Console.WriteLine("Test run is create.");
                Console.WriteLine("Test is running...");
                _alreadyRun = runCreate["id"].ToString();
      
        }


        public List<string> GetRunCase(AndroidDriver driver)
        {
            List<string> TCases = new List<string>();
            foreach (KeyValuePair<string, List<string>> testrun in _testRun)
                foreach (string value in testrun.Value)
                {   
                    // Необходимо придумать как выбирать нужный тест
                    //  Console.WriteLine(driver.GetType().Name);
                    //if (driver.GetType().Name.Contains(testrun.Key))
                        TCases.Add(value);
                }
            return TCases;
        }
        public void SetStatus(string caseID, int statusID, string resultMessage, string commentMessage)
        {
            client.User = _login;
            client.Password = _password;

            var addResultData = new Dictionary<string, object>
            {

                {"status_id", statusID}, 
                {"comment", resultMessage},
                {"custom_comment_test", commentMessage}
            };
            JObject r = (JObject)client.SendPost("add_result/" + caseID, addResultData);
        }
        public void CloseRun()
        {

            client.User = _login;
            client.Password = _password;
            var closeData = new Dictionary<string, object>
            {
                {"description", "Тест проведен и закрыт"}
            };
            try
            {

                JObject c = (JObject)client.SendPost("close_run/" + _runID, closeData);
                Console.WriteLine("Test run " + _runID + " successfully removed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void GetRunsProject()
        {
            Console.WriteLine("Актуальные проекты:");
            client.User = _login;
            client.Password = _password;
               JArray Runs = (JArray)client.SendGet("get_runs/3");
            foreach (var run in Runs)
            {
                if (Convert.ToBoolean(run["is_completed"]) == false)
                    Console.WriteLine("ID:" + run["id"].ToString() + "\tName:" + run["name"]);
            }
        }
        public void GetSuitesOfProject()
        {
            client.User = _login;
            client.Password = _password;
            JArray SuiteData = (JArray)client.SendGet("get_suites/3");
            Console.WriteLine("ID\tName");
            foreach (var suite in SuiteData)
               // if (!suite["name"].ToString().Contains("Desktop"))
                    Console.WriteLine(" " + suite["id"] + "\t" + suite["name"]);
        }
    }


}
