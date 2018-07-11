using System;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public interface IAddToList
    {
        void AddToList(IWebElement e, By s);
        void AddListToList();
    }
}
