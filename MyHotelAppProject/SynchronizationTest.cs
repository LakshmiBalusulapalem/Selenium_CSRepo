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

namespace SeleniumTests
{
    [TestFixture]
    public class SynchronizationTest:HotelApp_BusinessFunctions
    {
        private new IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {           
            driver = new FirefoxDriver();
            verificationErrors = new StringBuilder();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
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
        public void MySynchronizationTest ()
        {
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

            //Step added after Book Now button click  
            Thread.Sleep(10);
            String strOrderNo = driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_BookingHotel_OrderNo"])).GetAttribute("value");
            Console.WriteLine("Order Number generated is " + strOrderNo);

            driver.FindElement(By.LinkText(ConfigurationManager.AppSettings["Lnk_BookingHotel_Logout"])).Click();
            driver.FindElement(By.LinkText(ConfigurationManager.AppSettings["Lnk_Logout_ClickHeretoLoginAgain"])).Click();
            
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
