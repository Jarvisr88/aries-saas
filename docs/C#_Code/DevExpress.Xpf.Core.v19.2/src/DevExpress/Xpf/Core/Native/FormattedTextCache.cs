namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class FormattedTextCache
    {
        private readonly FrameworkElement owner;
        private System.Windows.Media.Typeface typeface;
        private Dictionary<string, FormattedText> cache;

        public FormattedTextCache(FrameworkElement owner);
        private FormattedText CreateFormattedText(string text);
        private System.Windows.Media.Typeface CreateTypeface();
        public FormattedText Get(string text);
        public void Invalidate();
        public static void Register<T>(DependencyPropertyRegistrator<T> registrator, Action<T> propertyChangedCallback) where T: DependencyObject;

        public static IEnumerable<DependencyProperty> DependentProperties { [IteratorStateMachine(typeof(FormattedTextCache.<get_DependentProperties>d__1))] get; }

        private System.Windows.Media.Typeface Typeface { get; }

    }
}

