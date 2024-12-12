namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class CustomColumnFilterContentPresenter : ContentPresenter
    {
        public static readonly DependencyProperty CustomColumnFilterProperty = DependencyProperty.Register("CustomColumnFilter", typeof(CriteriaOperator), typeof(CustomColumnFilterContentPresenter), new PropertyMetadata(null, new PropertyChangedCallback(CustomColumnFilterContentPresenter.OnCustomColumnFilterChanged)));
        public static readonly DependencyProperty ColumnFilterInfoProperty = DependencyProperty.Register("ColumnFilterInfo", typeof(CustomColumnFilterInfo), typeof(CustomColumnFilterContentPresenter), new PropertyMetadata(null, new PropertyChangedCallback(CustomColumnFilterContentPresenter.OnColumnFilterInfoChanged)));

        private void OnColumnFilterInfoChanged()
        {
            if (this.ColumnFilterInfo != null)
            {
                this.CustomColumnFilter = this.ColumnFilterInfo.CustomColumnFilter;
                base.ContentTemplate = this.ColumnFilterInfo.Column.CustomColumnFilterPopupTemplate;
            }
        }

        private static void OnColumnFilterInfoChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((CustomColumnFilterContentPresenter) obj).OnColumnFilterInfoChanged();
        }

        private void OnCustomColumnFilterChanged()
        {
            if (this.ColumnFilterInfo != null)
            {
                this.ColumnFilterInfo.CustomColumnFilter = this.CustomColumnFilter;
            }
        }

        private static void OnCustomColumnFilterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((CustomColumnFilterContentPresenter) obj).OnCustomColumnFilterChanged();
        }

        public CriteriaOperator CustomColumnFilter
        {
            get => 
                (CriteriaOperator) base.GetValue(CustomColumnFilterProperty);
            set => 
                base.SetValue(CustomColumnFilterProperty, value);
        }

        public CustomColumnFilterInfo ColumnFilterInfo
        {
            get => 
                (CustomColumnFilterInfo) base.GetValue(ColumnFilterInfoProperty);
            set => 
                base.SetValue(ColumnFilterInfoProperty, value);
        }
    }
}

