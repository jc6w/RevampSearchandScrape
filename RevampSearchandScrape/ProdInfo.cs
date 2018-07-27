using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class ProdInfo: ElementPresent, IProdInfo
    {
        public IExcel Excel { get; set; }
        IWebDriver driver;
        ReadOnlyCollection<IWebElement> anchors;
        IWebElement element;
        List<string> list;
        List<List<string>> list2;
        By ByObj;

        public ProdInfo(IExcel e, IDriver d)
        {
            list = new List<string>();
            list2 = new List<List<string>>();
            Excel = e;
            ByObj = By.Id("prodDetails");
            driver = d.WebDriver;
        }

        public void FindElement()
        {
            if (IsElementPresent(ByObj))
            {
                element = driver.FindElement(ByObj);
                anchors = element.FindElements(By.TagName("tr"));

                foreach (IWebElement e in anchors)
                {
                    list = new List<string>();
                    WriteToList(e, By.TagName("th"));
                    WriteToList(e, By.TagName("td"));
                    WriteListToList();
                }
            }
            WriteToExcel();
        }

        public bool IsElementPresent(By by) => base.IsElementPresent(driver, by);

        public override bool IsElementPresent(IWebElement el, By by) => base.IsElementPresent(el, by);

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
                if (e.Name == "Product Information")
                {
                    return e;
                }
            }

            return Excel.Pack.Workbook.Worksheets.Add("Product Information");
        }

        public void WriteToExcel()
        {
            ExcelWorksheet ws = WorkSheetExists();
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            ws.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            for (int x = 0; x < list2.Count; x++)
            {
                ws.Cells[x + 1, 1].Style.Font.Bold = true;
                for (int y = 0; y < list2[x].Count; y++)
                {
                    ws.Cells[x + 1, y + 1].Value = list2[x][y];
                }
            }
            ws.Cells["A"].AutoFitColumns();
            ws.Column(2).Width = 100;
            ws.Column(2).Style.WrapText = true;
        }
    }
}
