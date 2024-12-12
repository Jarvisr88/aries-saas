namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class AutoHideResizePointer : psvControl
    {
        public static readonly DependencyProperty DockProperty;

        static AutoHideResizePointer()
        {
            DependencyPropertyRegistrator<AutoHideResizePointer> registrator = new DependencyPropertyRegistrator<AutoHideResizePointer>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<System.Windows.Controls.Dock>("Dock", ref DockProperty, System.Windows.Controls.Dock.Left, null, null);
        }

        public System.Windows.Controls.Dock Dock
        {
            get => 
                (System.Windows.Controls.Dock) base.GetValue(DockProperty);
            set => 
                base.SetValue(DockProperty, value);
        }
    }
}

