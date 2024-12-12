namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class OnLoadedScheduler
    {
        private Action action;

        public static bool IsReallyLoaded(FrameworkElement element) => 
            element.IsLoaded;

        public void Schedule(Action action, FrameworkElement target)
        {
            Guard.ArgumentNotNull(action, "action");
            Guard.ArgumentNotNull(target, "target");
            this.action = action;
            if (IsReallyLoaded(target))
            {
                action();
            }
            else
            {
                target.Loaded += new RoutedEventHandler(this.target_Loaded);
            }
        }

        private void target_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement) sender;
            element.Loaded -= new RoutedEventHandler(this.target_Loaded);
            if (element.IsInVisualTree())
            {
                this.action();
            }
        }
    }
}

