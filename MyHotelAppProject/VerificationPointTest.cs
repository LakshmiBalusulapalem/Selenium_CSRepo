﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class VerificationPointTest
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
            driver = new FirefoxDriver();
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
        public void GetPageTitle()
        {
            driver.Navigate().GoToUrl(baseURL + "/HotelApp/");
            String pageTitle = driver.Title;
            if (pageTitle.Equals("AdactIn.com - Hotel Reservation System"))
                Console.WriteLine("Page Title is correct. Actual page title is " + pageTitle);
            else
                Console.WriteLine("Page Title is incorrect.");

        }
        
        [Test]
        public void VerificationPointTestCase() 
        {            
            driver.Navigate().GoToUrl(baseURL + "/HotelApp/");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("adactin123");
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("adactin123");
            driver.FindElement(By.Id("login")).Click(); 
            new SelectElement(driver.FindElement(By.Id("location"))).SelectByText("Sydney");
            new SelectElement(driver.FindElement(By.Id("hotels"))).SelectByText("Hotel Creek");
            new SelectElement(driver.FindElement(By.Id("room_type"))).SelectByText("Standard");
            new SelectElement(driver.FindElement(By.Id("child_room"))).SelectByText("1 - One");
            driver.FindElement(By.Id("Submit")).Click();

            String sLocation = driver.FindElement(By.XPath(".//*[@id='location_0']")).GetAttribute("value");
            if (sLocation.Equals("Sydney", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Searched location is correct on search page. Actual location is " + sLocation);
            }
            else
            {
                Console.WriteLine("Searched location is incorrect. Actual location is " + sLocation);
            }           
             
            driver.FindElement(By.Id("radiobutton_0")).Click();
            driver.FindElement(By.Id("continue")).Click();
            
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
            new SelectElement(driver.FindElement(By.Id("cc_exp_year"))).SelectByText("2016");
            new SelectElement(driver.FindElement(By.Id("cc_exp_year"))).SelectByText("2018");
            driver.FindElement(By.Id("cc_cvv")).Clear();
            driver.FindElement(By.Id("cc_cvv")).SendKeys("214");
            driver.FindElement(By.Id("book_now")).Click();
            try
            {                
                Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));
            }
            catch (AssertionException e)
            {                
                verificationErrors.Append(e.Message);
            }

        }
        [Test]
        public void VerifyErrorMessage()
        {
            driver.Navigate().GoToUrl(baseURL + "/HotelApp/");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("adactin123");
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("adactin123");
            driver.FindElement(By.Id("login")).Click();
            //new SelectElement(driver.FindElement(By.Id("location"))).SelectByText("Sydney");
            new SelectElement(driver.FindElement(By.Id("hotels"))).SelectByText("Hotel Creek");
            new SelectElement(driver.FindElement(By.Id("room_type"))).SelectByText("Standard");
            new SelectElement(driver.FindElement(By.Id("child_room"))).SelectByText("1 - One");
            driver.FindElement(By.Id("Submit")).Click();

          
            String sLocationFieldError = driver.FindElement(By.XPath(".//*[@id='location_span']")).Text;
            if (sLocationFieldError.Equals("Please Select a Location", StringComparison.InvariantCultureIgnoreCase))
                Console.WriteLine("Mandatory Error check for Location field passed. Actual Location Field Error is " + sLocationFieldError);
            else
                Console.WriteLine("Mandatory Error check for Location field failed. Actual Location Field Error is " + sLocationFieldError);
 
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
