using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Opera;

namespace RevampSearchandScrape
{
    public class Browse
    {
        static IWebDriver driver;
        protected static IWebElement searchBox;
        Excel excel;

        public Browse(string browser, string website)
        {
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                //case "Edge": driver = new EdgeDriver();
                //break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                case "Opera":
                    driver = new OperaDriver();
                    break;
                case "Safari":
                    driver = new SafariDriver();
                    break;
                default:
                    Console.WriteLine("INVALID BROWSER");
                    break;
            }

            driver.Url = "https://" + website;
            Setup();
        }

        void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            excel = new Excel();
        }

        public void SearchBox(string searchTerm)
        {
            searchBox = driver.FindElement(By.CssSelector("input[type='text']"));

            searchBox.SendKeys(searchTerm);
        }
        public void Close()
        {
            excel.SaveExcel();
            driver.Quit();
        }
    }
}