namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    public interface IEndUserFilteringViewModelBindableProperties : IBindingList, IList, ICollection, IEnumerable, ITypedList
    {
        PropertyDescriptor this[string path] { get; }
    }
}

