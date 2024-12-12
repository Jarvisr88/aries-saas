namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class PanelWrapper : IPanelWrapper<Panel>, ITargetWrapper<Panel>
    {
        public void AddChild(UIElement child)
        {
            this.Target.Children.Add(child);
        }

        public void RemoveChild(UIElement child)
        {
            this.Target.Children.Remove(child);
        }

        public Panel Target { get; set; }

        public IEnumerable Children =>
            this.Target.Children;
    }
}

