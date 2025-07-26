using System.Threading.Tasks;
using TestTask.Core;
using TestTask.Core.DataTable;
using TestTask.Core.Extension;
using TestTask.Core.Import;
using TestTask.Core.Models;

namespace TestTask.Model.Importer
{
    public class ExcelImpoterTable<T>
        : IExcelImpoterTable
        where T : Entity
    {
        private readonly IMessageBox _messageBox;
        private readonly BaseService<T> _service;
        private readonly ExcelImporter<T> _excelImporter;
        private readonly Table _table;

        public ExcelImpoterTable(IMessageBox messageBox, BaseService<T> service, ExcelImporter<T> excelImport, Table table)
        {
            _messageBox = messageBox;
            _service = service;
            _excelImporter = excelImport;
            _table = table;
        }

        public Table Table => _table;

        public async Task ImportAsync(string path)
        {
            var reader = _excelImporter.ImportFromFile(path);

            foreach (var item in reader)
            {
                if (item.Success)
                {
                    await _service.UpsertAsync(item.Value);
                }
            }

            if (!reader.IsNoErrorLine(out var message))
            {
                await _messageBox.ShowWarning(message, typeof(T).Name);
            }
        }
    }
}
