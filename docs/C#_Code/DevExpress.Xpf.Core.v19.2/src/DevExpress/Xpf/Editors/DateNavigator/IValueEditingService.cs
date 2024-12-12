namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public interface IValueEditingService
    {
        void SetFocusedDate(DateTime date);
        void SetSelectedDates(ObservableCollection<DateTime> selectedDates, bool clearSelection);

        DateTime StartDate { get; }

        DateTime EndDate { get; }

        DateTime FocusedDate { get; }

        IList<DateTime> SelectedDates { get; }
    }
}

