
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestingFrontPM
{

    public class TestAddPost
    {


        [Fact]
        public void BlockImg()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link = Helper.GetWebElement(driver, "link3",0);
            link.Click();
            IWebElement Image = Helper.GetWebElement(driver, "image0",0);
            Image.SendKeys("C:\\Users\\drcec\\Downloads\\ippo1.jpg");
            IWebElement deleteSpace = Helper.GetWebElement(driver, "deleteSpace0",0);
            deleteSpace.Click();
            driver.Quit();
        }
        [Fact]
        public void Inputs()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link = Helper.GetWebElement(driver, "link3",0);
            link.Click();
            IWebElement inputAnimalName = Helper.GetWebElement(driver, "animalName",0);
            inputAnimalName.SendKeys("Don pepe"); 
            IWebElement inputWeight = Helper.GetWebElement(driver, "weight",0);
            inputWeight.SendKeys("70000");
            IWebElement inputAge = Helper.GetWebElement(driver, "age",0);
            inputAge.SendKeys("7");
            IWebElement inputPrice = Helper.GetWebElement(driver, "price",0);
            inputPrice.SendKeys("7000");
            driver.Quit();

        }
        [Fact]
        public void Selects()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link = Helper.GetWebElement(driver, "link3", 0);
            link.Click();
            IWebElement PanelCity = Helper.GetWebElement(driver, "idcity", 0);
            PanelCity.Click();
            Helper.GetWebElement(driver, "idcity", 2);
            IWebElement optionCity = Helper.GetWebElement(driver, "city", 1);
            optionCity.Click();

            IWebElement selectGender = Helper.GetWebElement(driver, "idgender", 0);
            selectGender.Click();
            Helper.GetWebElement(driver, "idgender", 2);
            IWebElement optionGender = Helper.GetWebElement(driver, "gender", 1);
            optionGender.Click();

            IWebElement selectAnimalType = Helper.GetWebElement(driver, "idAnimalType", 0);
            selectAnimalType.Click();
            Helper.GetWebElement(driver, "idAnimalType", 2);
            IWebElement optionAnimalType = Helper.GetWebElement(driver, "AnimalType", 1);
            optionAnimalType.Click();

            IWebElement selectBreed = Helper.GetWebElement(driver, "breed", 0);
            selectBreed.Click();
            Helper.GetWebElement(driver, "breed", 2);
            IWebElement optionBreed = Helper.GetWebElement(driver, "optBreed", 1);
            optionBreed.Click();    
            driver.Quit();
        }


        
    }
}