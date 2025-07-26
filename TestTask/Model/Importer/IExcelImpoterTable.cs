using System.Threading.Tasks;
using TestTask.Core.DataTable;

namespace TestTask.Model.Importer
{
    public interface IExcelImpoterTable
    {
        Table Table { get; }

        Task ImportAsync(string path);
    }
}
