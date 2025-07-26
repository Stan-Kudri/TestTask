using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestTask.Core.DataTable
{
    public class SelectTable
    {
        protected IEnumerable<Table> _selectTable = new HashSet<Table>();

        public Table Table { get; set; }

        public SelectTable()
        {
        }

        public virtual IEnumerable<Table> SelectTables
        {
            get => _selectTable;
            set => _selectTable = value;
        }

        public ObservableCollection<Table> Items { get; set; } = new ObservableCollection<Table>(Table.List);
    }
}
