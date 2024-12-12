namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public static class RootVisualHelper
    {
        private static UIElement rootVisual;

        public static event EventHandler OnClick;

        private static void KeyDownHandler(object sender, KeyEventArgs e)
        {
            RaiseClickEvent(sender, e);
        }

        private static void MouseLeftButtonDownHandler(object sender, EventArgs e)
        {
            RaiseClickEvent(sender, e);
        }

        private static void OnRootVisualActivated(object sender, EventArgs e)
        {
            RaiseClickEvent(sender, e);
        }

        private static void OnRootVisualDeactivated(object sender, EventArgs e)
        {
            RaiseClickEvent(sender, e);
        }

        private static void OnRootVisualLocationChanged(object sender, EventArgs e)
        {
            RaiseClickEvent(sender, e);
        }

        private static void OnRootVisualSizeChanged(object sender, EventArgs e)
        {
            RaiseClickEvent(sender, e);
        }

        private static void OnRootVisualStateChanged(object sender, EventArgs e)
        {
            RaiseClickEvent(sender, e);
        }

        private static void RaiseClickEvent(object sender, EventArgs e)
        {
            if (OnClick != null)
            {
                OnClick(sender, e);
            }
        }

        public static void SubscribeOnClick()
        {
            UnsubscribeOnClick();
            SubscribeOnClickCore();
        }

        private static void SubscribeOnClickCore()
        {
            if (RootVisual != null)
            {
                if (rootVisual is Window)
                {
                    ((Window) RootVisual).Activated += new EventHandler(RootVisualHelper.OnRootVisualActivated);
                    ((Window) RootVisual).Deactivated += new EventHandler(RootVisualHelper.OnRootVisualDeactivated);
                    ((Window) RootVisual).LocationChanged += new EventHandler(RootVisualHelper.OnRootVisualLocationChanged);
                    ((Window) RootVisual).SizeChanged += new SizeChangedEventHandler(RootVisualHelper.OnRootVisualSizeChanged);
                    ((Window) RootVisual).StateChanged += new EventHandler(RootVisualHelper.OnRootVisualStateChanged);
                }
                RootVisual.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(RootVisualHelper.MouseLeftButtonDownHandler), true);
                RootVisual.AddHandler(UIElement.PreviewKeyDownEvent, new KeyEventHandler(RootVisualHelper.KeyDownHandler), true);
            }
        }

        public static void UnsubscribeOnClick()
        {
            if (RootVisual != null)
            {
                if (rootVisual is Window)
                {
                    ((Window) RootVisual).Activated -= new EventHandler(RootVisualHelper.OnRootVisualActivated);
                    ((Window) RootVisual).Deactivated -= new EventHandler(RootVisualHelper.OnRootVisualDeactivated);
                    ((Window) RootVisual).LocationChanged -= new EventHandler(RootVisualHelper.OnRootVisualLocationChanged);
                    ((Window) RootVisual).SizeChanged -= new SizeChangedEventHandler(RootVisualHelper.OnRootVisualSizeChanged);
                    ((Window) RootVisual).StateChanged -= new EventHandler(RootVisualHelper.OnRootVisualStateChanged);
                }
                RootVisual.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(RootVisualHelper.MouseLeftButtonDownHandler));
                RootVisual.RemoveHandler(UIElement.PreviewKeyDownEvent, new KeyEventHandler(RootVisualHelper.KeyDownHandler));
            }
        }

        public static void UpdateRootVisual(DependencyObject dependencyObject)
        {
            rootVisual = Window.GetWindow(dependencyObject);
            rootVisual ??= (LayoutHelper.FindRoot(dependencyObject, false) as UIElement);
        }

        public static UIElement RootVisual
        {
            get => 
                rootVisual;
            private set => 
                rootVisual = value;
        }
    }
}

