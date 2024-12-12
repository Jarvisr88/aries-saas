namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Windows;

    public static class VisualHelper
    {
        public static readonly DependencyProperty OffsetProperty;
        public static readonly DependencyProperty ClipToBoundsProperty;
        public static readonly DependencyProperty IsVisualBrickBorderProperty;

        static VisualHelper();
        public static bool GetClipToBounds(DependencyObject obj);
        public static bool GetIsVisualBrickBorder(DependencyObject obj);
        public static Point GetOffset(DependencyObject obj);
        private static void OnClipToBoundsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetClipToBounds(DependencyObject obj, bool value);
        public static void SetIsVisualBrickBorder(DependencyObject obj, bool value);
        public static void SetOffset(DependencyObject obj, Point value);
    }
}

