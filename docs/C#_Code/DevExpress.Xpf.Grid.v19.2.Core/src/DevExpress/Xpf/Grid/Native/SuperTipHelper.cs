namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using System;
    using System.Linq;
    using System.Windows;

    public static class SuperTipHelper
    {
        public static readonly DependencyProperty SuperTipHeaderProperty = DependencyProperty.RegisterAttached("SuperTipHeader", typeof(object), typeof(SuperTipHelper), new PropertyMetadata(null, new PropertyChangedCallback(SuperTipHelper.OnSuperTipHeaderChanged)));
        public static readonly DependencyProperty SuperTipContentProperty = DependencyProperty.RegisterAttached("SuperTipContent", typeof(object), typeof(SuperTipHelper), new PropertyMetadata(null, new PropertyChangedCallback(SuperTipHelper.OnSuperTipContentChanged)));

        private static SuperTip GetSuperTip(DependencyObject d)
        {
            GalleryItem item = (GalleryItem) d;
            item.SuperTip ??= new SuperTip();
            return item.SuperTip;
        }

        public static object GetSuperTipContent(DependencyObject obj) => 
            obj.GetValue(SuperTipContentProperty);

        public static object GetSuperTipHeader(DependencyObject obj) => 
            obj.GetValue(SuperTipHeaderProperty);

        private static void OnSuperTipContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SuperTip superTip = GetSuperTip(d);
            if ((superTip.Items.LastOrDefault<SuperTipItemBase>() != null) && (superTip.Items.LastOrDefault<SuperTipItemBase>().GetType() == typeof(SuperTipItem)))
            {
                superTip.Items.Remove(superTip.Items.LastOrDefault<SuperTipItemBase>());
            }
            if (e.NewValue != null)
            {
                SuperTipItem item = new SuperTipItem();
                item.Content = e.NewValue;
                superTip.Items.Add(item);
            }
        }

        private static void OnSuperTipHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SuperTip superTip = GetSuperTip(d);
            if ((superTip.Items.FirstOrDefault<SuperTipItemBase>() != null) && (superTip.Items.FirstOrDefault<SuperTipItemBase>().GetType() == typeof(SuperTipHeaderItem)))
            {
                superTip.Items.Remove(superTip.Items.First<SuperTipItemBase>());
            }
            if (e.NewValue != null)
            {
                SuperTipHeaderItem item = new SuperTipHeaderItem();
                item.Content = e.NewValue;
                superTip.Items.Insert(0, item);
            }
        }

        public static void SetSuperTipContent(DependencyObject obj, object value)
        {
            obj.SetValue(SuperTipContentProperty, value);
        }

        public static void SetSuperTipHeader(DependencyObject obj, object value)
        {
            obj.SetValue(SuperTipHeaderProperty, value);
        }
    }
}

