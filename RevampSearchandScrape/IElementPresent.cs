using System;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public interface IElementPresent
    {
        bool IsElementPresent(By by);
        bool IsElementPresent(IWebElement el, By by);
    }
}
