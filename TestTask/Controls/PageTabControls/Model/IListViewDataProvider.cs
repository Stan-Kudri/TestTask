﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTask.Core.Models;
using TestTask.Core.Models.Page;

namespace TestTask.Controls.PageTabControls.Model
{
    public interface IListViewDataProvider
    {
        IReadOnlyList<ListViewColumn> Columns { get; }

        PagedList<Entity> GetPage(Page page);

        void Remove(Entity entity);

        Entity GetEntity(ListViewItem item);

        bool Edit(Entity entity);

        Task<bool> Add();
    }
}
