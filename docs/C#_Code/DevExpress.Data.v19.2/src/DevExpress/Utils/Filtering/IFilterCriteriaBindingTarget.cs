namespace DevExpress.Utils.Filtering
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;

    public interface IFilterCriteriaBindingTarget : INotifyPropertyChanged
    {
        IDisposable Lock();
        void RaiseFilterCriteriaChanged();

        CriteriaOperator FilterCriteria { get; set; }
    }
}

