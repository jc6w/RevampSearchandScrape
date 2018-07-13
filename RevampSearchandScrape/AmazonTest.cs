using OfficeOpenXml;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class AmazonTest : ITest, ISearchBox
    {
        public string Website { get; set; }
        public string FileName { get; set; }
        public IProdSuggest ProdSuggest { get; set; }
        public ISearchResult SearchResult { get; set; }
        public IProdDescription ProdDesc { get; set; }
        public IProdInfo ProdInfo { get; set; }
        public IProdReviews ProdReviews { get; set; }
        public IWebDriver Driver { get; set; }

        public AmazonTest(IWebDriver driver, ExcelPackage e)
        {
            Website = "https://www.amazon.com";
            FileName = "/Users/jmcw/Downloads/AmazonList.xlsx";
            Driver = driver;
            ProdSuggest = new AmSuggest(By.Id("suggestions-template"), e, driver);
            SearchResult = new AmSearchResult(By.Id("atfResults"), e, driver);
            ProdDesc = new AmProdDescription(By.Id("feature-bullets"), e, driver);
            ProdInfo = new AmProdInfo(By.Id("prodDetails"), e, driver);
            ProdReviews = new AmProdReviews(By.Id("cr-medley-top-reviews-wrapper"), e, driver);
        }

        public IWebDriver GetDriver()
        {
            return Driver;
        }

        public string GetFileName()
        {
            return FileName;
        }

        public void SearchBox(string searchTerm)
        {
            IWebElement searchBox = Driver.FindElement(By.CssSelector("input[type='text']"));
                searchBox.SendKeys(searchTerm);
            ProdSuggest.FindElement();
            Driver.FindElement(By.CssSelector("[type='submit']")).Click();
        }

        public void FindProducts()
        {
            SearchResult.FindElement();
            ProdDesc.FindElement();
            ProdInfo.FindElement();
            ProdReviews.FindElement();
        }
    }
}