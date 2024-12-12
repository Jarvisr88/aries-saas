namespace DMEWorks.Forms
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public interface IGridSource : IBindingList, IList, ICollection, IEnumerable
    {
        void ApplyFilterText(string text);
    }
}

