using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class AmProdReviews : IProdReviews
    {
        public AmProdReviews(By b, ExcelPackage e, IWebDriver d)
        {
            List = new List<string>();
            List2 = new List<List<string>>();
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

                anchors = Element.FindElements(By.CssSelector("div[data-hook='review']"));

                int x = 0;
                foreach (IWebElement e in anchors)
                {
                    if (x == 5)
                    {
                        break;
                    }
                    List = new List<string>();
                    WriteToList(e, By.ClassName("a-profile-name"));
                    WriteToList(e, By.CssSelector("a[data-hook='review-title']"));
                    WriteToList(e.FindElement(By.CssSelector("i[data-hook='review-star-rating']")), By.CssSelector("span[class='a-icon-alt']"));
                    WriteToList(e, By.CssSelector("span[data-hook='review-date']"));
                    WriteToList(e, By.CssSelector("div[data-hook='review-collapsed']"));
                    WriteListToList();
                    x++;
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
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Product Reviews ");

            ws.Column(1).Style.Font.Bold = true;
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            ws.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            int count = 1;
            for (int x = 0; x < List2.Count; x++)
            {
                ws.Cells[x + count, 1].Value = "User Name";
                ws.Cells[x + count + 1, 1].Value = "Review Title";
                ws.Cells[x + count + 2, 1].Value = "Star Rating";
                ws.Cells[x + count + 3, 1].Value = "Date of Review";
                ws.Cells[x + count + 4, 1].Value = "Review";

                for (int y = 0; y < List2[x].Count; y++)
                {

                    ws.Cells[y + x + count, 2].Value = List2[x][y];
                }
                count += 5;
            }
            ws.Cells["A"].AutoFitColumns();
            ws.Column(2).Width = 100;
            ws.Cells[ws.Dimension.Address].Style.WrapText = true;
        }
    }
}
