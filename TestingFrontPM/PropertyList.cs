using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFrontPM
{
    public class PropertyList
    {
        [Fact]
        public void ExistPosts()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link2 = Helper.GetWebElement(driver, "link2", 0);
            link2.Click();
            try
            {
                Helper.GetWebElement(driver, "propertyCard", 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                return;
            }
            driver.Quit();
        }
        [Fact]
        public void ActionsPosts()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link2 = Helper.GetWebElement(driver, "link2", 0);
            link2.Click();
            IWebElement openDialog = Helper.GetWebElement(driver, "openDialog", 0);
            openDialog.Click(); // 

            IWebElement buttoninfo = Helper.GetWebElement(driver, "buttoninfo", 0);
            buttoninfo.Click();
            Helper.GetWebElement(driver, "mat-typography", 1, null);
            IWebElement buttonClosed = Helper.GetWebElement(driver, "buttonClosed", 0);

            buttonClosed.Click();
            Helper.GetWebElement(driver, "btnBuy", 0, null);
            driver.Quit();
        }
        [Fact]
        public void OwnerPosts()
        {
            IWebDriver driver = Helper.StartPage(); 
            IWebElement link2 = Helper.GetWebElement(driver, "link1", 0); 
            link2.Click();
            IWebElement btnMenuOwner = Helper.GetWebElement(driver, "btnMenuOwner", 0); 
            btnMenuOwner.Click();
            Helper.GetWebElement(driver, "btnProfileDelete", 0);
            Helper.GetWebElement(driver, "btnProfileEdit", 0,null);
            Helper.GetWebElement(driver, "body", 0, null);
            driver.Quit();

        }
    }
}
