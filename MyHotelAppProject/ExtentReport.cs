using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using AventStack.ExtentReports;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace SeleniumTests
{
    [TestFixture]
    public class ExtentReport:HotelApp_BusinessFunctions
    {
        private new IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        public ExtentReports extent;
        public ExtentTest test;

        [OneTimeSetUp]
        public void StartReport()
        {
            string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string actualPath = filepath.Substring(0, filepath.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            string reportPath = projectPath + "TestReports\\SampleExtentReport.html";
            
            //var dir = TestContext.CurrentContext.TestDirectory + "\\";
            //var fileName = this.GetType().ToString() + ".html";

            var htmlReporter = new ExtentHtmlReporter(reportPath);
            
            // make the charts visible on report open
            htmlReporter.Configuration().ChartVisibilityOnOpen = true; 
            htmlReporter.LoadConfig(projectPath + "Configuration\\extent-config.xml");

            extent = new ExtentReports();

            extent.AddSystemInfo("Host Name", "Adactin");                
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User Name", "adactin123");

            extent.AttachReporter(htmlReporter);            

        }

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();            
            verificationErrors = new StringBuilder();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();           
            extent.RemoveTest(test);
        }
        
        [TearDown]
        public void TeardownTest()
        {

            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
                var errorMessage = TestContext.CurrentContext.Result.Message;

                if(status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    test.Log(Status.Fail, stackTrace + errorMessage);
                }
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void MyPassingTest ()
        {
            test = extent.CreateTest("MyPassingTest","Test with valid user");
            
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["sAppURL"]);
            driver.Manage().Window.Maximize();            
            HA_BF_Login(driver, "adactin123", "adactin123");
            new SelectElement(driver.FindElement(By.Id(ConfigurationManager.AppSettings["Lst_SearchHotel_Location"]))).SelectByText("Sydney");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Btn_SearchHotel_Search"])).Click();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Rad_SelectHotel_RadioButton_1"])).Click();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Btn_SelectHotel_Continue"])).Click();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_FirstName"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_FirstName"])).SendKeys("test");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_LastName"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_LastName"])).SendKeys("test");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_Address"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_Address"])).SendKeys("test");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_CCNumber"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_CCNumber"])).SendKeys("1212121212121212");
            new SelectElement(driver.FindElement(By.Id(ConfigurationManager.AppSettings["Lst_BookingHotel_CCType"]))).SelectByText("American Express");
            new SelectElement(driver.FindElement(By.Id(ConfigurationManager.AppSettings["Lst_BookingHotel_CCExpMonth"]))).SelectByText("March");
            new SelectElement(driver.FindElement(By.Id(ConfigurationManager.AppSettings["Lst_BookingHotel_CCExpYear"]))).SelectByText("2015");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_CCCvvNumber"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_CCCvvNumber"])).SendKeys("111");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Btn_BookingHotel_BookNow"])).Click();
            driver.FindElement(By.LinkText(ConfigurationManager.AppSettings["Lnk_BookingHotel_Logout"])).Click();
            driver.FindElement(By.LinkText(ConfigurationManager.AppSettings["Lnk_Logout_ClickHeretoLoginAgain"])).Click();

            test.Log(Status.Pass, "Test passed as it is a valid user");        
        }

        [Test]
        public void MyFailingTest()
        {
            test = extent.CreateTest("MyFailingTest", "Test with invalid user");
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["sAppURL"]);
            driver.Manage().Window.Maximize();           
            HA_BF_Login(driver, "InvalidUser", "InvalidPassword");
            new SelectElement(driver.FindElement(By.Id(ConfigurationManager.AppSettings["Lst_SearchHotel_Location"]))).SelectByText("Sydney");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Btn_SearchHotel_Search"])).Click();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Rad_SelectHotel_RadioButton_1"])).Click();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Btn_SelectHotel_Continue"])).Click();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_FirstName"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_FirstName"])).SendKeys("test");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_LastName"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_LastName"])).SendKeys("test");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_Address"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_Address"])).SendKeys("test");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_CCNumber"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_CCNumber"])).SendKeys("1212121212121212");
            new SelectElement(driver.FindElement(By.Id(ConfigurationManager.AppSettings["Lst_BookingHotel_CCType"]))).SelectByText("American Express");
            new SelectElement(driver.FindElement(By.Id(ConfigurationManager.AppSettings["Lst_BookingHotel_CCExpMonth"]))).SelectByText("March");
            new SelectElement(driver.FindElement(By.Id(ConfigurationManager.AppSettings["Lst_BookingHotel_CCExpYear"]))).SelectByText("2015");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_CCCvvNumber"])).Clear();
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_CCCvvNumber"])).SendKeys("111");
            driver.FindElement(By.Id(ConfigurationManager.AppSettings["Btn_BookingHotel_BookNow"])).Click();
            driver.FindElement(By.LinkText(ConfigurationManager.AppSettings["Lnk_BookingHotel_Logout"])).Click();
            driver.FindElement(By.LinkText(ConfigurationManager.AppSettings["Lnk_Logout_ClickHeretoLoginAgain"])).Click();

            test.Log(Status.Pass, "Test failed as it is an invalid user");

        }

            private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
