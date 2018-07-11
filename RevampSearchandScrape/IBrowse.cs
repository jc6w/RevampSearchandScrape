using System;
using System.IO;
using OpenQA.Selenium;
using OfficeOpenXml;

namespace RevampSearchandScrape
{
    public interface IBrowse
    {
        IWebDriver Driver { get; set; }
    }
}