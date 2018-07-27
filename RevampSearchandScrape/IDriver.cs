using System;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public interface IDriver
    {
        IWebDriver WebDriver { get; set; }
    }
}
