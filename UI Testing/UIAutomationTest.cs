using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;
using System.Threading;

namespace NimbusQATest
{
    public class UIAutomationTest
    {        
        private IWebDriver driver;
        private string appURL;

        private string staffURL;
        private string _username;
        private string _password;
        
        const int ELEMENT_TIMEOUT = 10;   //Standard Timeout for all elements        
        const int SLEEP_TIME = 2000;   //Standard Sleep Time
        private static Random random = new Random();


        [Test]
        public void AddStaffManagementTest_And_Location_Loop10_Success_Test()
        {
            Login(_username, _password);
            Assert.AreEqual(driver.Title, "Home", "Login Failed");

            for (int i = 0; i < 10; i++)
            {
                GotoStaffManagement();
                Assert.AreEqual(driver.Title, "Staff Management");

                Staff_Management_AddRecord();
                driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_btnSave_btn_btnSave_input")).Click();

                Location_clickLocation();
                Location_clickAddButton();
                Location_fillForm();

                //Click Save
                driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl04_PerformInsertButton")).Click();

            }


        }
        [Test]
        public void Login_Success_Test()
        {
            Login(_username, _password);
            Assert.AreEqual(driver.Title, "Home", "Login Failed");
        }

        [Test]
        public void Login_Fail_Test()
        {
            Login(_username, "wrongpass");
            Assert.AreNotEqual(driver.Title, "Home", "Login Failed");
        }

        [Test]
        public void Navigate_To_Staff_Management_Success_Test()
        {
            Login(_username, _password);
            Assert.AreEqual(driver.Title, "Home", "Login Failed");

            GotoStaffManagement();
            Assert.AreEqual(driver.Title, "Staff Management");

        }

        [Test]
        public void AddStaffManagementTest_Success_Test()
        {
            Login(_username, _password);
            Assert.AreEqual(driver.Title, "Home", "Login Failed");

            GotoStaffManagement();
            Assert.AreEqual(driver.Title, "Staff Management");

            Staff_Management_AddRecord();
            driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_btnSave_btn_btnSave_input")).Click();

            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(ELEMENT_TIMEOUT));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_btnReset_btn_btnReset_input")));
            Assert.AreEqual(true, ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_btnReset_btn_btnReset_input")));
        }
        [Test]
        public void AddStaffManagementTest_And_Location_Success_Test()
        {
            Login(_username, _password);
            Assert.AreEqual(driver.Title, "Home", "Login Failed");

            GotoStaffManagement();
            Assert.AreEqual(driver.Title, "Staff Management");

            Staff_Management_AddRecord();
            driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_btnSave_btn_btnSave_input")).Click();
                        
            Location_clickLocation();
            Location_clickAddButton();
            Location_fillForm();

            //Click Save
            driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl04_PerformInsertButton")).Click();                
        }

