using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RevampSearchandScrape
{
    public class AmSuggest : IProdSuggest
    {

        public ReadOnlyCollection<IWebElement> anchors { get; set; }
        public IWebElement Element { get; set; }
        public List<string> List { get; set; }
        public List<List<string>> List2 { get; set; }
        public IWebDriver Driver { get; set; }
        public ExcelPackage Package { get; set; }
        public By by { get; set; }

        public AmSuggest(By b, ExcelPackage e, IWebDriver d)
        {
            List = new List<string>();
            Package = e;
            by = b;
            Driver = d;
        }

        public void FindElement()
        {
            if (IsElementPresent(by))
            {
                Element = Driver.FindElement(by);
                Console.WriteLine(Element.Text);

                IWebElement child = Element.FindElement(By.Id("suggestions"));

                anchors = child.FindElements(By.TagName("div"));

                foreach (IWebElement s in anchors)
                {
                    if (s.Text.Contains("in "))
                    {
                        List.Add("To Department " + s.Text);
                    }
                    else
                    {
                        List.Add(s.Text);
                    }
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

        public void WriteToExcel()
        {
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Amazon Suggestions " + List[0]);

            for (int x = 0; x < List.Count; x++)
            {
                ws.Cells[x + 1, 1].Value = List[x];
            }
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
        }
    }
}
