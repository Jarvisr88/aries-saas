namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using System;

    public class DateNavigatorStyleSettings : DateNavigatorStyleSettingsBase
    {
        protected override INavigationService CreateNavigationService() => 
            base.Navigator.IsMultiSelect ? ((INavigationService) new MultipleSelectionNavigationStrategy(base.Navigator, base.Navigator.AllowMultipleRanges)) : ((INavigationService) new SingleSelectionNavigationStrategy(base.Navigator));

        protected internal override void Initialize(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            base.Initialize(navigator);
            navigator.DisplayMode = DateNavigatorDisplayMode.Classic;
        }

        protected override void RegisterDefaultServices()
        {
            base.RegisterDefaultServices();
            base.RegisterDefaultService(typeof(INavigationCallbackService), new DummyNavigationCallbackService());
            base.RegisterDefaultService(typeof(IOptionsProviderService), new DateNavigatorOptionsProviderService(base.Navigator));
        }
    }
}

