namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DependencyObjectWrapper
    {
        private bool IsFrameworkElement;
        public FrameworkElement FE;
        public FrameworkContentElement FCE;

        public DependencyObjectWrapper(DependencyObject dObj)
        {
            this.FE = dObj as FrameworkElement;
            this.IsFrameworkElement = this.FE != null;
            if (!this.IsFrameworkElement)
            {
                this.FCE = dObj as FrameworkContentElement;
            }
        }

        public static void AddHandler(DependencyObject obj, RoutedEvent routedEvent, Delegate handler)
        {
            FrameworkElement element = obj as FrameworkElement;
            if (element != null)
            {
                element.AddHandler(routedEvent, handler);
            }
            else
            {
                (obj as FrameworkContentElement).AddHandler(routedEvent, handler);
            }
        }

        public void Clear()
        {
            this.FE = null;
            this.FCE = null;
        }

        public object GetDefaultStyleKey() => 
            !this.IsFrameworkElement ? this.FCE.GetDefaultStyleKey() : this.FE.GetDefaultStyleKey();

        public void Initialize()
        {
        }

        public void RaiseEvent(RoutedEventArgs e)
        {
            if (this.IsFrameworkElement)
            {
                this.FE.RaiseEvent(e);
            }
            else
            {
                this.FCE.RaiseEvent(e);
            }
        }

        public static void RemoveHandler(DependencyObject obj, RoutedEvent routedEvent, Delegate handler)
        {
            FrameworkElement element = obj as FrameworkElement;
            if (element != null)
            {
                element.RemoveHandler(routedEvent, handler);
            }
            else
            {
                (obj as FrameworkContentElement).RemoveHandler(routedEvent, handler);
            }
        }

        public void SetDefaultStyleKey(object value)
        {
            if (this.IsFrameworkElement)
            {
                this.FE.SetDefaultStyleKey(value);
            }
            else
            {
                this.FCE.SetDefaultStyleKey(value);
            }
        }

        public bool OverridesDefaultStyleKey =>
            !this.IsFrameworkElement ? this.FCE.OverridesDefaultStyle : this.FE.OverridesDefaultStyle;

        private string ThemeName { get; set; }
    }
}

