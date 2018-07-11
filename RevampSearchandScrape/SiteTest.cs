using System;
namespace RevampSearchandScrape
{
    public abstract class SiteTest
    {
        string website;
        Browse browser;
        Excel excel;
        public SiteTest(Browse b, Excel e)
        {
            browser = b;
            excel = e;
        }
    }
}
