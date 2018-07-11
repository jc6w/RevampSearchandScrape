using OpenQA.Selenium.Chrome;

namespace RevampSearchandScrape
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            SiteTest test = new SiteTest(new AmazonTest(), new ChromeDriver());
            test.Start();
            test.Stop();
        }
    }
}