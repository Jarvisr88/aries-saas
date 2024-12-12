namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;

    public class DashboardNavigationStrategy : SingleSelectionNavigationStrategy
    {
        public DashboardNavigationStrategy(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator) : base(navigator)
        {
        }

        public override void ToView(DateNavigatorCalendarView navigationState)
        {
            if (navigationState < this.MinimumView)
            {
                navigationState = this.MinimumView;
            }
            base.ToView(navigationState);
        }

        private DateNavigatorCalendarView MinimumView =>
            ((DateNavigatorDashboardStyleSettings) base.Navigator.StyleSettings).MinimumView;
    }
}

