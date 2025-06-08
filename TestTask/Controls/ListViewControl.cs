using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TestTask.BindingItem;
using TestTask.Controls.PageTabControls.Model;
using TestTask.Core;
using TestTask.Core.Models;
using TestTask.Core.Models.Page;

namespace TestTask.Controls
{
    public partial class ListViewControl : BaseUserControl
    {
        private const string MessageNotSelectedItem = "No items selected";
        private const int NoItemsSelected = 0;

        private IListViewDataProvider _provider;
        private IMessageBox _messageBox;

        private PagedList<Entity> _pagedList;

        private bool Resizing = false;
        private float[] _percentages;

        public ListViewControl()
            : base()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PageModel Page { get; set; } = new PageModel();

        public void Initialize(IListViewDataProvider provider, IMessageBox messageBox)
        {
            _provider = provider;
            _messageBox = messageBox;

            //Initialize AutoSize ListView
            float initialTotalColumnWidth = 0;
            foreach (var column in provider.Columns)
            {
                listView.Columns.Add(new ColumnHeader { Text = column.Name, Width = column.Width });
                initialTotalColumnWidth += column.Width;
            }
            _percentages = provider.Columns.Select(c => c.Width / initialTotalColumnWidth).ToArray();

            cmbPageSize.DataSource = Page.Items;
            Page.ChangeCurrentPage += LoadData;
            ResizeListView();
        }

        public void Closing() => Page.ChangeCurrentPage -= LoadData;

        private async void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (await _provider.Add())
            {
                LoadData();
            }
        }

        private async void BtnEditItem_Click(object sender, EventArgs e)
        {
            var indexEditItem = listView.SelectedIndices.Cast<int>();

            if (indexEditItem.Count() != 1)
            {
                await _messageBox.ShowWarning("Select one item.");
                return;
            }

            var row = indexEditItem.First();
            var rowItem = listView.Items[row];
            var entity = _provider.GetEntity(rowItem);

            if (_provider.Edit(entity))
            {
                LoadData();
            }
        }

        private async void BtnDeleteItems_Click(object sender, EventArgs e)
        {
            var selectedRowIndex = listView.SelectedIndices;

            if (selectedRowIndex.Count == NoItemsSelected)
            {
                await _messageBox.ShowWarning(MessageNotSelectedItem);
                return;
            }

            if (!await _messageBox.ShowQuestion("Delete selected items?"))
            {
                foreach (ListViewItem item in listView.Items)
                {
                    item.Selected = false;
                }
                return;
            }

            for (var i = 0; i < listView.Items.Count; i++)
            {
                var item = listView.Items[i];
                if (!item.Selected)
                {
                    continue;
                }

                var entity = _provider.GetEntity(item);
                _provider.Remove(entity);
            }

            LoadData();
        }

        private void BtnFirstPage_Click(object sender, EventArgs e)
        {
            if (_pagedList.HasPrevious)
            {
                Page.Number = 1;
            }
        }

        private void BtnBackPage_Click(object sender, EventArgs e)
        {
            if (_pagedList.HasPrevious)
            {
                Page.Number--;
            }
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            if (_pagedList.HasNext)
            {
                Page.Number++;
            }
        }

        private void BtnLastPage_Click(object sender, EventArgs e)
        {
            if (_pagedList.HasNext)
            {
                Page.Number = _pagedList.PageCount;
            }
        }

        protected void TbCurrentPage_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCurrentPage.Text, out var pageNumber)
                && pageNumber <= _pagedList.PageCount
                && _pagedList.PageNumber == Page.Number)
            {
                Page.Number = pageNumber;
            }

            tbCurrentPage.Text = string.Format("{0}/{1}", Page.Number, _pagedList.PageCount);
        }

        protected void CmbPageSize_Changed(object sender, EventArgs e)
        {
            var pageSizeCmb = Page.Items[cmbPageSize.SelectedIndex];

            if (Page.ChangedPage(pageSizeCmb))
            {
                Page.Size = pageSizeCmb;
                Page.Number = 1;
            }
        }

        private void ListView_SizeChanged(object sender, EventArgs e) => ChangeSizeColumnListView();

        public void ChangeSizeColumnListView()
        {
            if (Resizing)
            {
                return;
            }

            Resizing = true;
            ResizeListView();
            Resizing = false;
        }

        public void LoadData()
        {
            _pagedList = _provider.GetPage(Page.GetPage());
            if (IsNotFirstPageEmpty())
            {
                Page.Number -= 1;
            }

            listView.Items.Clear();
            foreach (var item in _pagedList.Items)
            {
                var values = _provider.Columns
                    .Select(cl => cl.ValueSelector(item).ToString())
                    .ToArray();
                listView.Items.Add(new ListViewItem(values));
            }
            UpdateButtons();

            tbCurrentPage.Text = _pagedList.PageNumber.ToString();
        }

        private void UpdateButtons()
        {
            tlpPagedCompanies.Visible =
                btnFirstPage.Enabled = btnFirstPage.Visible =
                    btnBackPage.Enabled = btnBackPage.Visible =
                        btnNextPage.Enabled = btnNextPage.Visible =
                            btnLastPage.Enabled = btnLastPage.Visible =
                                tbCurrentPage.Enabled = tbCurrentPage.Visible =
                                    _pagedList.PageCount > 0 ? true : false;
        }

        private bool IsNotFirstPageEmpty() => _pagedList.Count == 0 && Page.Number != 1;

        private void ResizeListView()
        {
            //Column Tage = Width custom size
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                listView.Columns[i].Width = (int)(_percentages[i] * listView.ClientRectangle.Width);
            }
        }
    }
}
