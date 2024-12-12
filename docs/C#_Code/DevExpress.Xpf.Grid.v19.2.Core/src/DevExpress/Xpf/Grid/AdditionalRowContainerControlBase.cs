namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public abstract class AdditionalRowContainerControlBase : Control, IAdditionalRowElement
    {
        public static readonly DependencyProperty RowTemplateProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty IndicatorWidthProperty;

        static AdditionalRowContainerControlBase()
        {
            Type ownerType = typeof(AdditionalRowContainerControlBase);
            RowTemplateProperty = DependencyProperty.Register("RowTemplate", typeof(ControlTemplate), ownerType, null);
            ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), ownerType, null);
            IndicatorWidthProperty = DependencyProperty.Register("IndicatorWidth", typeof(double), ownerType, new PropertyMetadata(16.0));
        }

        protected AdditionalRowContainerControlBase()
        {
            base.SetBinding(RowData.RowDataProperty, new Binding());
            base.SetBinding(RowData.CurrentRowDataProperty, new Binding());
        }

        public ControlTemplate RowTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(RowTemplateProperty);
            set => 
                base.SetValue(RowTemplateProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public double IndicatorWidth
        {
            get => 
                (double) base.GetValue(IndicatorWidthProperty);
            set => 
                base.SetValue(IndicatorWidthProperty, value);
        }

        protected internal abstract int RowHandle { get; }

        int IAdditionalRowElement.RowHandle =>
            this.RowHandle;

        bool IAdditionalRowElement.LockDataContext =>
            false;

        FrameworkElement IAdditionalRowElement.AdditionalElement =>
            this;

        DataViewBase IAdditionalRowElement.RowCurrentView
        {
            get
            {
                RowDataBase dataContext = base.DataContext as RowDataBase;
                return dataContext?.View;
            }
        }
    }
}

