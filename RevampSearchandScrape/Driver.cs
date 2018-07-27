using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RevampSearchandScrape
{
    public class Driver : IDriver
    {
        public IWebDriver WebDriver { get; set; }

        public Driver()
        {
            WebDriver = new ChromeDriver();
        }
    }
}
