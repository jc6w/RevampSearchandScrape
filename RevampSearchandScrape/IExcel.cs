using System.IO;
using OfficeOpenXml;

namespace RevampSearchandScrape
{
    public interface IExcel
    {
        ExcelPackage Pack { get; set; }
        void SaveExcel();
    }
}