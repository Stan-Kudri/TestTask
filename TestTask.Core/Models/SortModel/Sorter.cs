using System.Collections.Generic;
using System.Linq;

namespace TestTask.Core.Models.SortModel
{
    public sealed class Sorter<T>(ISortableField<T> defaultValue)
    {
        public IQueryable<T> Apply(IQueryable<T> items, IEnumerable<ISortableField<T>> sortFields, bool? ascending = true)
        {
            if (ascending == null)
            {
                return items;
            }

            var asc = ascending.Value;
            var actualSortFields = sortFields.ToList();
            if (actualSortFields.Count == 0)
            {
                actualSortFields.Add(defaultValue);
            }

            var query = actualSortFields[0].OrderBy(items, asc);
            foreach (var item in actualSortFields.Skip(1))
            {
                query = item.ThenBy(query, asc);
            }

            return query;
        }
    }
}
