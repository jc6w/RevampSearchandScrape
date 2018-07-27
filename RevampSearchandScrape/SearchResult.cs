using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class SearchResult : ElementPresent, ISearchResult
    {
        public IExcel Excel { get; set; }
        IWebDriver driver;
        ReadOnlyCollection<IWebElement> anchors;
        IWebElement element;
        List<string> list;
        List<List<string>> list2;
        By ByObj;

        public SearchResult(IExcel e, IDriver d)
        {
            list = new List<string>();
            list2 = new List<List<string>>();
            Excel = e;
            ByObj = By.Id("atfResults");
            driver = d.WebDriver;
        }

        public void GoToElement()
        {
            if (IsElementPresent(By.Id("result_4")))
            {
                element = driver.FindElement(By.Id("result_4"));
                element.FindElement(By.TagName("a")).Click();
            }
        }

        public void FindElement()
        {
            if (IsElementPresent(ByObj))
            {
                element = driver.FindElement(ByObj);

                anchors = element.FindElements(By.TagName("li"));

                foreach (IWebElement e in anchors)
                {
                    if (ListFilter(e.Text) == false)
                    {
                        if (e.GetAttribute("class") == "s-result-item celwidget  ")
                        {
                            list = new List<string>();
                            WriteToList(e, By.TagName("h2"));
                            WriteToList(e, By.CssSelector("span[class='a-size-small a-color-secondary']:nth-of-type(2)"));
                            WriteToList(e, By.TagName("h3"));
                            WriteToList(e, By.CssSelector("span[class='a-offscreen']"));
                            WriteListToList();
                        }
                    }
                }
            }
            WriteToExcel();
            GoToElement();
        }

        public bool IsElementPresent(By by) => base.IsElementPresent(driver, by);

        public override bool IsElementPresent(IWebElement el, By by) => base.IsElementPresent(el, by);

        public bool ListFilter(string s)
        {
            if (new[] { "Sponsored", "Our Brand", "Shop by Category" }.Any(x => s.Contains(x)))
            {
                return true;
            }
            return false;
        }

        public void WriteToList(IWebElement e, By b)
        {
            IWebElement temp;
            string text = "";
            if (IsElementPresent(e, b))
            {
                temp = e.FindElement(b);
                if (string.IsNullOrEmpty(temp.Text))
                {
                    text = temp.GetAttribute("innerHTML");
                }
                else
                {
                    text = temp.Text;
                }
                text.TrimStart('\0');
                text.TrimStart(' ');
                text.TrimEnd('\0');
                text.TrimEnd(' ');
                list.Add(text);
            }
            else
            {
                list.Add(null);
            }
        }

        public void WriteListToList()
        {
            list2.Add(list);
        }

        public ExcelWorksheet WorkSheetExists()
        {
            var excelList = Excel.Pack.Workbook.Worksheets.ToArray();
            foreach (ExcelWorksheet e in excelList)
            {
                if (e.Name == "Amazon Search Results")
                {
                    return e;
                }
            }

            return null;
        }

        public void WriteToExcel()
        {
            ExcelWorksheet ws = WorkSheetExists();

            if (ws == null)
            {
                ws = Excel.Pack.Workbook.Worksheets.Add("Amazon Search Results");
            }
            ws.Cells["A1:D1"].Style.Font.Bold = true;
            ws.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:D1"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            ws.Cells["A1"].Value = "Product Name";
            ws.Cells["B1"].Value = "Product Seller";
            ws.Cells["C1"].Value = "Product Type";
            ws.Cells["D1"].Value = "Product Price";
            for (int x = 0; x < list2.Count; x++)
            {
                for (int y = 0; y < list2[x].Count; y++)
                {
                    ws.Cells[x + 2, y + 1].Value = list2[x][y];
                }
            }
            ws.Cells["B"].AutoFitColumns();
            ws.Column(1).Width = 100;
            ws.Cells[ws.Dimension.Address].Style.WrapText = true;
        }
    }
}
