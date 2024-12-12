namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class BrushTypeSelectorControl : Control
    {
        public static readonly DependencyProperty BrushTypeProperty;

        static BrushTypeSelectorControl()
        {
            Type ownerType = typeof(BrushTypeSelectorControl);
            BrushTypeProperty = DependencyPropertyManager.Register("BrushType", typeof(DevExpress.Xpf.Editors.BrushType), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.BrushType.None));
        }

        public BrushTypeSelectorControl()
        {
            this.SetDefaultStyleKey(typeof(BrushTypeSelectorControl));
        }

        public DevExpress.Xpf.Editors.BrushType BrushType
        {
            get => 
                (DevExpress.Xpf.Editors.BrushType) base.GetValue(BrushTypeProperty);
            set => 
                base.SetValue(BrushTypeProperty, value);
        }
    }
}

