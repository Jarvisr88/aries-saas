namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class DetailRowControlBase : ContentControl
    {
        public static readonly DependencyProperty MasterRowDataProperty = DependencyPropertyManager.Register("MasterRowData", typeof(RowData), typeof(DetailRowControlBase), new PropertyMetadata(null));

        protected DetailRowControlBase()
        {
        }

        public RowData MasterRowData
        {
            get => 
                (RowData) base.GetValue(MasterRowDataProperty);
            set => 
                base.SetValue(MasterRowDataProperty, value);
        }
    }
}

