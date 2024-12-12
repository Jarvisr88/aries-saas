namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;

    public class DefaultTemplateSelector : DataTemplateSelector
    {
        [ThreadStatic]
        private static IFrameworkElementResourceAPIWrapper feWrapper;
        private static DataTemplateSelector defaultSelector;
        private static Func<DependencyObject, object, Type, object> findTemplateResourceFunc;
        private static Lazy<DefaultTemplateSelector> instance;

        static DefaultTemplateSelector();
        internal static DataTemplate FindTemplateResourceInternal(DependencyObject container, object item, Type type);
        internal static object FindTemplateResourceInTree(DependencyObject target, ArrayList keys, int exactMatch, ref int bestMatch);
        public override DataTemplate SelectTemplate(object item, DependencyObject container);

        public static DefaultTemplateSelector Instance { get; }

        private static Func<DependencyObject, object, Type, object> FindTemplateResourceFunc { get; }

        private static DataTemplateSelector DefaultSelector { get; }
    }
}

