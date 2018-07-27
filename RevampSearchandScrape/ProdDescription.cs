using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class ProdDescription : ElementPresent, IProdDescription
    {
        public IExcel Excel { get; set; }
        IWebDriver driver;
        //ExcelPackage package;
        ReadOnlyCollection<IWebElement> anchors;
        IWebElement element;
        List<string> list;
        By ByObj;

        public ProdDescription(IExcel e, IDriver d)
        {
            list = new List<string>();
            Excel = e;
            ByObj = By.Id("feature-bullets");
            driver = d.WebDriver;
        }

        public void FindElement()
        {
            if (IsElementPresent(ByObj))
            {
                element = driver.FindElement(ByObj);

                anchors = element.FindElements(By.TagName("li"));

                foreach (IWebElement e in anchors)
                {
                    if (!(e.GetAttribute("class") == "aok-hidden"))
                        list.Add(e.Text);
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
                if (e.Name == "Product Description")
                {
                    return e;
                }
            }

            return Excel.Pack.Workbook.Worksheets.Add("Product Description");
        }

        public void WriteToExcel()
        {
            ExcelWorksheet ws = WorkSheetExists();
            ws.Cells["A1"].Style.Font.Bold = true;
            ws.Cells["A1"].Value = "Product Description:";
            for (int x = 0; x < list.Count; x++)
            {
                ws.Cells[x + 2, 1].Value = list[x];
            }
            ws.Column(1).Style.WrapText = true;
            ws.Column(1).Width = 100;
        }
    }
}
