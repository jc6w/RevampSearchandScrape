using System;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class SiteTest
    {
        public ITest Test { get; set; }
        public IWebDriver Browse { get; set; }
        public Excel excel { get; set; }

        public SiteTest(ITest t, IWebDriver b, Excel e)
        {
            Test = t;
            Browse = b;
            Browse.Url = Test.Website;
            excel = e;
        }

        public void Initialize()
        {
            Browse.Manage().Window.Maximize();
            Browse.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }


        public void Start(string searchTerm)
        {
            Test.SearchFor(searchTerm);

        }

        public void Stop()
        {
            excel.SaveExcel();
            Browse.Quit();
        }
    }
}