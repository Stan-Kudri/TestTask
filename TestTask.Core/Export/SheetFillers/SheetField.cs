using Ardalis.SmartEnum;
using NPOI.SS.UserModel;
using TestTask.Core.Models;

namespace TestTask.Core.Export.SheetFillers
{
    public abstract class SheetField<T, TField> : SmartEnum<TField>
        where TField : SmartEnum<TField>
        where T : Entity
    {
        protected SheetField(string name, int value) : base(name, value)
        {
        }

        public abstract void FillCell(ICell cell, T item);
    }
}
