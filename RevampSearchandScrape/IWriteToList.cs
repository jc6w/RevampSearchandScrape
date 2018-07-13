using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public interface IWriteToList
    {
        void WriteToList(IWebElement e, By b);
    }
}
