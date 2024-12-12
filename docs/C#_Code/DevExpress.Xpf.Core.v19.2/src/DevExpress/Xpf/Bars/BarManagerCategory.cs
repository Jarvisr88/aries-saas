namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarManagerCategory : DependencyObject
    {
        public static readonly DependencyProperty NameProperty;
        public static readonly DependencyProperty CaptionProperty;
        public static readonly DependencyProperty VisibleProperty;

        static BarManagerCategory();
        public virtual List<BarItem> GetBarItems();
        protected virtual void OnVisibleChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void UpdateItems();

        [Description("Gets or sets the category's name.This is a dependency property.")]
        public string Name { get; set; }

        [Description("Gets or sets whether the category is visible in the Customization Window at runtime.This is a dependency property.")]
        public bool Visible { get; set; }

        [Description("Gets or sets the category's display caption.")]
        public string Caption { get; set; }

        protected internal bool IsSpecialCategory { get; set; }

        protected internal DependencyObject ScopeNode { get; set; }
    }
}

