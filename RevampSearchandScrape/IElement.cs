using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public interface IElement : IFindElement, IElementPresent, IWriteToExcel
    {
        ReadOnlyCollection<IWebElement> anchors { get; set; }
        IWebElement Element { get; set; }
        List<string> List { get; set; }
        List<List<string>> List2 { get; set; }
        IWebDriver Driver { get; set; }
        ExcelPackage Package { get; set; }
        By by { get; set; }
    }
}