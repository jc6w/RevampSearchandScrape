using System;
namespace RevampSearchandScrape
{
    public class AmazonTest : ITest
    {
        public string website { get; set; }

        public AmazonTest()
        {
            website = "https://www.amazon.com";
        }
    }
}
