namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Specialized;
    using System.Windows;

    public interface IDataControlOriginationElement
    {
        DataControlBase GetOriginationControl(DataControlBase sourceControl);
        void NotifyBeginInit(DataControlBase sourceElement, Func<DataControlBase, ISupportInitialize> getTarget);
        void NotifyCollectionChanged(DataControlBase sourceControl, Func<DataControlBase, IList> getCollection, Func<object, object> convertAction, NotifyCollectionChangedEventArgs e);
        void NotifyEndInit(DataControlBase sourceElement, Func<DataControlBase, ISupportInitialize> getTarget);
        void NotifyPropertyChanged(DataControlBase sourceControl, DependencyProperty property, Func<DataControlBase, DependencyObject> getTarget, Type baseComponentType);

        Locker SynchronizationLocker { get; }

        Locker ColumnsChangedLocker { get; }
    }
}

