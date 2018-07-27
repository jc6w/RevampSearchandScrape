using System;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class SiteTest : ISiteTest
    {
        public ITest Test { get; set; }
        public IDriver Driver { get; set; }
        public IExcel Excel { get; set; }
        IWebDriver Browse;

        public SiteTest(ITest t, IDriver b, IExcel e)
        {
            Test = t;
            Driver = b;
            Browse = Driver.WebDriver;
            Browse.Url = Test.Website;
            Excel = e;
        }

        public void Initialize()
        {
            Browse.Manage().Window.Maximize();
        }


        public void Start(string searchTerm)
        {
            Test.SearchFor(searchTerm);

        }

        public void Stop()
        {
            Excel.SaveExcel();
            Browse.Quit();
        }
    }
}