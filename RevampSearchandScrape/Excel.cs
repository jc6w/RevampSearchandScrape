using System.IO;
using OfficeOpenXml;
namespace RevampSearchandScrape
{
    public class Excel: IExcel
    {
        public ExcelPackage Pack { get; set; }
        FileInfo fileName;

        public Excel()
        {
            fileName = new FileInfo("/Users/jmcw/Downloads/AmazonList.xlsx");
            Pack = new ExcelPackage(fileName);
        }

        public void SaveExcel()
        {
            Pack.SaveAs(Pack.File);
        }
    }
}


//Unity 
