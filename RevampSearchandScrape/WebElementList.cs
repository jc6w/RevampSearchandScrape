using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{//MAKE ABSTRACT
    public abstract class WebElementList
    {
        List<string> list;
        List<List<string>> list2;

        static ReadOnlyCollection<IWebElement> anchors;
        IWebElement element;
        IWebDriver driver;

        WebElementList(IWebDriver dr, IWebElement we)
        {
            driver = dr;
            element = we;
        }
    }
}
