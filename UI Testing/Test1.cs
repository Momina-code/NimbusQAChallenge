using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NimbusQATest
{
    public class Test1
    {
        private TestContext testContextInstance;
        private IWebDriver driver;
        public string appURL;
        string staffURL;
        string username;
        string password;
         
        const int ELEMENT_TIMEOUT = 10;   //Standard Timeout for all elements
        const int PAGE_LOAD_TIMEOUT = 15;   //Standard Timeout for all elements
        private static Random random = new Random();


        [Test]
        public void UIAutomationTest()
        {
            LoginTest();
            GotoStaffManagementTest();
        }
        
        private void LoginTest()
        {
            driver.Navigate().GoToUrl(appURL);
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(ELEMENT_TIMEOUT));

            wait.Until(ExpectedConditions.ElementExists(By.Id("tbUsername")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("tbPassword")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("btnLogin_btn_btnLogin")));

            IWebElement elogin = driver.FindElement(By.Id("tbUsername"));
            IWebElement epassword = driver.FindElement(By.Id("tbPassword"));
            elogin.SendKeys(username);
            epassword.SendKeys(password);

            driver.FindElement(By.Id("btnLogin_btn_btnLogin")).Click();
            Assert.AreEqual(driver.Title, "Home", "Login Failed");
        }

        
        private void GotoStaffManagementTest()
        {
            driver.Navigate().GoToUrl(staffURL);            
            Assert.AreEqual(driver.Title, "Staff Management");        
        }


        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [SetUp]
        public void SetupTest()
        {
            appURL = "http://AutoTest.Time2Work.com";
            staffURL = appURL + "/Staff/User.aspx";

            username = "Momina@Time2Work.com";
            password = "@Momina";

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }


        }
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}

