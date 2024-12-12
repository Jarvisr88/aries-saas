namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.DateNavigator.Controls;
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class NavigationStrategyBase : INavigationService
    {
        protected NavigationStrategyBase(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            this.Navigator = navigator;
            this.ViewSpecific = new MonthNavigationLogic();
        }

        public virtual void BringToView()
        {
            this.ScrollTo(this.SelectionManager.FocusedDate, false);
        }

        public virtual void CheckSelectedDates()
        {
        }

        private DateNavigatorCalendarView GetView(DateNavigatorCalendarView view, int offset)
        {
            int num = ((int) view) + offset;
            if (num < 0)
            {
                return DateNavigatorCalendarView.Month;
            }
            int num2 = EnumHelper.GetEnumCount(typeof(DateNavigatorCalendarView)) - 1;
            return ((num <= num2) ? ((DateNavigatorCalendarView) num) : ((DateNavigatorCalendarView) num2));
        }

        private bool MakeDirectionalMove(Func<DateTime, DateTime> moveHandler)
        {
            DateTime focusedDate = this.SelectionManager.FocusedDate;
            while (true)
            {
                DateTime arg = focusedDate;
                focusedDate = moveHandler(arg);
                if (!focusedDate.Equals(arg))
                {
                    if ((this.Navigator.MinValue != null) && (this.Navigator.MinValue.Value.CompareTo(focusedDate) >= 0))
                    {
                        return this.Move(this.Navigator.MinValue.Value);
                    }
                    if (this.Navigator.MaxValue != null)
                    {
                        DateTime? maxValue = this.Navigator.MaxValue;
                        if (maxValue.Value.CompareTo(focusedDate) <= 0)
                        {
                            return this.Move(this.Navigator.MaxValue.Value);
                        }
                    }
                    if (this.DateCalculationService.IsDisabled(focusedDate))
                    {
                        continue;
                    }
                }
                return this.Move(focusedDate);
            }
        }

        public virtual bool Move(DateTime dateTime)
        {
            this.SelectionManager.SetFocusedDate(dateTime, false);
            this.BringToView();
            return true;
        }

        public virtual bool MoveDown()
        {
            ViewSpecificNavigationLogic viewSpecific = this.ViewSpecific;
            return this.MakeDirectionalMove(new Func<DateTime, DateTime>(viewSpecific.MoveDown));
        }

        public virtual bool MoveLeft()
        {
            ViewSpecificNavigationLogic viewSpecific = this.ViewSpecific;
            return this.MakeDirectionalMove(new Func<DateTime, DateTime>(viewSpecific.MoveLeft));
        }

        protected bool MoveLeftRight(bool isLeft)
        {
            if (this.Navigator.FlowDirection == FlowDirection.RightToLeft)
            {
                isLeft = !isLeft;
            }
            return (isLeft ? this.MoveLeft() : this.MoveRight());
        }

        public virtual bool MoveRight()
        {
            ViewSpecificNavigationLogic viewSpecific = this.ViewSpecific;
            return this.MakeDirectionalMove(new Func<DateTime, DateTime>(viewSpecific.MoveRight));
        }

        public virtual bool MoveUp()
        {
            ViewSpecificNavigationLogic viewSpecific = this.ViewSpecific;
            return this.MakeDirectionalMove(new Func<DateTime, DateTime>(viewSpecific.MoveUp));
        }

        public virtual void ProcessKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
                {
                    this.ToView(this.GetView(this.Navigator.CalendarView, 1));
                }
                else
                {
                    this.ToView(this.GetView(this.Navigator.CalendarView, -1));
                }
            }
        }

        public virtual void ProcessKeyUp(KeyEventArgs e)
        {
        }

        public virtual void ProcessMouseDown(DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            if (this.Navigator.CaptureMouse())
            {
                this.MouseDownDate = new DateTime?(buttonDate);
                this.MouseDownDateButtonKind = buttonKind;
                if (this.Navigator.CalendarView != DateNavigatorCalendarView.Month)
                {
                    this.Navigator.Dispatcher.BeginInvoke(delegate {
                        VisualStateManager.GoToState(DateNavigatorHelper.GetCalendarCellButton(this.Navigator, buttonDate), "Normal", true);
                        this.SelectionManager.SetFocusedDate(buttonDate, true);
                    }, null);
                }
                if ((this.Navigator.CalendarView == DateNavigatorCalendarView.Month) && (buttonKind == DateNavigatorCalendarButtonKind.Date))
                {
                    this.Navigator.Dispatcher.BeginInvoke(() => VisualStateManager.GoToState(DateNavigatorHelper.GetCalendarCellButton(this.Navigator, buttonDate), "Normal", true), null);
                }
            }
        }

        public virtual void ProcessMouseMove(DateTime? buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            if ((this.Navigator.CalendarView != DateNavigatorCalendarView.Month) && (this.MouseDownDate != null))
            {
                if (buttonDate != null)
                {
                    this.SelectionManager.SetFocusedDate(buttonDate.Value, false);
                }
                else
                {
                    DateNavigatorCalendarCellButton calendarCellButton = DateNavigatorHelper.GetCalendarCellButton(this.Navigator, this.Navigator.ActiveContent.FocusedDate);
                    if (calendarCellButton != null)
                    {
                        VisualStateManager.GoToState(calendarCellButton, "Normal", true);
                    }
                }
            }
        }

        public void ProcessMouseUp(DateTime? buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            if (this.MouseDownDate != null)
            {
                try
                {
                    this.ProcessMouseUpCore(buttonDate);
                }
                finally
                {
                    this.MouseDownDate = null;
                }
            }
        }

        protected virtual void ProcessMouseUpCore(DateTime? date)
        {
            if (date != null)
            {
                this.Move(date.Value);
                this.ToView(this.GetView(this.Navigator.CalendarView, -1));
            }
        }

        public virtual bool ScrollNext() => 
            this.ScrollTo(this.ViewSpecific.GetPage(this.ActiveContent.EndDate, 1), false);

        public virtual bool ScrollNextPage()
        {
            if (!this.ScrollSelection)
            {
                return this.ScrollTo(this.ViewSpecific.NextPage(this.ActiveContent.EndDate, this.ActiveContent.CalendarCount), false);
            }
            DateTime time = this.Navigator.SelectedDates[0];
            this.NavigationCallback.Scroll((TimeSpan) (this.Navigator.SelectedDates[0].AddMonths(1) - this.Navigator.SelectedDates[0]));
            bool flag = (this.ActiveContent.GetCalendar(this.Navigator.SelectedDates[this.Navigator.SelectedDates.Count - 1], true) != null) ? this.Navigator.SetActiveContentDateTime(this.Navigator.SelectedDates[0], false) : this.Navigator.SetActiveContentDateTime(this.Navigator.SelectedDates[0].AddMonths(this.ActiveContent.CalendarCount - 1), false);
            this.Navigator.FocusedDate = this.Navigator.FocusedDate.AddDays((double) (this.Navigator.SelectedDates[0] - time).Days);
            return flag;
        }

        public virtual bool ScrollPrevious() => 
            this.ScrollTo(this.ViewSpecific.GetPage(this.ActiveContent.StartDate, -1), false);

        public virtual bool ScrollPreviousPage()
        {
            if (!this.ScrollSelection)
            {
                return this.ScrollTo(this.ViewSpecific.PreviousPage(this.ActiveContent.StartDate, this.ActiveContent.CalendarCount), false);
            }
            DateTime time = this.Navigator.SelectedDates[0];
            this.NavigationCallback.Scroll((TimeSpan) (this.Navigator.SelectedDates[0].AddMonths(-1) - this.Navigator.SelectedDates[0]));
            bool flag = this.Navigator.SetActiveContentDateTime(this.Navigator.SelectedDates[0], true);
            this.Navigator.FocusedDate = this.Navigator.FocusedDate.AddDays((double) (this.Navigator.SelectedDates[0] - time).Days);
            return flag;
        }

        public virtual bool ScrollTo(DateTime dateTime, bool scrollIfValueInactive)
        {
            bool flag = this.Navigator.SetActiveContentDateTime(dateTime, scrollIfValueInactive);
            if (flag)
            {
                TimeSpan offset = new TimeSpan();
                this.NavigationCallback.Scroll(offset);
            }
            return flag;
        }

        public virtual bool Select(IList<DateTime> selectedDates, bool clearSelection)
        {
            if (selectedDates.All<DateTime>(x => this.DateCalculationService.IsDisabled(x)))
            {
                return false;
            }
            this.SelectionManager.SetSelection(selectedDates, clearSelection);
            return true;
        }

        public virtual bool Select(DateTime dateTime, bool clearSelection)
        {
            ObservableCollection<DateTime> selectedDates = new ObservableCollection<DateTime>();
            selectedDates.Add(dateTime);
            return this.Select(selectedDates, clearSelection);
        }

        public virtual bool SelectDown(bool clearSelection) => 
            true;

        public virtual bool SelectLeft(bool clearSelection) => 
            true;

        protected bool SelectLeftRight(bool clearSelection, bool isLeft)
        {
            if (this.Navigator.FlowDirection == FlowDirection.RightToLeft)
            {
                isLeft = !isLeft;
            }
            return (isLeft ? this.SelectLeft(clearSelection) : this.SelectRight(clearSelection));
        }

        public virtual bool SelectRight(bool clearSelection) => 
            true;

        public virtual bool SelectUp(bool clearSelection) => 
            true;

        public virtual void ToView(DateNavigatorCalendarView navigationState)
        {
            if (navigationState == DateNavigatorCalendarView.Month)
            {
                this.ViewSpecific = new MonthNavigationLogic();
            }
            if (navigationState == DateNavigatorCalendarView.Year)
            {
                this.ViewSpecific = new YearNavigationLogic();
            }
            if (navigationState == DateNavigatorCalendarView.Years)
            {
                this.ViewSpecific = new YearsNavigationLogic();
            }
            if (navigationState == DateNavigatorCalendarView.YearsRange)
            {
                this.ViewSpecific = new YearsGroupNavigationLogic();
            }
            this.Navigator.CalendarView = navigationState;
        }

        public virtual bool Unselect(DateTime dateTime, bool clearSelection) => 
            true;

        protected DevExpress.Xpf.Editors.DateNavigator.SelectionManager SelectionManager =>
            this.Navigator.SelectionManager;

        protected DevExpress.Xpf.Editors.DateNavigator.DateNavigator Navigator { get; private set; }

        private INavigationCallbackService NavigationCallback =>
            this.Navigator.NavigationCallbackService;

        protected IOptionsProviderService OptionsProvider =>
            this.Navigator.OptionsProviderService;

        protected bool ScrollSelection =>
            (this.OptionsProvider != null) && (this.OptionsProvider.ScrollSelection && ((this.Navigator.SelectedDates != null) && ((this.Navigator.SelectedDates.Count > 0) && ((this.Navigator.CalendarView == DateNavigatorCalendarView.Month) && (this.ActiveContent != null)))));

        protected ViewSpecificNavigationLogic ViewSpecific { get; private set; }

        protected IDateNavigatorContent ActiveContent =>
            this.Navigator.ActiveContent;

        protected DateTime? MouseDownDate { get; private set; }

        protected DateNavigatorCalendarButtonKind MouseDownDateButtonKind { get; private set; }

        protected IDateCalculationService DateCalculationService =>
            (IDateCalculationService) ((IServiceProvider) this.Navigator).GetService(typeof(IDateCalculationService));
    }
}

