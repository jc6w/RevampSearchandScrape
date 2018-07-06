using System;
using System.IO;
using OfficeOpenXml;
namespace RevampSearchandScrape
{
    public abstract class Excel
    {
        ExcelPackage pack;
        FileInfo fileName;

        public Excel()
        {
            pack = new ExcelPackage();
            fileName = new FileInfo("toBeFilled.xlsx");
        }
        //make abstract???
        public abstract void WriteToExcel();

        public void SaveExcel()
        {

        }

    }
}
