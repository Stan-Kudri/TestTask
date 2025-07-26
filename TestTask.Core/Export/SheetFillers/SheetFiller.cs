using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.SmartEnum;
using NPOI.SS.UserModel;
using TestTask.Core.Models;

namespace TestTask.Core.Export.SheetFillers
{
    public class SheetFiller<TEntity, TField> : ISheetFiller
        where TField : SheetField<TEntity, TField>
        where TEntity : Entity
    {
        private readonly BaseService<TEntity> _service;
        private readonly List<TField> _fields;

        public string Name => typeof(TEntity).Name;

        public SheetFiller(BaseService<TEntity> service)
        {
            _service = service;
            _fields = SmartEnum<TField>.List.OrderBy(field => field.Value).ToList();
        }

        public async Task Fill(ISheet sheet)
        {
            var rowIndex = 0;
            var header = sheet.CreateRow(rowIndex);
            for (var i = 0; i < _fields.Count; i++)
            {
                header.CreateCell(i).SetCellValue(_fields[i].Name);
            }

            var items = await _service.GetAll();
            foreach (var item in items)
            {
                rowIndex++;
                var row = sheet.CreateRow(rowIndex);
                for (var i = 0; i < _fields.Count; i++)
                {
                    _fields[i].FillCell(row.CreateCell(i), item);
                }
            }
        }
    }
}
