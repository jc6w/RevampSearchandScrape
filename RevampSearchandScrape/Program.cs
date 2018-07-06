using System;
namespace RevampSearchandScrape
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string browser = "";
            string website = "";
            string searchTerm = "";
            Console.WriteLine("Hello World!");
            Console.WriteLine("Please choose from the following browsers");

            Browse browse = new Browse(browser, website);

            browse.SearchBox(searchTerm);

            Console.WriteLine("blahblaeh");
        }
    }
}