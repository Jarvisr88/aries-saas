namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class CellItemsControlBase : GridCachedItemsControl, INotifyCurrentViewChanged, ILayoutNotificationHelperOwner
    {
        private LayoutNotificationHelper layoutNotificationHelper;

        public CellItemsControlBase()
        {
            this.layoutNotificationHelper = new LayoutNotificationHelper(this);
        }

        protected sealed override FrameworkElement CreateChild(object item) => 
            this.CreateChildCore((GridCellData) item);

        protected abstract FrameworkElement CreateChildCore(GridCellData cellData);
        void ILayoutNotificationHelperOwner.InvalidateMeasure()
        {
            base.InvalidateMeasure();
        }

        void INotifyCurrentViewChanged.OnCurrentViewChanged(DependencyObject d)
        {
            this.OnCurrentViewChanged();
        }

        private void DoInvalidateMeasure()
        {
            if (this.View != null)
            {
                UIElement reference = this;
                while (true)
                {
                    reference = VisualTreeHelper.GetParent(reference) as UIElement;
                    if ((reference == null) || ReferenceEquals(reference, this.View.DataPresenter))
                    {
                        return;
                    }
                    reference.InvalidateMeasure();
                }
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.layoutNotificationHelper.Subscribe();
            return base.MeasureOverride(constraint);
        }

        protected override void OnCollectionChanged()
        {
            base.OnCollectionChanged();
            this.DoInvalidateMeasure();
        }

        protected virtual void OnCurrentViewChanged()
        {
            base.InvalidateMeasure();
        }

        protected sealed override void ValidateElement(FrameworkElement element, object item)
        {
            GridCellData cellData = (GridCellData) item;
            if (cellData.Column != null)
            {
                this.ValidateElementCore(element, cellData);
            }
        }

        protected abstract void ValidateElementCore(FrameworkElement element, GridCellData cellData);

        protected override DataViewBase View =>
            DataControlBase.GetCurrentView(this);

        DependencyObject ILayoutNotificationHelperOwner.NotificationManager =>
            this.View?.DataControl;
    }
}

