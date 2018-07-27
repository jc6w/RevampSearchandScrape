using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class ProdReviews : ElementPresent, IProdReviews
    {
        public IExcel Excel { get; set; }
        IWebDriver driver;
        ReadOnlyCollection<IWebElement> anchors;
        IWebElement element;
        List<string> list;
        List<List<string>> list2;
        By ByObj;

        public ProdReviews(IExcel e, IDriver d)
        {
            list = new List<string>();
            list2 = new List<List<string>>();
            Excel = e;
            ByObj = By.Id("cr-medley-top-reviews-wrapper");
            driver = d.WebDriver;
        }

        public void FindElement()
        {
            if (IsElementPresent(ByObj))
            {
                element = driver.FindElement(ByObj);
                anchors = element.FindElements(By.CssSelector("div[data-hook='review']"));

                int x = 0;
                foreach (IWebElement e in anchors)
                {
                    if (x == 5)
                    {
                        break;
                    }
                    list = new List<string>();
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
                if (e.Name == "Product Reviews")
                {
                    return e;
                }
            }

            return Excel.Pack.Workbook.Worksheets.Add("Product Reviews");
        }

        public void WriteToExcel()
        {
            ExcelWorksheet ws = WorkSheetExists();

            ws.Column(1).Style.Font.Bold = true;
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            ws.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            int count = 1;
            for (int x = 0; x < list2.Count; x++)
            {
                ws.Cells[x + count, 1].Value = "User Name";
                ws.Cells[x + count + 1, 1].Value = "Review Title";
                ws.Cells[x + count + 2, 1].Value = "Star Rating";
                ws.Cells[x + count + 3, 1].Value = "Date of Review";
                ws.Cells[x + count + 4, 1].Value = "Review";

                for (int y = 0; y < list2[x].Count; y++)
                {
                    ws.Cells[y + x + count, 2].Value = list2[x][y];
                }
                count += 5;
            }
            ws.Cells["A"].AutoFitColumns();
            ws.Column(2).Width = 100;
            ws.Column(2).Style.WrapText = true;
        }
    }
}
