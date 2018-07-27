using OpenQA.Selenium;
namespace RevampSearchandScrape
{
    public interface ITest: ISearchBox
    {
        string Website { get; set; }
        IDriver Driver { get; set; }
        IProdSuggest ProdSuggest { get; set; }
        ISearchResult SearchResult { get; set; }
        IProdDescription ProdDesc { get; set; }
        IProdInfo ProdInfo { get; set; }
        IProdReviews ProdReviews { get; set; }

        IWebDriver GetDriver();
    }
}
