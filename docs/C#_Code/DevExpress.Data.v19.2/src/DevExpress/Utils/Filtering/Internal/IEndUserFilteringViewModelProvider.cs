namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.MVVM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IEndUserFilteringViewModelProvider : IEndUserFilteringViewModelPropertyValues, IEnumerable<IEndUserFilteringMetricViewModel>, IEnumerable, IEndUserFilteringCriteriaChangeAware
    {
        void ClearFilterCriteria();
        CriteriaOperator GetFilterCriteria(string path);
        void Reset();
        void RetrieveFields(Action<Type> retrieveFields, Type sourceType, IEnumerable<IEndUserFilteringMetricAttributes> attributes = null, Type viewModelBaseType = null);
        void UpdateMemberBindings(string path);

        IViewModelProvider ParentViewModelProvider { get; set; }

        object ParentViewModel { get; set; }

        Type SourceType { get; set; }

        IEnumerable<IEndUserFilteringMetricAttributes> Attributes { get; set; }

        IEndUserFilteringSettings Settings { get; }

        IEndUserFilteringViewModelProperties Properties { get; }

        IEndUserFilteringViewModelBindableProperties BindableProperties { get; }

        IEndUserFilteringViewModelPropertyValues PropertyValues { get; }

        Type ViewModelBaseType { get; set; }

        Type ViewModelType { get; }

        object ViewModel { get; }

        bool IsViewModelTypeCreated { get; }

        bool IsViewModelCreated { get; }

        CriteriaOperator FilterCriteria { get; }
    }
}

