namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class CalendarItemBase : Control
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(CalendarItemBase));

        protected CalendarItemBase()
        {
        }

        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }
    }
}

