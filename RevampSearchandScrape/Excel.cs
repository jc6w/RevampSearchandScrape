using System;
using System.IO;
using OfficeOpenXml;
namespace RevampSearchandScrape
{
    public class Excel
    {
        ExcelPackage pack;
        FileInfo fileName;

        public Excel()
        {
            pack = new ExcelPackage();
            fileName = new FileInfo("toBeFilled.xlsx");
        }

        public void WriteToExcel()
        {

        }

        public void SaveExcel()
        {
            
        }

    }
}
