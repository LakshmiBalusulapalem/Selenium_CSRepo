using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

namespace SeleniumTests
{
    [TestFixture]
    public class PopDialogTest
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            //service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            //driver = new FirefoxDriver(service);            
            //System.Environment.SetEnvironmentVariable("webdriver.gecko.driver", @"C:\Selenium_CS\MyHotelAppProject\packages\Selenium.WebDriver.GeckoDriver.Win64.0.16.0\driver\geckodriver.exe");
            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            //service.FirefoxBinaryPath = @"C:\Selenium_CS\MyHotelAppProject\packages\Selenium.Firefox.WebDriver.0.16.1\driver\geckodriver.exe";
            //FirefoxOptions options = new FirefoxOptions();
            //options.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);
            //TimeSpan t = TimeSpan.FromSeconds(10);

            driver = new FirefoxDriver();
            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            //service.FirefoxBinaryPath = @"C:\Selenium_CS\MyHotelAppProject\packages\Selenium.Firefox.WebDriver.0.16.1\driver\geckodriver.exe";
            //FirefoxOptions options = new FirefoxOptions();
            //TimeSpan time = TimeSpan.FromSeconds(10);
            //driver = new FirefoxDriver(service, options, time);
            //driver = new FirefoxDriver();
            baseURL = "http://adactin.com/";
            verificationErrors = new StringBuilder();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void MyPopDialogTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/HotelApp/");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("adactin123");
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("adactin123");
            driver.FindElement(By.Id("login")).Click();
            //Thread.Sleep(1000);
            new SelectElement(driver.FindElement(By.Id("location"))).SelectByText("Sydney");
            new SelectElement(driver.FindElement(By.Id("hotels"))).SelectByText("Hotel Creek");
            new SelectElement(driver.FindElement(By.Id("room_type"))).SelectByText("Standard");
            new SelectElement(driver.FindElement(By.Id("child_room"))).SelectByText("1 - One");
            driver.FindElement(By.Id("Submit")).Click();
            //driver.FindElement(By.Id("Submit")).Click();
            //Thread.Sleep(1000);
            driver.FindElement(By.Id("radiobutton_0")).Click();
            driver.FindElement(By.Id("continue")).Click();
            //Thread.Sleep(1000);
            driver.FindElement(By.Id("first_name")).Clear();
            driver.FindElement(By.Id("first_name")).SendKeys("sdsd");
            driver.FindElement(By.Id("last_name")).Clear();
            driver.FindElement(By.Id("last_name")).SendKeys("sdsd");
            driver.FindElement(By.Id("address")).Clear();
            driver.FindElement(By.Id("address")).SendKeys("dsdfvcfdf");
            driver.FindElement(By.Id("cc_num")).Clear();
            driver.FindElement(By.Id("cc_num")).SendKeys("1234567123456234");
            new SelectElement(driver.FindElement(By.Id("cc_type"))).SelectByText("American Express");
            new SelectElement(driver.FindElement(By.Id("cc_exp_month"))).SelectByText("March");            
            new SelectElement(driver.FindElement(By.Id("cc_exp_year"))).SelectByText("2011");
            driver.FindElement(By.Id("cc_cvv")).Clear();
            driver.FindElement(By.Id("cc_cvv")).SendKeys("214");
            driver.FindElement(By.Id("book_now")).Click();
            //After user clicks on Book Now            
            try
            {                
                string javascriptAlert = driver.SwitchTo().Alert().Text;
                Console.WriteLine(javascriptAlert); // Get text on alert box
                driver.SwitchTo().Alert().Accept();
                driver.FindElement(By.LinkText("Logout")).Click();
            }
            catch (NoAlertPresentException)
            {
                Console.WriteLine("No Alert");
            }             
            
            driver.FindElement(By.LinkText("Click here to login again")).Click();

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
