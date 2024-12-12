namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public static class InlineTextService
    {
        public static readonly DependencyProperty InlineInfoProperty = DependencyProperty.RegisterAttached("InlineInfo", typeof(InlineCollectionInfo), typeof(InlineTextService), new PropertyMetadata(null, new PropertyChangedCallback(InlineTextService.OnInlineInfoChanged)));

        public static InlineCollectionInfo GetInlineInfo(DependencyObject obj) => 
            (InlineCollectionInfo) obj.GetValue(InlineInfoProperty);

        private static void OnInlineInfoChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as TextBlock).Do<TextBlock>(t => UpdateInlines(t, e.NewValue as InlineCollectionInfo));
        }

        public static void SetInlineInfo(DependencyObject obj, IList<string> value)
        {
            obj.SetValue(InlineInfoProperty, value);
        }

        private static void UpdateInlines(TextBlock textBlock, InlineCollectionInfo info)
        {
            if ((textBlock != null) && (info != null))
            {
                new InlineTextUpdater { 
                    TextBlock = textBlock,
                    UseInlines = info.HasStyle
                }.Update(info);
            }
        }
    }
}

