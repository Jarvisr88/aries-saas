namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.DateNavigator.Controls;
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MultipleSelectionNavigationStrategy : NavigationStrategyBase
    {
        private readonly bool allowMultipleRanges;

        public MultipleSelectionNavigationStrategy(DevExpress.Xpf.Editors.DateNavigator.DateNavigator dateNavigator) : this(dateNavigator, true)
        {
        }

        public MultipleSelectionNavigationStrategy(DevExpress.Xpf.Editors.DateNavigator.DateNavigator dateNavigator, bool allowMultipleRanges) : base(dateNavigator)
        {
            this.allowMultipleRanges = allowMultipleRanges;
        }

        protected virtual void MonthCalendarMouseDown(DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            bool flag = !Keyboard2.IsShiftPressed && !Keyboard2.IsControlPressed;
            bool isShiftPressed = Keyboard2.IsShiftPressed;
            bool flag3 = Keyboard2.IsControlPressed && !Keyboard2.IsShiftPressed;
            if (flag)
            {
                base.SelectionManager.Snapshot();
                this.Move(buttonDate);
                base.SelectionManager.Flush();
                this.SelectRange(buttonDate, (buttonKind == DateNavigatorCalendarButtonKind.WeekNumber) ? 7 : 1, true);
            }
            else if (isShiftPressed)
            {
                this.Move(buttonDate);
                base.SelectionManager.Flush();
                this.SelectRange(buttonDate, (buttonKind == DateNavigatorCalendarButtonKind.WeekNumber) ? 7 : 1, !this.allowMultipleRanges);
            }
            else if (flag3)
            {
                base.SelectionManager.Snapshot();
                if (SelectedDatesHelper.Contains(base.SelectionManager.SelectedDates, buttonDate))
                {
                    this.Move(buttonDate);
                    this.Unselect(buttonDate, !this.allowMultipleRanges);
                    base.SelectionManager.Flush();
                }
                else
                {
                    this.Move(buttonDate);
                    base.SelectionManager.Flush();
                    this.Select(buttonDate, !this.allowMultipleRanges);
                }
            }
        }

        protected virtual bool PerformModifiersNavigation(KeyEventArgs e)
        {
            if (e.Handled)
            {
                return e.Handled;
            }
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
            bool clearSelection = (ModifierKeysHelper.IsShiftPressed(keyboardModifiers) || ModifierKeysHelper.IsCtrlPressed(keyboardModifiers)) ? !this.allowMultipleRanges : true;
            bool flag2 = ModifierKeysHelper.IsCtrlPressed(keyboardModifiers) && !ModifierKeysHelper.IsShiftPressed(keyboardModifiers);
            switch (e.Key)
            {
                case Key.Left:
                    return (flag2 ? base.MoveLeftRight(true) : base.SelectLeftRight(clearSelection, true));

                case Key.Up:
                    return (flag2 ? this.MoveUp() : this.SelectUp(clearSelection));

                case Key.Right:
                    return (flag2 ? base.MoveLeftRight(false) : base.SelectLeftRight(clearSelection, false));

                case Key.Down:
                    return (flag2 ? this.MoveDown() : this.SelectDown(clearSelection));
            }
            return false;
        }

        protected virtual bool PerformNoModifiersNavigation(KeyEventArgs e)
        {
            if (e.Handled)
            {
                return true;
            }
            bool flag = false;
            switch (e.Key)
            {
                case Key.Left:
                    if (base.MoveLeftRight(true))
                    {
                        flag = this.Select(base.SelectionManager.FocusedDate, true);
                    }
                    break;

                case Key.Up:
                    if (this.MoveUp())
                    {
                        flag = this.Select(base.SelectionManager.FocusedDate, true);
                    }
                    break;

                case Key.Right:
                    if (base.MoveLeftRight(false))
                    {
                        flag = this.Select(base.SelectionManager.FocusedDate, true);
                    }
                    break;

                case Key.Down:
                    if (this.MoveDown())
                    {
                        flag = this.Select(base.SelectionManager.FocusedDate, true);
                    }
                    break;

                default:
                    break;
            }
            return flag;
        }

        protected virtual bool PerformSpace(KeyEventArgs e)
        {
            if (!e.Handled)
            {
                bool clearSelection = !ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e));
                if (!SelectedDatesHelper.Contains(base.SelectionManager.SelectedDates, base.SelectionManager.FocusedDate))
                {
                    return this.Select(base.SelectionManager.FocusedDate, clearSelection);
                }
                this.Unselect(base.SelectionManager.FocusedDate, clearSelection);
                base.SelectionManager.Flush();
            }
            return true;
        }

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            base.ProcessKeyDown(e);
            if (!e.Handled)
            {
                if (KeyboardHelper.IsShiftKey(e.Key))
                {
                    base.SelectionManager.Snapshot();
                }
                else if (KeyboardHelper.IsControlKey(e.Key) && !Keyboard2.IsShiftPressed)
                {
                    base.SelectionManager.Snapshot();
                }
                else
                {
                    ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
                    if (ModifierKeysHelper.NoModifiers(keyboardModifiers))
                    {
                        e.Handled = this.PerformNoModifiersNavigation(e);
                    }
                    if (ModifierKeysHelper.IsCtrlPressed(keyboardModifiers) || ModifierKeysHelper.IsShiftPressed(keyboardModifiers))
                    {
                        e.Handled = this.PerformModifiersNavigation(e);
                    }
                }
            }
        }

        public override void ProcessKeyUp(KeyEventArgs e)
        {
            base.ProcessKeyUp(e);
            if (!e.Handled)
            {
                if (KeyboardHelper.IsShiftKey(e.Key))
                {
                    base.SelectionManager.Post();
                }
                if (KeyboardHelper.IsControlKey(e.Key) && !Keyboard2.IsShiftPressed)
                {
                    base.SelectionManager.Post();
                }
                if (e.Key == Key.Space)
                {
                    e.Handled = this.PerformSpace(e);
                }
            }
        }

        public override void ProcessMouseDown(DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            if (((buttonKind != DateNavigatorCalendarButtonKind.WeekNumber) || (!Keyboard2.IsControlPressed || Keyboard2.IsShiftPressed)) && DateNavigatorHelper.HasEnabledDatesInView(base.Navigator, buttonDate))
            {
                base.ProcessMouseDown(buttonDate, buttonKind);
                if (base.Navigator.CalendarView == DateNavigatorCalendarView.Month)
                {
                    this.MonthCalendarMouseDown(buttonDate, buttonKind);
                }
            }
        }

        public override void ProcessMouseMove(DateTime? buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            base.ProcessMouseMove(buttonDate, buttonKind);
            if (((base.Navigator.CalendarView == DateNavigatorCalendarView.Month) && (base.MouseDownDate != null)) && (!Keyboard2.IsControlPressed || Keyboard2.IsShiftPressed))
            {
                base.SelectionManager.Snapshot();
                if (buttonDate != null)
                {
                    this.Move(buttonDate.Value);
                    if (base.MouseDownDateButtonKind == DateNavigatorCalendarButtonKind.Date)
                    {
                        IDateCalculationService dateCalculationService = base.DateCalculationService;
                        this.Select(SelectedDatesHelper.GetSelectionWithoutDisabled(buttonDate.Value, base.SelectionManager.SelectionStart, new Func<DateTime, bool>(dateCalculationService.IsDisabled), base.Navigator.AllowMultipleRanges), !base.Navigator.AllowMultipleRanges);
                    }
                    else
                    {
                        DateTime weekFirstDateByDate = base.ActiveContent.GetWeekFirstDateByDate(buttonDate.Value);
                        DateTime? nullable = buttonDate;
                        DateTime? mouseDownDate = base.MouseDownDate;
                        DateTime startDate = (((nullable != null) & (mouseDownDate != null)) ? ((DateTime) (nullable.GetValueOrDefault() >= mouseDownDate.GetValueOrDefault())) : ((DateTime) false)) ? base.MouseDownDate.Value : weekFirstDateByDate;
                        mouseDownDate = buttonDate;
                        nullable = base.MouseDownDate;
                        this.SelectRange(startDate, 7 + ((((mouseDownDate != null) & (nullable != null)) ? (mouseDownDate.GetValueOrDefault() >= nullable.GetValueOrDefault()) : false) ? (weekFirstDateByDate - startDate).Days : (base.MouseDownDate.Value - startDate).Days), false);
                    }
                }
            }
        }

        protected override void ProcessMouseUpCore(DateTime? date)
        {
            if (base.Navigator.CalendarView != DateNavigatorCalendarView.Month)
            {
                base.ProcessMouseUpCore(date);
            }
            else if (!base.Navigator.IsKeyboardFocusWithin)
            {
                base.SelectionManager.Post();
            }
            else if (!Keyboard2.IsControlPressed)
            {
                if (!Keyboard2.IsShiftPressed)
                {
                    base.SelectionManager.Post();
                }
                else
                {
                    base.SelectionManager.Flush();
                }
            }
        }

        public override bool SelectDown(bool clearSelection)
        {
            if (!this.MoveDown())
            {
                return false;
            }
            IDateCalculationService dateCalculationService = base.DateCalculationService;
            bool flag = this.Select(SelectedDatesHelper.GetSelectionWithoutDisabled(base.SelectionManager.FocusedDate, base.SelectionManager.SelectionStart, new Func<DateTime, bool>(dateCalculationService.IsDisabled), base.Navigator.AllowMultipleRanges), clearSelection);
            base.SelectDown(clearSelection);
            return flag;
        }

        public override bool SelectLeft(bool clearSelection)
        {
            if (!this.MoveLeft())
            {
                return false;
            }
            IDateCalculationService dateCalculationService = base.DateCalculationService;
            bool flag = this.Select(SelectedDatesHelper.GetSelectionWithoutDisabled(base.SelectionManager.FocusedDate, base.SelectionManager.SelectionStart, new Func<DateTime, bool>(dateCalculationService.IsDisabled), base.Navigator.AllowMultipleRanges), clearSelection);
            base.SelectLeft(clearSelection);
            return flag;
        }

        protected bool SelectRange(DateTime startDate, int dayCount, bool clearSelection)
        {
            List<DateTime> selectedDates = new List<DateTime>();
            for (int i = 0; i < dayCount; i++)
            {
                if (base.DateCalculationService.IsDisabled(startDate.AddDays((double) i)))
                {
                    selectedDates.Clear();
                }
                else
                {
                    selectedDates.Add(startDate.AddDays((double) i));
                }
            }
            return this.Select(selectedDates, clearSelection);
        }

        public override bool SelectRight(bool clearSelection)
        {
            if (!this.MoveRight())
            {
                return false;
            }
            IDateCalculationService dateCalculationService = base.DateCalculationService;
            bool flag = this.Select(SelectedDatesHelper.GetSelectionWithoutDisabled(base.SelectionManager.FocusedDate, base.SelectionManager.SelectionStart, new Func<DateTime, bool>(dateCalculationService.IsDisabled), base.Navigator.AllowMultipleRanges), clearSelection);
            base.SelectRight(clearSelection);
            return flag;
        }

        public override bool SelectUp(bool clearSelection)
        {
            if (!this.MoveUp())
            {
                return false;
            }
            IDateCalculationService dateCalculationService = base.DateCalculationService;
            bool flag = this.Select(SelectedDatesHelper.GetSelectionWithoutDisabled(base.SelectionManager.FocusedDate, base.SelectionManager.SelectionStart, new Func<DateTime, bool>(dateCalculationService.IsDisabled), base.Navigator.AllowMultipleRanges), clearSelection);
            base.SelectUp(clearSelection);
            return flag;
        }

        public override bool Unselect(DateTime dateTime, bool clearSelection)
        {
            IList<DateTime> selection = clearSelection ? new ObservableCollection<DateTime>() : SelectedDatesHelper.Remove(base.SelectionManager.SelectedDates, dateTime);
            base.SelectionManager.SetSelection(selection, true);
            base.Unselect(dateTime, clearSelection);
            return true;
        }
    }
}

