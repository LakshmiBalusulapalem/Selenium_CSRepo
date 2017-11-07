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
    public class HA_TestSuite
    {      

        [Test]
        public void MasterTestSuite()
        {           
                //Instantiating Object for each of the tests


                SeleniumTests.CalledByTestSuiteTest bp1 = new SeleniumTests.CalledByTestSuiteTest();

                // More tests can be added in similar fashion
                // VerificationPointTest bp2 = new VerificationPointTest();
                //SynchronizationTest bp3 = new SynchronizationTest();
                //SharedUIMapTest bp4 = new SharedUIMapTest();

                // Defining which tests to run

                Boolean bCalledbyTestSuiteTest = true;

            // More variables can be added in similar fashion for other tests
            // Boolean bVerificationPointTest = false;
            // Boolean bSynchronizationTest = false;
            // Boolean bSharedUIMapTest = false;

            //Call and run tests


            if (bCalledbyTestSuiteTest)
                bp1.CalledByMasterTestSuite();            

        }
       
    }
}
