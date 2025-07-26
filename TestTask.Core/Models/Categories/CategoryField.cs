using System;
using NPOI.SS.UserModel;
using TestTask.Core.Export.SheetFillers;
using TestTask.Core.Extension;
using TestTask.Core.Import.Importers;

namespace TestTask.Core.Models.Categories
{
    public class CategoryField : SheetField<Category, CategoryField>, IFieldHandler<Category>
    {
        public static CategoryField ID = new("ID", 0,
                                            (field, item) => field.SetCellValue(item.Id),
                                            (model, row, idx) =>
                                            {
                                                var res = row.GetInt(idx, "Id");
                                                if (!res.Success) return res.ToError<Category>();
                                                model.Id = res.Value;
                                                return Result<Category>.CreateSuccess(model, row.RowNum);
                                            });

        public static CategoryField NameCategory = new("Name", 1,
                                                       (field, item) => field.SetCellValue(item.Name),
                                                       (model, row, idx) =>
                                                       {
                                                           var res = row.GetString(idx, "Name");
                                                           if (!res.Success) return res.ToError<Category>();
                                                           if (string.IsNullOrEmpty(res.Value))
                                                               return Result<Category>.CreateFail("Name should not be empty", row.RowNum);
                                                           model.Name = res.Value;
                                                           return Result<Category>.CreateSuccess(model, row.RowNum);
                                                       });

        private readonly Action<ICell, Category> SetCellValue;

        public Func<Category, IRow, int, Result<Category>> HandlerCell { get; }

        public string ColumnName => Name;

        private CategoryField(string name, int value,
                             Action<ICell, Category> setCellValue,
                             Func<Category, IRow, int, Result<Category>> handler)
            : base(name, value)
        {
            SetCellValue = setCellValue;
            HandlerCell = handler;
        }

        public override void FillCell(ICell cell, Category item)
            => SetCellValue(cell, item);
    }
}
