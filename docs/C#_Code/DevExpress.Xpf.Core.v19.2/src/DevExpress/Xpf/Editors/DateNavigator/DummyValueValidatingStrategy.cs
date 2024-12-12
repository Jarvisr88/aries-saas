namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DummyValueValidatingStrategy : IValueValidatingService
    {
        public DummyValueValidatingStrategy(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            this.Navigator = navigator;
        }

        IList<DateTime> IValueValidatingService.Validate(IList<DateTime> selectedDates) => 
            selectedDates;

        private DevExpress.Xpf.Editors.DateNavigator.DateNavigator Navigator { get; set; }
    }
}

