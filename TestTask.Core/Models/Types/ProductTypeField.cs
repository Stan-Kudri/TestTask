using System;
using NPOI.SS.UserModel;
using TestTask.Core.Export.SheetFillers;
using TestTask.Core.Extension;
using TestTask.Core.Import.Importers;

namespace TestTask.Core.Models.Types
{
    public class ProductTypeField : SheetField<ProductType, ProductTypeField>, IFieldHandler<ProductType>
    {
        public static ProductTypeField ID = new("ID", 0,
                                                 (field, item) => field.SetCellValue(item.Id),
                                                 (model, row, idx) =>
                                                 {
                                                     var res = row.GetInt(idx, "Id");
                                                     if (!res.Success) return res.ToError<ProductType>();
                                                     model.Id = res.Value;
                                                     return Result<ProductType>.CreateSuccess(model, row.RowNum);
                                                 });

        public static ProductTypeField NameType = new("Name", 1,
                                                       (field, item) => field.SetCellValue(item.Name),
                                                       (model, row, idx) =>
                                                       {
                                                           var res = row.GetString(idx, "Name");
                                                           if (!res.Success) return res.ToError<ProductType>();
                                                           if (string.IsNullOrEmpty(res.Value))
                                                               return Result<ProductType>.CreateFail("Name should not be empty", row.RowNum);
                                                           model.Name = res.Value;
                                                           return Result<ProductType>.CreateSuccess(model, row.RowNum);
                                                       });

        public static ProductTypeField CategoryId = new("CategoryId", 2,
                                                         (field, item) => field.SetCellValue(item.CategoryId),
                                                         (model, row, idx) =>
                                                         {
                                                             var res = row.GetInt(idx, "CategoryId");
                                                             if (!res.Success) return res.ToError<ProductType>();
                                                             model.CategoryId = res.Value;
                                                             return Result<ProductType>.CreateSuccess(model, row.RowNum);
                                                         });

        private readonly Action<ICell, ProductType> SetCellValue;

        public Func<ProductType, IRow, int, Result<ProductType>> HandlerCell { get; }

        public string ColumnName => Name;

        private ProductTypeField(string name, int value,
                                  Action<ICell, ProductType> setCellValue,
                                  Func<ProductType, IRow, int, Result<ProductType>> handler)
            : base(name, value)
        {
            SetCellValue = setCellValue;
            HandlerCell = handler;
        }

        public override void FillCell(ICell cell, ProductType item)
            => SetCellValue(cell, item);
    }
}
