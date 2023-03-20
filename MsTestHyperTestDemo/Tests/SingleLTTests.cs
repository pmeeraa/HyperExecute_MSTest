using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace SingleLTTests
{
    [TestClass("chrome", "latest", "Windows 10")]
        [TestCategory("ToDoTest")]
        public class MsTestSeleniumSample
        {
            public static string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME") == null ? "LT_USERNAME" : Environment.GetEnvironmentVariable("LT_USERNAME");
            public static string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY") == null ? "LT_ACCESS_KEY" : Environment.GetEnvironmentVariable("LT_ACCESS_KEY");
            public static string gridURL = "@hub.lambdatest.com/wd/hub";

            ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
            public String browser;
            public String version;
            public String os;

            [TestInitialize]
            public void Init()
            {
                DesiredCapabilities capabilities = new DesiredCapabilities();
                capabilities.SetCapability(CapabilityType.BrowserName, browser);
                capabilities.SetCapability(CapabilityType.Version, version);
                capabilities.SetCapability(CapabilityType.Platform, os);
                capabilities.SetCapability("build", "[HyperTest] Selenium C# ToDo Demo1");

                capabilities.SetCapability("user", LT_USERNAME);
                capabilities.SetCapability("accessKey", LT_ACCESS_KEY);

                capabilities.SetCapability("video",true);
                  capabilities.SetCapability("visual",true);
                  capabilities.SetCapability("network",true);
                capabilities.SetCapability("console",true);
                      capabilities.SetCapability("terminal",true);

driver = new RemoteWebDriver(new Uri("https://" + LT_USERNAME + ":" + LT_ACCESS_KEY + gridURL), capabilities, TimeSpan.FromSeconds(2000));
                Console.Out.WriteLine(driver);
            }



            [TestMethod]
            public void Todotest()
            {
                String context_name = TestContext.CurrentContext.Test.Name + " on " + browser + " " + version + " " + os;
               // TC_Name = context_name;

                //_test = _extent.CreateTest(context_name);

                Console.WriteLine("Navigating to todos app.");
                driver.Value.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

                driver.Value.FindElement(By.Name("li4")).Click();
                Console.WriteLine("Clicking Checkbox");
                driver.Value.FindElement(By.Name("li5")).Click();

                /* If both clicks worked, then te following List should have length 2 */
                IList<IWebElement> elems = driver.Value.FindElements(By.ClassName("done-true"));

                /* so we'll assert that this is correct. */
                Assert.AreEqual(2, elems.Count);

                Console.WriteLine("Entering Text");
                driver.Value.FindElement(By.Id("sampletodotext")).SendKeys("Yey, Let's add it to list");
                driver.Value.FindElement(By.Id("addbutton")).Click();

                /* lets also assert that the new todo we added is in the list */
                string spanText = driver.Value.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span")).Text;
                Assert.AreEqual("Yey, Let's add it to list", spanText);
            }

            [ClassCleanup]
            protected void ExtentClose()
            {
                Console.WriteLine("OneTimeTearDown");
                //_extent.Flush();
            }
        }
}
