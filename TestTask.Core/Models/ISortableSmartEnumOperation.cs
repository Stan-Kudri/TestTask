﻿using System.Collections.Generic;

namespace TestTask.Core.Models
{
    public interface ISortableSmartEnumOperation<T>
    {
        static abstract ISortableSmartEnum<T> DefaultValue { get; }
        static abstract IReadOnlyCollection<ISortableSmartEnum<T>> List { get; }
    }
}
