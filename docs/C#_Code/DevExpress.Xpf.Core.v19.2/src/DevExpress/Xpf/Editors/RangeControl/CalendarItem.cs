namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Windows;

    public class CalendarItem : CalendarItemBase
    {
        public static readonly DependencyProperty IsSelectedProperty;

        static CalendarItem()
        {
            Type ownerType = typeof(CalendarItem);
            IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
        }

        public CalendarItem()
        {
            base.DefaultStyleKey = typeof(CalendarItem);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }
    }
}

