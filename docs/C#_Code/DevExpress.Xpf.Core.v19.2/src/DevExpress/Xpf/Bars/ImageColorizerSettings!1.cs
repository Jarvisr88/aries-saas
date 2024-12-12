namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("States")]
    public class ImageColorizerSettings<T>
    {
        public ImageColorizerSettings();
        public virtual void Apply(DependencyObject target, T state);

        public Dictionary<T, Color> States { get; set; }
    }
}

