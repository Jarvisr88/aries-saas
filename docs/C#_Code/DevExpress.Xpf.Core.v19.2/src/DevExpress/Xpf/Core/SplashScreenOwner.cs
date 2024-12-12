namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SplashScreenOwner
    {
        public static readonly DependencyProperty PreferVisualTreeForOwnerSearchProperty = DependencyProperty.RegisterAttached("PreferVisualTreeForOwnerSearch", typeof(bool), typeof(SplashScreenOwner), new PropertyMetadata(false));

        public SplashScreenOwner(DependencyObject owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("Owner");
            }
            this.Owner = owner;
        }

        internal WindowContainer CreateOwnerContainer(WindowStartupLocation splashScreenStartupLocation)
        {
            WindowContainer container = null;
            if (splashScreenStartupLocation == WindowStartupLocation.CenterOwner)
            {
                WindowArrangerContainer container1 = new WindowArrangerContainer(this.Owner, SplashScreenLocation.CenterWindow);
                container1.ArrangeMode = SplashScreenArrangeMode.ArrangeOnStartupOnly;
                container = container1;
            }
            if (splashScreenStartupLocation == WindowStartupLocation.CenterScreen)
            {
                WindowArrangerContainer container2 = new WindowArrangerContainer(this.Owner, SplashScreenLocation.CenterScreen);
                container2.ArrangeMode = SplashScreenArrangeMode.ArrangeOnStartupOnly;
                container = container2;
            }
            if ((container == null) || !container.IsInitialized)
            {
                container = new WindowContainer(this.Owner);
            }
            return container;
        }

        public static bool GetPreferVisualTreeForOwnerSearch(DependencyObject obj) => 
            (bool) obj.GetValue(PreferVisualTreeForOwnerSearchProperty);

        public static void SetPreferVisualTreeForOwnerSearch(DependencyObject obj, bool value)
        {
            obj.SetValue(PreferVisualTreeForOwnerSearchProperty, value);
        }

        public DependencyObject Owner { get; private set; }
    }
}

