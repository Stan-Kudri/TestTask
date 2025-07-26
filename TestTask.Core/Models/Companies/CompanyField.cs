using System;
using NPOI.SS.UserModel;
using TestTask.Core.Export.SheetFillers;
using TestTask.Core.Extension;
using TestTask.Core.Import.Importers;

namespace TestTask.Core.Models.Companies
{
    public class CompanyField : SheetField<Company, CompanyField>, IFieldHandler<Company>
    {
        public static CompanyField ID = new("ID", 0,
                                            (field, item) => field.SetCellValue(item.Id),
                                            (model, row, idx) =>
                                            {
                                                var res = row.GetInt(idx, "Id");
                                                if (!res.Success) return res.ToError<Company>();
                                                model.Id = res.Value;
                                                return Result<Company>.CreateSuccess(model, row.RowNum);
                                            });

        public static CompanyField NameCompany = new("Name", 1,
                                                     (field, item) => field.SetCellValue(item.Name),
                                                     (model, row, idx) =>
                                                     {
                                                         var res = row.GetString(idx, "Name");
                                                         if (!res.Success) return res.ToError<Company>();
                                                         if (string.IsNullOrEmpty(res.Value))
                                                             return Result<Company>.CreateFail("Name should not be empty", row.RowNum);
                                                         model.Name = res.Value;
                                                         return Result<Company>.CreateSuccess(model, row.RowNum);
                                                     });

        public static CompanyField DateCreation = new("DateCreation", 2,
                                                      (field, item) => field.SetCellValue(item.DateCreation.ToString("d")),
                                                      (model, row, idx) =>
                                                      {
                                                          var res = row.GetDate(idx, "DateCreation");
                                                          if (!res.Success) return res.ToError<Company>();
                                                          model.DateCreation = res.Value;
                                                          return Result<Company>.CreateSuccess(model, row.RowNum);
                                                      });

        public static CompanyField Country = new("Country", 3,
                                                 (field, item) => field.SetCellValue(item.Country),
                                                 (model, row, idx) =>
                                                 {
                                                     var res = row.GetString(idx, "Country");
                                                     if (!res.Success) return res.ToError<Company>();
                                                     if (string.IsNullOrEmpty(res.Value))
                                                         return Result<Company>.CreateFail("Country should not be empty", row.RowNum);
                                                     model.Country = res.Value;
                                                     return Result<Company>.CreateSuccess(model, row.RowNum);
                                                 });

        private readonly Action<ICell, Company> SetCellValue;

        public Func<Company, IRow, int, Result<Company>> HandlerCell { get; }

        public string ColumnName => Name;

        private CompanyField(string name, int value,
                             Action<ICell, Company> setCellValue,
                             Func<Company, IRow, int, Result<Company>> handler)
            : base(name, value)
        {
            SetCellValue = setCellValue;
            HandlerCell = handler;
        }

        public override void FillCell(ICell cell, Company item)
            => SetCellValue(cell, item);
    }
}
