using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace SingleLTTests
{
        [TestClass]
        [TestCategory("ToDoTest")]
        public class MsTestSeleniumSample
        {
            public static string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME") == null ? "LT_USERNAME" : Environment.GetEnvironmentVariable("LT_USERNAME");
            public static string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY") == null ? "LT_ACCESS_KEY" : Environment.GetEnvironmentVariable("LT_ACCESS_KEY");
            public static string gridURL = "@hub.lambdatest.com/wd/hub";

          //  ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
             IWebDriver driver;
            public String browser;
            public String version;
            public String os;
            DesiredCapabilities capabilities = new DesiredCapabilities();

            [TestInitialize]
            public void Init()
            {
                capabilities.SetCapability("video",true);
                capabilities.SetCapability("visual",true);
                capabilities.SetCapability("network",true);
                capabilities.SetCapability("console",true);
                capabilities.SetCapability("terminal",true);

            }


            [DataTestMethod]
            [DataRow("chrome", "72.0", "Windows 10")]
            [TestMethod]

            public void Todotest(String browser, String version, String os)
            {
                String itemName = "Yey, Let's add it to list";

                capabilities.SetCapability("browserName", browser);
                capabilities.SetCapability("version", version);
                capabilities.SetCapability("platform", os);
                capabilities.SetCapability("user", LT_USERNAME);
                capabilities.SetCapability("accessKey", LT_ACCESS_KEY);
                capabilities.SetCapability("build", "LT ToDoApp using MsTest on LambdaTest");
                capabilities.SetCapability("name", "LT ToDoApp using MsTest on LambdaTest");

                driver = new RemoteWebDriver(new Uri("https://" + LT_USERNAME + ":" + LT_ACCESS_KEY + gridURL), capabilities, TimeSpan.FromSeconds(2000));
                driver.Url = "https://lambdatest.github.io/sample-todo-app/";
                Assert.AreEqual("Sample page - lambdatest.com", driver.Title);
                // Click on First Check box
                IWebElement firstCheckBox = driver.FindElement(By.Name("li1"));
                firstCheckBox.Click();

                // Click on Second Check box
                IWebElement secondCheckBox = driver.FindElement(By.Name("li2"));
                secondCheckBox.Click();

                // Enter Item name
                IWebElement textfield = driver.FindElement(By.Id("sampletodotext"));
                textfield.SendKeys(itemName);

                // Click on Add button
                IWebElement addButton = driver.FindElement(By.Id("addbutton"));
                addButton.Click();

                // Verified Added Item name
                IWebElement itemtext = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span"));
                String getText = itemtext.Text;
                Assert.IsTrue(itemName.Contains(getText));

                /* Perform wait to check the output */
                System.Threading.Thread.Sleep(2000);

                Console.WriteLine("LT_ToDo_Test Passed");
            }

            [TestCleanup]
            public void Cleanup()
            {
                if (driver != null)
                    driver.Quit();
             }
        }
}
