using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class AmProdDescription: IProdDescription
    {
        public AmProdDescription(By b, ExcelPackage e, IWebDriver d)
        {
            List = new List<string>();
            Package = e;
            by = b;
            Driver = d;
        }

        public ReadOnlyCollection<IWebElement> anchors { get; set; }
        public IWebElement Element { get; set; }
        public List<string> List { get; set; }
        public List<List<string>> List2 { get; set; }
        public IWebDriver Driver { get; set; }
        public ExcelPackage Package { get; set; }
        public By by { get; set; }

        public void FindElement()
        {
            if (IsElementPresent(by))
            {
                Element = Driver.FindElement(by);

                anchors = Element.FindElements(By.TagName("li"));

                foreach (IWebElement e in anchors)
                {
                    if (!(e.GetAttribute("class") == "aok-hidden"))
                        List.Add(e.Text);
                }
            }
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

        public void WriteToExcel()
        {
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Product Description");
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            ws.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            ws.Cells["A1"].Style.Font.Bold = true;
            ws.Cells["A1"].Value = "Product Description:";
            for (int x = 0; x < List.Count; x++)
            {
                ws.Cells[x + 2, 1].Value = List[x];
            }
        }
    }
}
