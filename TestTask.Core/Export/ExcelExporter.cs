using System.Collections.Generic;
using System.IO;
using NPOI.XSSF.UserModel;
using TestTask.Core.Export.SheetFillers;

namespace TestTask.Core.Export
{
    public class ExcelExporter(IReadOnlyCollection<ISheetFiller> fillers)
    {
        public void Export(Stream destination)
        {
            using var workbook = new XSSFWorkbook();

            foreach (var filler in fillers)
            {
                var sheet = workbook.CreateSheet(filler.Name);
                filler.Fill(sheet);
            }

            workbook.Write(destination);
        }

        public void ExportToFile(string path)
        {
            using var fileStream = File.OpenWrite(path);
            Export(fileStream);
        }
    }
}
