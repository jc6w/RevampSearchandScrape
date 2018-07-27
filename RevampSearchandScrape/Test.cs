using OfficeOpenXml;
using OpenQA.Selenium;

namespace RevampSearchandScrape
{
    public class Test : ITest
    {
        public string Website { get; set; }
        public IProdSuggest ProdSuggest { get; set; }
        public ISearchResult SearchResult { get; set; }
        public IProdDescription ProdDesc { get; set; }
        public IProdInfo ProdInfo { get; set; }
        public IProdReviews ProdReviews { get; set; }
        public IDriver Driver { get; set; }
        IWebDriver WebDriver;

        public Test(IDriver driver, IExcel e, IProdSuggest suggest, ISearchResult result, IProdDescription description, IProdInfo info, IProdReviews reviews)
        {
            Website = "https://www.amazon.com";
            Driver = driver;
            WebDriver = Driver.WebDriver;
            suggest = new ProdSuggest(e, driver);
            ProdSuggest = suggest;
            result = new SearchResult(e, driver);
            SearchResult = result;
            description = new ProdDescription(e, driver);
            ProdDesc = description;
            info = new ProdInfo(e, driver);
            ProdInfo = info;
            reviews = new ProdReviews(e, driver);
            ProdReviews = reviews;
        }

        public IWebDriver GetDriver()
        {
            return WebDriver;
        }

        public void SearchFor(string searchTerm)
        {
            IWebElement searchBox = WebDriver.FindElement(By.CssSelector("input[type='text']"));
            searchBox.SendKeys(searchTerm);
            ProdSuggest.FindElement();
            IWebElement Click = WebDriver.FindElement(By.CssSelector("[type='submit']"));
            Click.Click();

            SearchResult.FindElement();
            ProdDesc.FindElement();
            ProdInfo.FindElement();
            ProdReviews.FindElement();
        }
    }
}