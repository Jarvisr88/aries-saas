namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class RowDataPropertyChangeSubscriber : IObjectListenerOwner
    {
        private readonly RowData rowData;
        private readonly ObjectListener listener;
        private readonly ItemPropertyNotificationMode notificationMode;

        public RowDataPropertyChangeSubscriber(RowData rowData, ItemPropertyNotificationMode notificationMode)
        {
            this.rowData = rowData;
            this.notificationMode = notificationMode;
            this.listener = new ObjectListener(this, notificationMode);
        }

        void IObjectListenerOwner.OnErrorsChanged(ObjectListener listener, object obj, string propertyName)
        {
            this.rowData.OnErrorsChanged(propertyName);
        }

        void IObjectListenerOwner.OnPropertyChanged(ObjectListener listener, object obj, string propertyName)
        {
            this.rowData.OnRowPropertyChanged(new PropertyChangedEventArgs(listener.GetDescriptorName(propertyName)));
        }

        void IObjectListenerOwner.OnPropertyChanging(ObjectListener listener, object obj, string propertyName)
        {
        }

        private ObservablePropertySchemeNode[] GetScheme() => 
            (this.DataControl == null) ? new ObservablePropertySchemeNode[0] : this.DataControl.ObservablePropertyScheme;

        public void SubcribePropertyChanged(object row)
        {
            if (this.notificationMode != ItemPropertyNotificationMode.None)
            {
                this.listener.Subscribe(row, this.GetScheme());
            }
        }

        public void UnsubcribePropertyChanged(object row)
        {
            if (this.notificationMode != ItemPropertyNotificationMode.None)
            {
                this.listener.Unsubscribe(row);
            }
        }

        public ItemPropertyNotificationMode NotificationMode =>
            this.notificationMode;

        private DataControlBase DataControl
        {
            get
            {
                Func<DataViewBase, DataControlBase> evaluator = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<DataViewBase, DataControlBase> local1 = <>c.<>9__7_0;
                    evaluator = <>c.<>9__7_0 = v => v.DataControl;
                }
                return this.rowData.View.With<DataViewBase, DataControlBase>(evaluator);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RowDataPropertyChangeSubscriber.<>c <>9 = new RowDataPropertyChangeSubscriber.<>c();
            public static Func<DataViewBase, DataControlBase> <>9__7_0;

            internal DataControlBase <get_DataControl>b__7_0(DataViewBase v) => 
                v.DataControl;
        }
    }
}

