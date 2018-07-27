namespace RevampSearchandScrape
{
    public interface ISiteTest
    {
        ITest Test { get; set; }
        IDriver Driver { get; set; }
        IExcel Excel { get; set; }

        void Initialize();
        void Start(string s);
        void Stop();
    }
}