        #region Private
        private void Login(string username, string password)
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

        }
        private void Staff_Management_AddRecord()
        {
            Staff_Management_clickAddButton();
            Staff_Management_FillForm();
        }
        private void Staff_Management_clickAddButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(ELEMENT_TIMEOUT));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserList_ctl00_ctl02_ctl00_ctl00_InitInsertButton")));
            driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserList_ctl00_ctl02_ctl00_ctl00_InitInsertButton")).Click();
        }
        private void Staff_Management_FillForm()
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(ELEMENT_TIMEOUT));

            #region ElementExists
            //Textboxes
            wait.Until(ExpectedConditions.ElementExists(By.Id("cphContent_cphContent_frmEdit_Username")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("cphContent_cphContent_frmEdit_tbStaffPassword")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("cphContent_cphContent_frmEdit_Surname")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("cphContent_cphContent_frmEdit_Payroll")));

            //Date Time
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_StartDate_dateInput")));
            //Checkboxes
            wait.Until(ExpectedConditions.ElementExists(By.Id("cphContent_cphContent_frmEdit_Rosterable")));

            //DateTime
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_StartDate_dateInput")));
            //Checkbox
            wait.Until(ExpectedConditions.ElementExists(By.Id("cphContent_cphContent_frmEdit_Rosterable")));
            //DD
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_TitleDescription_cb_TitleDescription_Input")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_GenderDescription_cb_GenderDescription_Input")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_TimezoneDescription_cb_TimezoneDescription_Input")));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//table[@id='cphContent_cphContent_frmEdit_tblEdit']/tbody/tr[9]/td[4]")));
            #endregion

            var randomUserName = RandomString(10);
            var randomPassword = new string(randomUserName.Reverse().ToArray());
            var firstName = randomUserName.Substring(0, 5);
            var surName = randomUserName.Substring(randomUserName.Length - 5);
            var payroll = randomUserName.Substring(2, 5);

            driver.FindElement(By.Id("cphContent_cphContent_frmEdit_Username")).SendKeys(randomUserName + "@time2Work.com");
            driver.FindElement(By.Id("cphContent_cphContent_frmEdit_tbStaffPassword")).SendKeys(randomPassword);
            driver.FindElement(By.Id("cphContent_cphContent_frmEdit_Forename")).SendKeys(firstName);
            driver.FindElement(By.Id("cphContent_cphContent_frmEdit_Surname")).SendKeys(surName);
            driver.FindElement(By.Id("cphContent_cphContent_frmEdit_Payroll")).SendKeys(payroll);

            //Fill Date and Time            
            var startDate = driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_StartDate_dateInput"));
            startDate.Clear();
            startDate.SendKeys(DateTime.Now.AddDays(7).Date.ToString("dd/MM/yyyy"));

            //Fill checkboxes
            var rosterableChk = driver.FindElement(By.Id("cphContent_cphContent_frmEdit_Rosterable"));
            if (rosterableChk.Selected)
            {
                rosterableChk.Click();
            }

            //Fill DDs
            var title = driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_TitleDescription_cb_TitleDescription_Input"));
            title.Clear();
            title.SendKeys("Ms.");

            var gender = driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_GenderDescription_cb_GenderDescription_Input"));
            gender.Clear();
            gender.SendKeys("Female");

            var timezone = driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_frmEdit_TimezoneDescription_cb_TimezoneDescription_Input"));
            timezone.SendKeys("AEST (AUS Eastern Standard Time)");
            Thread.Sleep(SLEEP_TIME);

            var timezoneclick = driver.FindElement(By.XPath("//table[@id='cphContent_cphContent_frmEdit_tblEdit']/tbody/tr[9]/td[4]"));
            timezoneclick.Click();
            Thread.Sleep(SLEEP_TIME);
        }
        private void Location_fillForm()
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(ELEMENT_TIMEOUT));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl04_RDIPEffectiveFrom_dateInput")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl04_ddlLocation_cb_ddlLocation_Input")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl04_PerformInsertButton")));

            //Fill Dates 
            var location_from = driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl04_RDIPEffectiveFrom_dateInput"));
            location_from.Clear();
            location_from.SendKeys(DateTime.Now.AddDays(7).Date.ToString("dd/MM/yyyy"));

            //Fill Dropdown
            var location_loc = driver.FindElement(By.Id("ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl04_ddlLocation_cb_ddlLocation_Input"));
            location_loc.Clear();
            location_loc.SendKeys("Test Location");
            Thread.Sleep(SLEEP_TIME);

        }
        private void Location_clickLocation()
        {
            Thread.Sleep(SLEEP_TIME);
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@id='ctl00_ctl00_cphContent_cphContent_tabStrip']/div/ul/li[5]/a")));
            driver.FindElement(By.XPath("//div[@id='ctl00_ctl00_cphContent_cphContent_tabStrip']/div/ul/li[5]/a")).Click();
        }
        private void Location_clickAddButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[@id='ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl00_hdrAddHist_InitInsertButton']/span[2]")));
            driver.FindElement(By.XPath("//span[@id='ctl00_ctl00_cphContent_cphContent_tgUserLocation_ctl00_ctl02_ctl00_hdrAddHist_InitInsertButton']/span[2]")).Click();
            Thread.Sleep(SLEEP_TIME);
        }
        private void GotoStaffManagement()
        {
            driver.Navigate().GoToUrl(staffURL);
        }
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }
       
        [SetUp]
        public void SetupTest()
        {
            appURL = "http://AutoTest.Time2Work.com";
            staffURL = appURL + "/Staff/User.aspx";

            _username = "Momina@Time2Work.com";
            _password = "@Momina";

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
        


    }
}

