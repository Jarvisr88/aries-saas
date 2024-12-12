namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Specialized;
    using System.Windows;

    public class NullDataControlOriginationElement : IDataControlOriginationElement
    {
        public static readonly IDataControlOriginationElement Instance = new NullDataControlOriginationElement();
        private Locker synchronizationLocker = new Locker();
        private Locker columnsChangedLocker = new Locker();

        private NullDataControlOriginationElement()
        {
        }

        DataControlBase IDataControlOriginationElement.GetOriginationControl(DataControlBase sourceControl) => 
            sourceControl;

        void IDataControlOriginationElement.NotifyBeginInit(DataControlBase sourceElement, Func<DataControlBase, ISupportInitialize> getTarget)
        {
        }

        void IDataControlOriginationElement.NotifyCollectionChanged(DataControlBase sourceControl, Func<DataControlBase, IList> getCollection, Func<object, object> convertAction, NotifyCollectionChangedEventArgs e)
        {
        }

        void IDataControlOriginationElement.NotifyEndInit(DataControlBase sourceElement, Func<DataControlBase, ISupportInitialize> getTarget)
        {
        }

        void IDataControlOriginationElement.NotifyPropertyChanged(DataControlBase sourceControl, DependencyProperty property, Func<DataControlBase, DependencyObject> getTarget, Type baseComponentType)
        {
        }

        Locker IDataControlOriginationElement.SynchronizationLocker =>
            this.synchronizationLocker;

        Locker IDataControlOriginationElement.ColumnsChangedLocker =>
            this.columnsChangedLocker;
    }
}

