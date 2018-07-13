using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class AmProdInfo: IProdInfo
    {
        public ReadOnlyCollection<IWebElement> anchors { get; set; }
        public IWebElement Element { get; set; }
        public List<string> List { get; set; }
        public List<List<string>> List2 { get; set; }
        public IWebDriver Driver { get; set; }
        public ExcelPackage Package { get; set; }
        public By by { get; set; }

        public AmProdInfo(By b, ExcelPackage e, IWebDriver d)
        {
            List = new List<string>();
            List2 = new List<List<string>>();
            Package = e;
            by = b;
            Driver = d;
        }

        public void FindElement()
        {
            if (IsElementPresent(by))
            {
                Element = Driver.FindElement(by);
                anchors = Element.FindElements(By.TagName("tr"));

                foreach (IWebElement e in anchors)
                {
                    List = new List<string>();
                    WriteToList(e, By.TagName("th"));
                    WriteToList(e, By.TagName("td"));
                    WriteListToList();
                }
            }
            WriteToExcel();
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
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Product Information");
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            ws.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            for (int x = 0; x < List2.Count; x++)
            {
                ws.Cells[x + 1, 1].Style.Font.Bold = true;
                for (int y = 0; y < List2[x].Count; y++)
                {
                    ws.Cells[x + 1, y + 1].Value = List2[x][y];
                }
            }
            ws.Cells["B"].AutoFitColumns();
            ws.Column(1).Style.WrapText = true;
            ws.Column(1).Width = 100;
        }
    }
}
