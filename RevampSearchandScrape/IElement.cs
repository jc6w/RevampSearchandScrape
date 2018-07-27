using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public interface IElement : IFindElement, IWriteToExcel
    {
        IExcel Excel { get; set; }
    }
}