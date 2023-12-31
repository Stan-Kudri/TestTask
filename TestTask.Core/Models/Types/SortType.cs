﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestTask.Core.Models.Types
{
    public class SortType : ISortEntity<ProductType, ProductTypeSortType>
    {
        private IEnumerable<ProductTypeSortType> _sortFields = new HashSet<ProductTypeSortType>() { ProductTypeSortType.Id };

        public ProductTypeSortType sortType { get; set; }

        public SortType()
        {
        }

        public virtual IEnumerable<ProductTypeSortType> SortFields
        {
            get => _sortFields;
            set => _sortFields = value;
        }
        public ObservableCollection<ProductTypeSortType> Items { get; set; } = new ObservableCollection<ProductTypeSortType>(ProductTypeSortType.List);

        public IQueryable<ProductType> Apply(IQueryable<ProductType> items, bool? ascending = true)
        {
            var sorter = new Sorter<ProductType, ProductTypeSortType>(ProductTypeSortType.Id);
            return sorter.Apply(items, _sortFields, ascending);
        }

        public void Clear() => _sortFields = new HashSet<ProductTypeSortType>();
    }
}
