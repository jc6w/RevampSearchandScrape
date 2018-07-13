using OfficeOpenXml;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace RevampSearchandScrape
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ExcelPackage package = new ExcelPackage();
            ITest newTest = new AmazonTest(new ChromeDriver(), package);
            SiteTest test = new SiteTest(newTest, newTest.GetDriver(), new Excel(package, new FileInfo(newTest.GetFileName())));
            test.Initialize();
            test.Start("USB C Cable");
            test.Stop();
        }
    }
}