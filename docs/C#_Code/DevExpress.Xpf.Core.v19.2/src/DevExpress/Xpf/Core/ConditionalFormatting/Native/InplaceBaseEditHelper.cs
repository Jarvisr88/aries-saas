namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;

    public static class InplaceBaseEditHelper
    {
        public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.RegisterAttached("TextDecorations", typeof(TextDecorationCollection), typeof(InplaceBaseEditHelper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(InplaceBaseEditHelper.OnTextDecorationsChanged)));

        public static TextDecorationCollection GetTextDecorations(DependencyObject obj) => 
            (TextDecorationCollection) obj.GetValue(TextDecorationsProperty);

        private static void OnTextDecorationsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateTextDecorationsCore(d, (TextDecorationCollection) e.NewValue);
        }

        public static void SetTextDecorations(DependencyObject obj, TextDecorationCollection value)
        {
            obj.SetValue(TextDecorationsProperty, value);
        }

        public static void UpdateTextDecorations(DependencyObject d)
        {
            UpdateTextDecorationsCore(d, GetTextDecorations(d));
        }

        private static void UpdateTextDecorationsCore(DependencyObject d, TextDecorationCollection newValue)
        {
            if ((newValue != null) && ((newValue.Count == 0) && ((d is IInplaceBaseEdit) && !((IInplaceBaseEdit) d).HasTextDecorations)))
            {
                newValue = null;
            }
            BaseEditHelper.SetTextDecorations(d, newValue);
        }
    }
}

