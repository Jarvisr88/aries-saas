namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public abstract class BaseBarManagerSerializationStrategy
    {
        public static readonly DependencyProperty StrategyProperty;

        static BaseBarManagerSerializationStrategy();
        protected BaseBarManagerSerializationStrategy();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static BaseBarManagerSerializationStrategy GetStrategy(DependencyObject obj);
        public static void SetStrategy(DependencyObject obj, BaseBarManagerSerializationStrategy value);
    }
}

