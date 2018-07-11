using System;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class SiteTest
    {
        public ITest Test { get; set; }
        public IWebDriver Browse { get; set; }

        public SiteTest(ITest t, IWebDriver b)
        {
            Test = t;
            Browse = b;
            Browse.Url = Test.website;
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            Browse.Quit();
        }
    }
}
