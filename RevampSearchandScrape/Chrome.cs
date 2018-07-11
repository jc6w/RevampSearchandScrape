using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RevampSearchandScrape
{
    public class Chrome: IBrowse
    {
        public IWebDriver Driver { get; set; }
        public Chrome()
        {
            Driver = new ChromeDriver();
        }
    }
}
