namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class TabPanelSelector : IValueConverter
    {
        private ItemsPanelTemplate multiLineView;
        private ItemsPanelTemplate scrollView;
        private ItemsPanelTemplate stretchView;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public ItemsPanelTemplate MultiLineView { get; set; }

        public ItemsPanelTemplate ScrollView { get; set; }

        public ItemsPanelTemplate StretchView { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabPanelSelector.<>c <>9;
            public static Action<ItemsPanelTemplate> <>9__5_0;
            public static Action<ItemsPanelTemplate> <>9__8_0;
            public static Action<ItemsPanelTemplate> <>9__11_0;

            static <>c();
            internal void <set_MultiLineView>b__5_0(ItemsPanelTemplate x);
            internal void <set_ScrollView>b__8_0(ItemsPanelTemplate x);
            internal void <set_StretchView>b__11_0(ItemsPanelTemplate x);
        }
    }
}

