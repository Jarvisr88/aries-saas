namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;

    public class DateNavigatorOutlookStyleSettings : DateNavigatorStyleSettings
    {
        protected override INavigationService CreateNavigationService() => 
            (base.Navigator.OwnerDateEdit == null) ? (base.Navigator.IsMultiSelect ? ((INavigationService) new MultipleSelectionNavigationStrategy(base.Navigator, base.Navigator.AllowMultipleRanges)) : ((INavigationService) new SingleSelectionNavigationStrategy(base.Navigator))) : ((INavigationService) new DateEditNavigationStrategy(base.Navigator));

        protected internal override void Initialize(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            base.Initialize(navigator);
            navigator.DisplayMode = DateNavigatorDisplayMode.Outlook;
        }
    }
}

