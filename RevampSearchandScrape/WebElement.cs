using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{//MAKE ABSTRACT
    public abstract class WebElement
    {
        List<string> list;
        List<List<string>> list2;

        static ReadOnlyCollection<IWebElement> anchors;
        IWebElement element;
        IWebDriver driver;

        WebElement(IWebDriver dr, IWebElement we)
        {
            driver = dr;
            element = we;
        }
    }
}
