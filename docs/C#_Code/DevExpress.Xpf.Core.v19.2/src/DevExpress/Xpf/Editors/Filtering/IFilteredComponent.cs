namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using System.Collections.Generic;

    public interface IFilteredComponent : IFilteredComponentBase
    {
        IEnumerable<FilterColumn> CreateFilterColumnCollection();
    }
}

