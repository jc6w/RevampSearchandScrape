using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OpenQA.Selenium;
using Selenium.Extensions;

namespace RevampSearchandScrape
{
    public class ProdSuggest : ElementPresent, IProdSuggest
    {
        public IExcel Excel { get; set; }
        IWebDriver driver;
        //ExcelPackage package;
        ReadOnlyCollection<IWebElement> anchors;
        IWebElement element;
        List<string> list;
        By ByObj;

        public ProdSuggest(IExcel e, IDriver d)
        {
            list = new List<string>();
            Excel = e;
            ByObj = By.Id("suggestions-template");
            driver = d.WebDriver;
        }

        public void FindElement()
        {
            if (IsElementPresent(ByObj))
            {
                WaitUntil.ElementPresent(driver, ByObj);
                element = driver.FindElement(ByObj);
                WaitUntil.ElementPresent(driver, By.Id("suggestions"));
                IWebElement child = driver.FindElement(By.Id("suggestions"));

                anchors = child.FindElements(By.TagName("div"));

                foreach (IWebElement s in anchors)
                {
                    if (s.Text.Contains("in "))
                    {
                        list.Add("To Department " + s.Text);
                    }
                    else
                    {
                        list.Add(s.Text);
                    }
                }
            }
            WriteToExcel();
        }

        public bool IsElementPresent(By by) => base.IsElementPresent(driver, by);

        public override bool IsElementPresent(IWebElement el, By by) => base.IsElementPresent(el, by);

        public ExcelWorksheet WorkSheetExists()
        {
            var excelList = Excel.Pack.Workbook.Worksheets.ToArray();
            foreach (ExcelWorksheet e in excelList)
            {
                if (e.Name == "Amazon Suggestions")
                {
                    return e;
                }
            }

            return Excel.Pack.Workbook.Worksheets.Add("Amazon Suggestions");
        }

        public void WriteToExcel()
        {
            ExcelWorksheet ws = WorkSheetExists();

            for (int x = 0; x < list.Count; x++)
            {
                ws.Cells[x + 1, 1].Value = list[x];
            }
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
        }
    }
}
