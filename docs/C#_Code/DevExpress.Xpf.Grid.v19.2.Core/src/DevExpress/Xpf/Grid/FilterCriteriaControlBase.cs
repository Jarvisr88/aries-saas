namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class FilterCriteriaControlBase : ContentControl
    {
        public static readonly DependencyProperty ColumnProperty;
        public static readonly DependencyProperty DefaultIndexProperty;

        static FilterCriteriaControlBase()
        {
            ColumnProperty = DependencyProperty.Register("Column", typeof(ColumnBase), typeof(FilterCriteriaControlBase), new PropertyMetadata(null, (d, e) => ((FilterCriteriaControlBase) d).ColumnChanged((ColumnBase) e.OldValue)));
            DefaultIndexProperty = DependencyProperty.Register("DefaultIndex", typeof(int), typeof(FilterCriteriaControlBase), new PropertyMetadata(-1));
        }

        protected FilterCriteriaControlBase()
        {
        }

        protected abstract void ColumnChanged(ColumnBase oldColumn);

        public ColumnBase Column
        {
            get => 
                (ColumnBase) base.GetValue(ColumnProperty);
            set => 
                base.SetValue(ColumnProperty, value);
        }

        public int DefaultIndex
        {
            get => 
                (int) base.GetValue(DefaultIndexProperty);
            set => 
                base.SetValue(DefaultIndexProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterCriteriaControlBase.<>c <>9 = new FilterCriteriaControlBase.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterCriteriaControlBase) d).ColumnChanged((ColumnBase) e.OldValue);
            }
        }
    }
}

