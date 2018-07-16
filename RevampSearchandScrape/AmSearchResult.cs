using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class AmSearchResult: ISearchResult
    {

        public ReadOnlyCollection<IWebElement> anchors { get; set; }
        public IWebElement Element { get; set; }
        public List<string> List { get; set; }
        public List<List<string>> List2 { get; set; }
        public IWebDriver Driver { get; set; }
        public ExcelPackage Package { get; set; }
        public By by { get; set; }

        public AmSearchResult(By b, ExcelPackage e, IWebDriver d)
        {
            List = new List<string>();
            List2 = new List<List<string>>();
            Package = e;
            by = b;
            Driver = d;
        }

        public void GoToElement()
        {
            if (IsElementPresent(By.Id("result_4")))
            {
                Element = Driver.FindElement(By.Id("result_4"));
                Element.FindElement(By.TagName("a")).Click();
            }
        }

        public void FindElement()
        {
            if (IsElementPresent(by))
            {
                Element = Driver.FindElement(by);

                anchors = Element.FindElements(By.TagName("li"));

                foreach (IWebElement e in anchors)
                {
                    if (ListFilter(e.Text) == false)
                    {
                        if (e.GetAttribute("class") == "s-result-item celwidget  ")
                        {
                            List = new List<string>();
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

        public bool IsElementPresent(By by)
        {
            try
            {
                Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public bool IsElementPresent(IWebElement el, By by)
        {
            try
            {
                el.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

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
                List.Add(text);
            }
            else
            {
                List.Add(null);
            }
        }

        public void WriteListToList()
        {
            List2.Add(List);
        }

        public void WriteToExcel()
        {
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Amazon Search Results ");

            ws.Cells["A1:D1"].Style.Font.Bold = true;
            ws.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:D1"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            ws.Cells["A1"].Value = "Product Name";
            ws.Cells["B1"].Value = "Product Seller";
            ws.Cells["C1"].Value = "Product Type";
            ws.Cells["D1"].Value = "Product Price";
            for (int x = 0; x < List2.Count; x++)
            {
                for (int y = 0; y < List2[x].Count; y++)
                {
                    ws.Cells[x + 2, y + 1].Value = List2[x][y];
                }
            }
            ws.Cells["B"].AutoFitColumns();
            ws.Column(1).Width = 100;
            ws.Cells[ws.Dimension.Address].Style.WrapText = true;
        }
    }
}
