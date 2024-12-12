namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IPredefinedFilter
    {
        string Name { get; }

        CriteriaOperator Filter { get; }
    }
}

