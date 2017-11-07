using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Configuration;
using System.Threading;
using NUnit.Framework;
using System.Diagnostics;

namespace SeleniumTests
{
    public class HotelApp_BusinessFunctions
    {
        public static IWebDriver driver;
        public void HA_BF_Login(IWebDriver driver, string sUsername, string sPassword)
        {            
                //Provide Username
                driver.FindElement(By.XPath(ConfigurationManager.AppSettings["Txt_Login_Username"])).Clear();
                driver.FindElement(By.XPath(ConfigurationManager.AppSettings["Txt_Login_Username"])).SendKeys(sUsername);

                //Provide Password
                driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_Login_Password"])).Clear();
                driver.FindElement(By.Id(ConfigurationManager.AppSettings["Txt_Login_Password"])).SendKeys(sPassword);

                //Click on Login button
                driver.FindElement(By.Id(ConfigurationManager.AppSettings["Btn_Login_Login"])).Click();               

        }


        //Function to dynamically wait for element presence
        public void HA_GF_WaitForElementPresent(IWebDriver driver, By by, int iTimeOut)
        {
            try
            {
                int iTotal = 0;
                int iSleepTime = 5000;
                while (iTotal < iTimeOut)
                {
                    IReadOnlyCollection<IWebElement> iWebElements = driver.FindElements(by);
                    if (iWebElements.Count > 0)
                        return;
                    else
                    {

                        Thread.Sleep(iSleepTime);
                        iTotal = iTotal + iSleepTime;
                        Console.Write(String.Concat("Waited for " + iTotal + " milliseconds " + by));

                    }
                }
            }
            catch (ElementNotVisibleException)
            {
                return;
            }            
        }

    }




}

