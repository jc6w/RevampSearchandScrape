using System;
using System.IO;
using OpenQA.Selenium;
using OfficeOpenXml;

namespace RevampSearchandScrape
{
    public class Browse
    {
        static IWebDriver driver;

        public Browse(IWebDriver browser, string website)
        {
            driver = browser;
            driver.Url = website;
        }
    }
}