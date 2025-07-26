using System.Collections.Generic;
using Ardalis.SmartEnum;
using NPOI.SS.UserModel;
using TestTask.Core.Exeption;
using TestTask.Core.Import.Importers;

namespace TestTask.Core.Extension
{
    public static class SheetExtension
    {
        public static List<(TField Field, int Index)> ReadHeader<TModel, TField>(this ISheet sheet)
        where TField : SmartEnum<TField>, IFieldHandler<TModel>
        {
            if (sheet.LastRowNum <= 0)
            {
                throw new BusinessLogicException("File is empty");
            }

            var headerRow = sheet.GetRow(0);
            var result = new List<(TField, int)>();

            foreach (var field in SmartEnum<TField>.List)
            {
                bool found = false;
                for (var i = 0; i < headerRow.Cells.Count; i++)
                {
                    var str = headerRow.GetString(i, string.Empty);
                    if (str.Success && str.Value == field.ColumnName)
                    {
                        result.Add((field, i));
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    throw new BusinessLogicException($"Missing column: {field.ColumnName}");
                }
            }

            return result;
        }
    }
}
