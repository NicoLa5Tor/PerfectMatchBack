using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFrontPM
{
    public class Login
    {
        [Fact]
        public void TLogin()
        {
            WebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Helper.Url);
            IWebElement inputEmail = driver.FindElement(By.Id("inputEmail"));
            inputEmail.SendKeys("davidroller066@gmail.com");
            IWebElement inputPassword = driver.FindElement(By.Id("inputPassword"));
            inputPassword.SendKeys("asdfghjkl");
            IWebElement Signin = driver.FindElement(By.Id("Signin"));
            Signin.Click();
            driver.Quit();
        }

        [Fact]
        public void Register()
        {
            WebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Helper.Url);
            Helper.GetWebElement(driver, "menuRegister", 0, null);
            Helper.GetWebElement(driver, "nameUser", 0, "nameUser");
            Helper.GetWebElement(driver, "inputEmail", 0, "inputEmail@email.com");
            Helper.GetWebElement(driver, "inputPass", 0, "inputPass");
            Helper.GetWebElement(driver, "selectCity", 0, null);
            Helper.GetWebElement(driver, "optionSelect", 0, null);
            Helper.GetWebElement(driver, "iBirthDate", 0, "12/12/2000");
            Helper.GetWebElement(driver, "iCodePay", 0, "000");

            Assert.True(driver.FindElement(By.Id("btnSubmitRegis")).Enabled);
            driver.Quit();
        }
        [Fact]
        public void GenerateTokenToRecoverPass()
        {
            WebDriver driver = new ChromeDriver(); 
            driver.Navigate().GoToUrl(Helper.Url); 
            Helper.GetWebElement(driver, "forgotform", 0, null);
            Helper.GetWebElement(driver, "email", 0, "email@email.com");
            Helper.GetWebElement(driver, "btnSubmitFRecover", 0, null); 
            Helper.GetWebElement(driver, "snackbar", 1);
            driver.Quit();
        }
        [Fact]
        public void NewPassword()
        {
            WebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/newpassword");
            Thread.Sleep(500);
            var snackbar=Helper.GetWebElement(driver, "snackbar", 1);
            Assert.Equal(snackbar.Text, "Error: NO se ha enviado el token\r\nDone");
            driver.Navigate().GoToUrl("http://localhost:4200/newpassword?token=lala");
            Thread.Sleep(500);
            var snackbar1 = Helper.GetWebElement(driver, "snackbar", 1);
            Assert.Equal(snackbar1.Text, "Token expired\r\nDone");
            Thread.Sleep(3000);
            Assert.Equal(driver.Url, Helper.Url);
            driver.Quit();
        }
    }
}
