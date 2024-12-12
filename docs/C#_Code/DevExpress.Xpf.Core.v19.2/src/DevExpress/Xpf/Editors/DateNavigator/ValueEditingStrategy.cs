namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.DateNavigator.Controls;
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ValueEditingStrategy : IValueEditingService
    {
        public ValueEditingStrategy(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            this.Navigator = navigator;
            this.PropertyChangedLocker = new Locker();
            this.InternalSyncLocker = new Locker();
        }

        public virtual void DateTimeChanged(DateTime? oldValue, DateTime? newValue)
        {
            this.PropertyChangedLocker.DoLockedActionIfNotLocked(delegate {
                ObservableCollection<DateTime> collection2;
                if (newValue == null)
                {
                    collection2 = null;
                }
                else
                {
                    ObservableCollection<DateTime> collection1 = new ObservableCollection<DateTime>();
                    collection1.Add(newValue.Value);
                    collection2 = collection1;
                }
                this.Instance.SetSelectedDates(collection2, true);
                if (newValue != null)
                {
                    this.Instance.SetFocusedDate(newValue.Value);
                }
            });
        }

        void IValueEditingService.SetFocusedDate(DateTime date)
        {
            if (!SelectedDatesHelper.AreEquals(this.Navigator.FocusedDate, date))
            {
                this.Navigator.FocusedDate = date;
            }
        }

        void IValueEditingService.SetSelectedDates(ObservableCollection<DateTime> selectedDates, bool clearSelection)
        {
            ObservableCollection<DateTime> observables = clearSelection ? selectedDates : SelectedDatesHelper.Merge(this.Navigator.SelectedDates, selectedDates);
            if (!SelectedDatesHelper.AreEquals(this.Navigator.SelectedDates, observables))
            {
                this.Navigator.SetSelectedDates(observables);
            }
        }

        public virtual void FocusedDateChanged(DateTime oldValue, DateTime newValue)
        {
        }

        public virtual DateNavigatorCalendarCellButton GetCalendarCellButton(DateTime dt) => 
            DateNavigatorHelper.GetCalendarCellButton(this.Navigator, dt);

        public virtual DateNavigatorCalendarCellButton GetCalendarCellButton(DateNavigatorCalendarView view, DateTime dt)
        {
            DateNavigatorContent content = ((IDateNavigatorContentContainer) this.Navigator).GetContent(view);
            if (content == null)
            {
                return null;
            }
            return ((IDateNavigatorContent) content).GetCalendar(dt)?.GetCellButton(dt);
        }

        public virtual void HolidaysChanged()
        {
            if (this.ActiveContent != null)
            {
                this.ActiveContent.UpdateCalendarsHolidays();
            }
        }

        public virtual bool IsWorkday(DateTime date) => 
            true;

        public void ResetFocusedCellButtonFocusedState(DateNavigatorCalendarView view)
        {
            DateNavigatorCalendarCellButton calendarCellButton = this.GetCalendarCellButton(view, this.Instance.FocusedDate);
            if (calendarCellButton != null)
            {
                DateNavigatorCalendar.SetCellFocused(calendarCellButton, false);
            }
        }

        public virtual void ResetFocusedCellButtonMouseOverState()
        {
            DateNavigatorCalendarCellButton calendarCellButton = this.GetCalendarCellButton(this.Instance.FocusedDate);
            if (calendarCellButton != null)
            {
                VisualStateManager.GoToState(calendarCellButton, "Normal", true);
            }
        }

        public virtual void SelectedDatesChanged(IList<DateTime> selectedDates)
        {
            this.UpdateSelectedState();
        }

        public virtual void SetFocusedCellButtonFocusedState()
        {
            DateNavigatorCalendarCellButton calendarCellButton = this.GetCalendarCellButton(this.Instance.FocusedDate);
            if (calendarCellButton != null)
            {
                DateNavigatorCalendar.SetCellFocused(calendarCellButton, true);
            }
        }

        public virtual void SetFocusedCellButtonMouseOverState()
        {
            DateNavigatorCalendarCellButton calendarCellButton = this.GetCalendarCellButton(this.Instance.FocusedDate);
            if (calendarCellButton != null)
            {
                VisualStateManager.GoToState(calendarCellButton, "MouseOver", true);
            }
        }

        public virtual void UpdateSelectedState()
        {
            if (this.ActiveContent != null)
            {
                this.ActiveContent.UpdateCalendarsSelectedDates();
            }
        }

        public virtual void UpdateSelection()
        {
        }

        private DevExpress.Xpf.Editors.DateNavigator.SelectionManager SelectionManager =>
            this.Navigator.SelectionManager;

        private Locker PropertyChangedLocker { get; set; }

        private Locker InternalSyncLocker { get; set; }

        protected DevExpress.Xpf.Editors.DateNavigator.DateNavigator Navigator { get; private set; }

        protected IDateNavigatorContent ActiveContent =>
            this.Navigator.ActiveContent;

        private IValueEditingService Instance =>
            this;

        private IValueValidatingService ValueValidating =>
            this.Navigator.ValueValidatingService;

        DateTime IValueEditingService.StartDate =>
            (this.ActiveContent != null) ? this.ActiveContent.StartDate : this.Instance.FocusedDate;

        DateTime IValueEditingService.EndDate =>
            (this.ActiveContent != null) ? this.ActiveContent.EndDate : this.Instance.FocusedDate;

        DateTime IValueEditingService.FocusedDate =>
            this.SelectionManager.FocusedDate;

        IList<DateTime> IValueEditingService.SelectedDates =>
            this.SelectionManager.SelectedDates;
    }
}

