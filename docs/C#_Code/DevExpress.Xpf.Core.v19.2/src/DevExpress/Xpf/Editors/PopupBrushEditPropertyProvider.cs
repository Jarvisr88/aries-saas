namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class PopupBrushEditPropertyProvider : PopupBaseEditPropertyProvider
    {
        public static readonly DependencyProperty BrushProperty;

        static PopupBrushEditPropertyProvider()
        {
            Type ownerType = typeof(PopupBrushEditPropertyProvider);
            BrushProperty = DependencyPropertyManager.Register("Brush", typeof(System.Windows.Media.Brush), ownerType);
        }

        public PopupBrushEditPropertyProvider(PopupBrushEditBase editor) : base(editor)
        {
        }

        public System.Windows.Media.Brush Brush
        {
            get => 
                (System.Windows.Media.Brush) base.GetValue(BrushProperty);
            set => 
                base.SetValue(BrushProperty, value);
        }
    }
}

