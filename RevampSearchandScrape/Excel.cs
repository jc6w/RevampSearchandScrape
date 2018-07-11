using System;
using System.IO;
using OfficeOpenXml;
namespace RevampSearchandScrape
{
    public class Excel
    {
        ExcelPackage pack;
        FileInfo fileName;

        public Excel(ExcelPackage package, FileInfo f)
        {
            pack = package;
            fileName = f;
        }

        public void SaveExcel()
        {
            pack.SaveAs(fileName);
        }

    }
}


//Unity 
