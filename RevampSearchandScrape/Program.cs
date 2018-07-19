using OfficeOpenXml;
using OpenQA.Selenium.Chrome;
using System.IO;
using Microsoft.Practices.Unity;
using Unity;

namespace RevampSearchandScrape
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ExcelPackage package = new ExcelPackage();
            ITest newTest = new AmazonTest(new ChromeDriver(), package);
            Excel excel = new Excel(package, new FileInfo(newTest.GetFileName()));
            SiteTest test = new SiteTest(newTest, newTest.GetDriver(), excel);
            test.Initialize();
            test.Start("USB C Cable");
            test.Stop();
            //IUnityContainer unityContainer = ContainerUnity.Initialize();
            //ExcelPackage package = new ExcelPackage();
            //ITest newTest = unityContainer.Resolve<ITest>();
            //SiteTest test = unityContainer.Resolve<SiteTest>();
            test.Initialize();
            test.Start("USB C Cable");
            test.Stop();

        }
    }
}