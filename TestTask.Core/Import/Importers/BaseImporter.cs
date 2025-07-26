using System.Collections.Generic;
using Ardalis.SmartEnum;
using NPOI.SS.UserModel;
using TestTask.Core.Exeption;
using TestTask.Core.Extension;

namespace TestTask.Core.Import.Importers
{
    public abstract class BaseImporter<TModel, TField> : IImporter<TModel>
        where TModel : new()
        where TField : SmartEnum<TField>, IFieldHandler<TModel>
    {
        private List<(TField Field, int Index)> _fieldMap;

        public bool ReadHeader(ISheet sheet)
        {
            try
            {
                _fieldMap = sheet.ReadHeader<TModel, TField>();
                return true;
            }
            catch
            {
                _fieldMap = null;
                return false;
            }
        }

        public Result<TModel> ReadRow(IRow row)
        {
            if (_fieldMap == null)
                throw new BusinessLogicException("Header not read");

            if (row.Cells.Count < _fieldMap.Count)
                return Result<TModel>.CreateFail("Fewer cells than needed", row.RowNum);

            var model = new TModel();
            Result<TModel> lastResult = null;

            foreach (var (field, index) in _fieldMap)
            {
                lastResult = field.HandlerCell(model, row, index);
                if (!lastResult.Success)
                    return lastResult;
            }

            return lastResult;
        }

        public abstract bool IsModelSheet(string sheetName);
    }
}
