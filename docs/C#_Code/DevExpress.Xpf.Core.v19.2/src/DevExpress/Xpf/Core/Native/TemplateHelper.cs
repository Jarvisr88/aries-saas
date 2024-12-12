namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TemplateHelper
    {
        public static ContentPresenter CreateBindedContentPresenter(object source, string contentTemplatePropertyName);
        public static ContentPresenter CreateBindedContentPresenter(object source, string contentTemplatePropertyName, object dataContext);
        public static T LoadFromTemplate<T>(DataTemplate template) where T: class;
        public static T LoadFromTemplate<T>(DataTemplate template, Func<object, object> coerceItem) where T: class;
        public static object LoadFromTemplate(DataTemplate template, Type valueType, Func<object, object> coerceItem = null);
        private static bool LoadFromTemplateImpl(DataTemplate template, Type valueType, out object result, Func<object, object> coerceItem = null);
        public static IEnumerable<T> LoadItemsFromTemplate<T>(DataTemplate template) where T: class;
        [IteratorStateMachine(typeof(TemplateHelper.<LoadItemsFromTemplate>d__4))]
        public static IEnumerable LoadItemsFromTemplate(DataTemplate template, Type valueType);

    }
}

