namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class DateNavigatorDashboardStyleSettings : DateNavigatorStyleSettingsBase
    {
        public static readonly DependencyProperty MinimumViewProperty;

        static DateNavigatorDashboardStyleSettings()
        {
            Type ownerType = typeof(DateNavigatorDashboardStyleSettings);
            MinimumViewProperty = DependencyProperty.Register("MinimumView", typeof(DateNavigatorCalendarView), ownerType, new PropertyMetadata(DateNavigatorCalendarView.Month, (o, args) => ((DateNavigatorDashboardStyleSettings) o).MinimumViewChanged((DateNavigatorCalendarView) args.NewValue)));
        }

        protected override INavigationService CreateNavigationService() => 
            new DashboardNavigationStrategy(base.Navigator);

        protected internal override void Initialize(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            base.Initialize(navigator);
            navigator.NavigationService.ToView(this.MinimumView);
        }

        protected virtual void MinimumViewChanged(DateNavigatorCalendarView newValue)
        {
            if ((base.Navigator != null) && (base.Navigator.NavigationService != null))
            {
                base.Navigator.NavigationService.ToView(this.MinimumView);
            }
        }

        protected override void RegisterDefaultServices()
        {
            base.RegisterDefaultService(typeof(INavigationCallbackService), new DummyNavigationCallbackService());
            base.RegisterDefaultService(typeof(IOptionsProviderService), new DateNavigatorOptionsProviderService(base.Navigator));
            base.RegisterDefaultServices();
        }

        public DateNavigatorCalendarView MinimumView
        {
            get => 
                (DateNavigatorCalendarView) base.GetValue(MinimumViewProperty);
            set => 
                base.SetValue(MinimumViewProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateNavigatorDashboardStyleSettings.<>c <>9 = new DateNavigatorDashboardStyleSettings.<>c();

            internal void <.cctor>b__1_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((DateNavigatorDashboardStyleSettings) o).MinimumViewChanged((DateNavigatorCalendarView) args.NewValue);
            }
        }
    }
}

