using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Core;
using TestTask.Core.DataTable;
using TestTask.Forms;

namespace TestTask.ChildForms.Import
{
    public partial class ImportDatabaseForm : BaseForm
    {
        private readonly IMessageBox _messageBox;

        public ImportDatabaseForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _messageBox = serviceProvider.GetRequiredService<IMessageBox>();
            cbCompany.Checked
                = cbCategory.Checked
                = cbType.Checked
                = cbProduct.Checked
                = true;
        }

        private readonly Dictionary<Table, bool> SelectTables = new Dictionary<Table, bool>()
        {
            { Table.Company, true},
            { Table.Category, true},
            { Table.TypeProduct, true},
            { Table.Product, true}
        };

        public HashSet<Table> GetSelectTable()
        {
            var selectTable = new HashSet<Table>();

            foreach (var table in SelectTables)
            {
                if (table.Value)
                {
                    selectTable.Add(table.Key);
                }
            }

            return selectTable;
        }

        private async void BtnImportData_Click(object sender, EventArgs e)
        {
            if (!SelectTables.ContainsValue(true))
            {
                await _messageBox.ShowWarning("The tables to load are not selected.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cbCompany_CheckedChanged(object sender, EventArgs e)
            => SelectTables[Table.Company] = cbCompany.Checked;

        private void cbProduct_CheckedChanged(object sender, EventArgs e)
            => SelectTables[Table.Product] = cbProduct.Checked;

        private void cbType_CheckedChanged(object sender, EventArgs e)
            => SelectTables[Table.TypeProduct] = cbType.Checked;

        private void cbCategory_CheckedChanged(object sender, EventArgs e)
            => SelectTables[Table.Category] = cbCategory.Checked;
    }
}
