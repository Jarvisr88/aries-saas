namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DragIconControl : Control
    {
        public static readonly DependencyProperty DragTypeProperty;

        static DragIconControl()
        {
            Type ownerType = typeof(DragIconControl);
            DragTypeProperty = DependencyProperty.Register("DragType", typeof(DevExpress.Xpf.Core.DragType), ownerType, new PropertyMetadata(DevExpress.Xpf.Core.DragType.Move));
        }

        public DragIconControl()
        {
            base.DefaultStyleKey = typeof(DragIconControl);
        }

        public DevExpress.Xpf.Core.DragType DragType
        {
            get => 
                (DevExpress.Xpf.Core.DragType) base.GetValue(DragTypeProperty);
            set => 
                base.SetValue(DragTypeProperty, value);
        }
    }
}

