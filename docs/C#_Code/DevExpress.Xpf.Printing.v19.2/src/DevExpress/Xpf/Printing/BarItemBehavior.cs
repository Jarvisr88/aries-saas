namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public static class BarItemBehavior
    {
        public static readonly DependencyProperty SendZoomToModelOnClickProperty = DependencyPropertyManager.RegisterAttached("SendZoomToModelOnClick", typeof(PreviewModelBase), typeof(BarItemBehavior), new PropertyMetadata(null, new PropertyChangedCallback(BarItemBehavior.OnSendZoomToModelOnClickChanged)));

        private static void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem item = (BarButtonItem) e.Item;
            PreviewModelBase sendZoomToModelOnClick = GetSendZoomToModelOnClick(item);
            if (sendZoomToModelOnClick == null)
            {
                throw new InvalidProgramException();
            }
            sendZoomToModelOnClick.ZoomMode = (ZoomItemBase) item.CommandParameter;
        }

        public static PreviewModelBase GetSendZoomToModelOnClick(DependencyObject obj) => 
            (PreviewModelBase) obj.GetValue(SendZoomToModelOnClickProperty);

        private static void OnSendZoomToModelOnClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BarButtonItem item = d as BarButtonItem;
            if (item == null)
            {
                throw new NotSupportedException();
            }
            if (e.OldValue is PreviewModelBase)
            {
                item.ItemClick -= new ItemClickEventHandler(BarItemBehavior.barButtonItem_ItemClick);
            }
            if (e.NewValue is PreviewModelBase)
            {
                item.ItemClick += new ItemClickEventHandler(BarItemBehavior.barButtonItem_ItemClick);
            }
        }

        public static void SetSendZoomToModelOnClick(DependencyObject obj, PreviewModelBase value)
        {
            obj.SetValue(SendZoomToModelOnClickProperty, value);
        }
    }
}

