namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DetailRowContentPresenter : ContentPresenter
    {
        public static readonly DependencyProperty MasterRowDataProperty = DependencyPropertyManager.Register("MasterRowData", typeof(RowData), typeof(DetailRowContentPresenter), new PropertyMetadata(null));

        public RowData MasterRowData
        {
            get => 
                (RowData) base.GetValue(MasterRowDataProperty);
            set => 
                base.SetValue(MasterRowDataProperty, value);
        }
    }
}

