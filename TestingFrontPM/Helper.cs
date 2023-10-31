using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestingFrontPM
{
    public static class Helper
    {
        public static string Url { get; set; } = "http://localhost:4200/login";
        public static IWebDriver StartPage()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Url);
            IWebElement inputEmail = driver.FindElement(By.Id("inputEmail"));
            inputEmail.SendKeys("davidroller066@gmail.com");
            IWebElement inputPassword = driver.FindElement(By.Id("inputPassword"));
            inputPassword.SendKeys("asdfghjkl");
            IWebElement Signin = driver.FindElement(By.Id("Signin"));
            Signin.Click();
            return driver;
        }
        public static IWebElement GetWebElement(IWebDriver driver, string Name, int type)
        {
            int i = 0;
            IWebElement link = null;
            while (true)
            {
                try
                {
                    if (type == 0)
                    {
                        link = driver.FindElement(By.Id(Name));
                    }
                    if (type == 1)
                    {
                        link = driver.FindElement(By.ClassName(Name));
                    }
                    if (type == 2)
                    {
                        link = driver.FindElement(By.Name(Name));

                        link.Click();

                    }

                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(2000);
                    if (i++ == 5)
                    {
                        Assert.Fail(ex.Message);
                        break;
                    }

                }
            }
            return link;
        }
        public static IWebElement GetWebElement(IWebDriver driver, string Name, int type,string input)
        {
            int i = 0;
            IWebElement link = null;
            while (true)
            {
                try
                {
                    switch (type)
                    {
                        case 0:
                                link = driver.FindElement(By.Id(Name));
                            if (input != null)
                            {
                                link.SendKeys(input);
                            }
                            else
                            {
                                link.Click();
                            }
                            break;
                        case 1:
                            link = driver.FindElement(By.ClassName(Name));
                            if (input == null)
                            {
                                link.Click();
                            }
                            break; 
                        case 2:
                            link = driver.FindElement(By.Name(Name));
                            link.Click();
                            break; 
                    }

                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(2000);
                    if (i++ == 5)
                    {
                        Assert.Fail(ex.Message);
                        break;
                    }

                }
            }
            return link;
        }
    }
}
