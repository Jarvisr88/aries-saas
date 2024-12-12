namespace DevExpress.Utils.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;

    public interface IFilterItems : IEnumerable
    {
        void ApplyFilter();
        void CheckAllItems(bool isChecked);

        bool? CheckState { get; }

        bool CanAccept { get; }

        int Count { get; }

        IFilterItem this[int index] { get; }
    }
}

