using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFrontPM
{
    public class Principal
    {
        [Fact]
        public void Buttons()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link1 = Helper.GetWebElement(driver, "link1", 0);
            link1.Click();
            IWebElement link2 = Helper.GetWebElement(driver, "link2", 0);
            link2.Click();
            IWebElement BFilters = Helper.GetWebElement(driver, "button_filters", 0);
            BFilters.Click();
            IWebElement link3 = Helper.GetWebElement(driver, "link3", 0);
            link3.Click();
            IWebElement BNotifications = Helper.GetWebElement(driver, "BShowNotifications", 0);
            BNotifications.Click(); 
            driver.Quit();
        }
        [Fact]
        public void Notification()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement BNotifications = Helper.GetWebElement(driver, "BShowNotifications", 0);
            BNotifications.Click();
            Helper.GetWebElement(driver, "notificationObj", 0);
            Helper.GetWebElement(driver, "deleteNoti", 1);
            driver.Quit();
        }
        [Fact]
        public void Filter()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link2 = Helper.GetWebElement(driver, "link2", 0);
            link2.Click();
            IWebElement BFilters = Helper.GetWebElement(driver, "button_filters", 0);
            BFilters.Click();
            IWebElement query=  Helper.GetWebElement(driver, "query", 0);
            IWebElement BSorts = Helper.GetWebElement(driver, "buttonSorts", 0);
            BSorts.Click();
            IWebElement sortInvers = Helper.GetWebElement(driver, "sortInvers", 0);

            sortInvers.Click();

            driver.Quit();
        }


        [Fact]
        public void Sorts()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link2 = Helper.GetWebElement(driver, "link2", 0);
            link2.Click();
            IWebElement BFilters = Helper.GetWebElement(driver, "button_filters", 0);
            BFilters.Click();
            IWebElement BSorts = Helper.GetWebElement(driver, "buttonSorts", 0);
            BSorts.Click();
            IWebElement sortByCity = Helper.GetWebElement(driver, "sortByCity", 0);
            sortByCity.Click();
            BSorts.Click();
            IWebElement sortByWeight = Helper.GetWebElement(driver, "sortByWeight", 0);
            sortByWeight.Click();
            BSorts.Click();
            IWebElement sortByAnimalName = Helper.GetWebElement(driver, "sortByAnimalName", 0);
            sortByAnimalName.Click();
            driver.Quit();
        }
        [Fact]
        public void Panels()
        {
            IWebDriver driver = Helper.StartPage();
            IWebElement link2 = Helper.GetWebElement(driver, "link2", 0);
            link2.Click();
            IWebElement BFilters = Helper.GetWebElement(driver, "button_filters", 0);
            BFilters.Click();

            //cityPanel selectCity optionCity
            IWebElement cityPanel = Helper.GetWebElement(driver, "cityPanel", 0);
            cityPanel.Click();
            Helper.GetWebElement(driver, "selectCity", 2);
            IWebElement optionCity = Helper.GetWebElement(driver, "optionCity", 0);
            optionCity.Click();

            //panelSeller selectSeller optionSeller
            IWebElement panelSeller = Helper.GetWebElement(driver, "panelSeller", 0);
            panelSeller.Click();
            Helper.GetWebElement(driver, "selectSeller", 2);
            IWebElement optionSeller = Helper.GetWebElement(driver, "optionSeller", 0);
            optionSeller.Click();

            //panelWeight inputWeightI  inputWeightF
            IWebElement panelWeight = Helper.GetWebElement(driver, "panelWeight", 0);
            panelWeight.Click();
            Helper.GetWebElement(driver, "inputWeightI", 0,"100");
            Helper.GetWebElement(driver, "inputWeightF", 0, "500");

            //panelGender selectGender optionGender
            IWebElement panelGender = Helper.GetWebElement(driver, "panelGender", 0);
            panelGender.Click();
            Helper.GetWebElement(driver, "selectGender", 2);
            IWebElement optionGender = Helper.GetWebElement(driver, "optionGender", 0);
            optionGender.Click();

            //PanelAnimalT selectAnimalT optionAnimalT
            IWebElement PanelAnimalT = Helper.GetWebElement(driver, "PanelAnimalT", 0);
            PanelAnimalT.Click();
            Helper.GetWebElement(driver, "selectAnimalT", 2);
            IWebElement optionAnimalT = Helper.GetWebElement(driver, "optionAnimalT", 0);
            optionAnimalT.Click();

            //panelBreed selectBreed optionBreed
            IWebElement panelBreed = Helper.GetWebElement(driver, "panelBreed", 0);
            panelBreed.Click();
            Helper.GetWebElement(driver, "selectBreed", 2);
            IWebElement optionBreed = Helper.GetWebElement(driver, "optionBreed", 0);
            optionBreed.Click();

            //panelAge inputAgeI inputAgeF
            IWebElement panelAge = Helper.GetWebElement(driver, "panelAge", 0);
            panelAge.Click();
            Helper.GetWebElement(driver, "inputAgeI", 0, "0");
            Helper.GetWebElement(driver, "inputAgeF", 0, "30");
            driver.Quit();
        }
    }
}
