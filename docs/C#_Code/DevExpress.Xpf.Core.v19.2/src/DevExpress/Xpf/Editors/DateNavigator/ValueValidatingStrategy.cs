namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ValueValidatingStrategy : IValueValidatingService
    {
        public ValueValidatingStrategy(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            this.Navigator = navigator;
        }

        IList<DateTime> IValueValidatingService.Validate(IList<DateTime> selectedDates) => 
            (this.Navigator.MaxSelectionLength >= 0) ? new ObservableCollection<DateTime>(selectedDates.Take<DateTime>(this.Navigator.MaxSelectionLength)) : selectedDates;

        private DevExpress.Xpf.Editors.DateNavigator.DateNavigator Navigator { get; set; }
    }
}

