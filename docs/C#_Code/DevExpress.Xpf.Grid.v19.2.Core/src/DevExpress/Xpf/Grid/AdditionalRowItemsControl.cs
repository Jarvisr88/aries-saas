namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class AdditionalRowItemsControl : ItemsControl, INotifyCurrentViewChanged
    {
        public AdditionalRowItemsControl()
        {
            if (this.View != null)
            {
                this.UpdateRows();
            }
        }

        void INotifyCurrentViewChanged.OnCurrentViewChanged(DependencyObject d)
        {
            if (this.View != null)
            {
                this.View.TableViewBehavior.AdditionalRowItemsControl = this;
                this.UpdateRows();
            }
            foreach (object obj2 in (IEnumerable) base.Items)
            {
                if (obj2 is DependencyObject)
                {
                    DataControlBase.SetCurrentView((DependencyObject) obj2, this.View?.ViewBase);
                }
            }
        }

        private void UpdateFilterRow()
        {
            if ((base.Items != null) && (base.Items.Count == 0))
            {
                this.AddChild(this.View.ViewBase.CreateAutoFilterRowElement(this.View.AutoFilterRowData));
            }
        }

        public void UpdateRows()
        {
            if (base.Items != null)
            {
                if (base.Items.Count == 0)
                {
                    this.UpdateFilterRow();
                }
                if ((base.Items.Count == 1) && ((this.ViewBase != null) && ((this.ViewBase.ViewBehavior != null) && (this.ViewBase.ViewBehavior.IsNewItemRowVisible && (this.View.NewItemRowData != null)))))
                {
                    this.AddChild(this.ViewBase.CreateNewItemRowElement(this.View.NewItemRowData));
                }
            }
        }

        private DataViewBase ViewBase =>
            DataControlBase.GetCurrentView(this);

        private ITableView View =>
            DataControlBase.GetCurrentView(this) as ITableView;
    }
}

