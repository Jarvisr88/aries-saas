namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class ColumnHeaderFilterControl : ContentControl
    {
        public static readonly DependencyProperty ColumnFilterPopupProperty = DependencyPropertyManager.RegisterAttached("ColumnFilterPopup", typeof(PopupBaseEdit), typeof(ColumnHeaderFilterControl), new PropertyMetadata(null, new PropertyChangedCallback(ColumnHeaderFilterControl.OnColumnFilterPopupChanged)));

        public ColumnHeaderFilterControl()
        {
            base.Loaded += new RoutedEventHandler(this.ColumnHeaderFilterControl_Loaded);
            base.Unloaded += new RoutedEventHandler(this.ColumnHeaderFilterControl_Unloaded);
        }

        private void ColumnHeaderFilterControl_Loaded(object sender, RoutedEventArgs e)
        {
            base.Content = this.ColumnFilterPopup;
        }

        private void ColumnHeaderFilterControl_Unloaded(object sender, RoutedEventArgs e)
        {
            base.Content = null;
        }

        private void OnColumnFilterPopupChanged()
        {
            base.Content = this.ColumnFilterPopup;
        }

        private static void OnColumnFilterPopupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnHeaderFilterControl) d).OnColumnFilterPopupChanged();
        }

        public PopupBaseEdit ColumnFilterPopup
        {
            get => 
                (PopupBaseEdit) base.GetValue(ColumnFilterPopupProperty);
            set => 
                base.SetValue(ColumnFilterPopupProperty, value);
        }
    }
}

