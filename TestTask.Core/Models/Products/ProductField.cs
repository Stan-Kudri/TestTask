using System;
using NPOI.SS.UserModel;
using TestTask.Core.Export.SheetFillers;
using TestTask.Core.Extension;
using TestTask.Core.Import.Importers;

namespace TestTask.Core.Models.Products
{
    public class ProductField : SheetField<Product, ProductField>, IFieldHandler<Product>
    {
        public static ProductField ID = new("ID", 0,
                                            (field, item) => field.SetCellValue(item.Id),
                                            (model, row, idx) =>
                                            {
                                                var res = row.GetInt(idx, "Id");
                                                if (!res.Success) return res.ToError<Product>();
                                                model.Id = res.Value;
                                                return Result<Product>.CreateSuccess(model, row.RowNum);
                                            });

        public static ProductField NameProduct = new("Name", 1,
                                                     (field, item) => field.SetCellValue(item.Name),
                                                     (model, row, idx) =>
                                                     {
                                                         var res = row.GetString(idx, "Name");
                                                         if (!res.Success) return res.ToError<Product>();
                                                         if (string.IsNullOrEmpty(res.Value))
                                                             return Result<Product>.CreateFail("Name should not be empty", row.RowNum);
                                                         model.Name = res.Value;
                                                         return Result<Product>.CreateSuccess(model, row.RowNum);
                                                     });

        public static ProductField CompanyId = new("CompanyId", 2,
                                                         (field, item) => field.SetCellValue(item.CompanyId),
                                                         (model, row, idx) =>
                                                         {
                                                             var res = row.GetInt(idx, "CompanyId");
                                                             if (!res.Success) return res.ToError<Product>();
                                                             model.CompanyId = res.Value;
                                                             return Result<Product>.CreateSuccess(model, row.RowNum);
                                                         });

        public static ProductField CategoryId = new("CategoryId", 3,
                                                         (field, item) => field.SetCellValue(item.CategoryId),
                                                         (model, row, idx) =>
                                                         {
                                                             var res = row.GetInt(idx, "CategoryId");
                                                             if (!res.Success) return res.ToError<Product>();
                                                             model.CategoryId = res.Value;
                                                             return Result<Product>.CreateSuccess(model, row.RowNum);
                                                         });

        public static ProductField TypeId = new("TypeId", 4,
                                                         (field, item) => field.SetCellValue(item.TypeId),
                                                         (model, row, idx) =>
                                                         {
                                                             var res = row.GetInt(idx, "TypeId");
                                                             if (!res.Success) return res.ToError<Product>();
                                                             model.TypeId = res.Value;
                                                             return Result<Product>.CreateSuccess(model, row.RowNum);
                                                         });

        public static ProductField Price = new("Price", 5,
                                               (field, item) => field.SetCellValue(Convert.ToDouble(item.Price)),
                                               (model, row, idx) =>
                                               {
                                                   var res = row.GetDecimal(idx, "Price");
                                                   if (!res.Success) return res.ToError<Product>();
                                                   model.Price = res.Value;
                                                   return Result<Product>.CreateSuccess(model, row.RowNum);
                                               });

        public static ProductField Destination = new("Destination", 6,
                                                     (field, item) => field.SetCellValue(item.Destination),
                                                     (model, row, idx) =>
                                                     {
                                                         var res = row.GetString(idx, "Destination");
                                                         if (!res.Success) return res.ToError<Product>();
                                                         model.Destination = res.Value;
                                                         return Result<Product>.CreateSuccess(model, row.RowNum);
                                                     });

        private readonly Action<ICell, Product> SetCellValue;

        public Func<Product, IRow, int, Result<Product>> HandlerCell { get; }

        public string ColumnName => Name;

        private ProductField(string name, int value,
                             Action<ICell, Product> setCellValue,
                             Func<Product, IRow, int, Result<Product>> handler)
            : base(name, value)
        {
            SetCellValue = setCellValue;
            HandlerCell = handler;
        }

        public override void FillCell(ICell cell, Product item)
            => SetCellValue(cell, item);
    }
}
