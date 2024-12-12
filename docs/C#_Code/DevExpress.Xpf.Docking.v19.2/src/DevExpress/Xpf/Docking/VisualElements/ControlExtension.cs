namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public static class ControlExtension
    {
        public static void AttachToolTip(this FrameworkElement control, object tooltip)
        {
            control.ToolTip = tooltip;
        }

        public static void Forward(this FrameworkElement control, FrameworkElement target, DependencyProperty targetProperty, string property, BindingMode mode = 1)
        {
            Binding binding = new Binding(property);
            binding.Source = control;
            binding.Mode = mode;
            target.SetBinding(targetProperty, binding);
        }

        public static void Forward(this FrameworkElement control, FrameworkElement target, DependencyProperty targetProperty, DependencyProperty property, BindingMode mode = 1)
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(property);
            binding.Source = control;
            binding.Mode = mode;
            target.SetBinding(targetProperty, binding);
        }

        public static void InvalidateVisual(this UIElement control)
        {
            if (control != null)
            {
                control.InvalidateVisual();
            }
        }

        public static void StartListen(this FrameworkElement control, DependencyProperty property, string target, BindingMode mode = 1)
        {
            Binding binding = new Binding(target);
            binding.Source = control;
            binding.Mode = mode;
            control.SetBinding(property, binding);
        }
    }
}

