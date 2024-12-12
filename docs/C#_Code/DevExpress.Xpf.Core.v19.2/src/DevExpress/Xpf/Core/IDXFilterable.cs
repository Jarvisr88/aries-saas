namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IDXFilterable
    {
        event RoutedEventHandler FilterChanged;

        CriteriaOperator FilterCriteria { get; set; }

        CriteriaOperator SearchFilterCriteria { get; }

        bool IsFilterEnabled { get; set; }
    }
}

