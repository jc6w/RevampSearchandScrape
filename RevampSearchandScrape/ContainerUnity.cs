using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Unity;

namespace RevampSearchandScrape
{
    public class ContainerUnity
    {
        public static void RegisterElements(IUnityContainer unityContainer)
        {
            ExcelPackage package = new ExcelPackage();
            unityContainer.RegisterInstance(package);

            unityContainer.RegisterType<ITest, AmazonTest>("Amazon");
            unityContainer.RegisterType<IWebDriver, ChromeDriver>("Chrome");




            var testType = typeof(ITest);
            var browserType = typeof(IWebDriver);

        }
    }
}
