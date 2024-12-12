namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.DateNavigator.Controls;
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class DateEditNavigationStrategy : NavigationStrategyBase
    {
        public DateEditNavigationStrategy(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator) : base(navigator)
        {
            this.SetSelectedDates();
        }

        public override void CheckSelectedDates()
        {
            this.SetSelectedDates();
            throw new Exception(EditorLocalizer.GetString(EditorStringId.CantModifySelectedDates));
        }

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            base.ProcessKeyDown(e);
            if (!e.Handled)
            {
                Key key = e.Key;
                if (key == Key.Return)
                {
                    this.Select(base.Navigator.FocusedDate, true);
                    e.Handled = true;
                }
                else
                {
                    switch (key)
                    {
                        case Key.Left:
                            e.Handled = base.MoveLeftRight(true);
                            return;

                        case Key.Up:
                            e.Handled = this.MoveUp();
                            return;

                        case Key.Right:
                            e.Handled = base.MoveLeftRight(false);
                            return;

                        case Key.Down:
                            e.Handled = this.MoveDown();
                            return;
                    }
                }
            }
        }

        public override void ProcessMouseDown(DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            if ((buttonKind != DateNavigatorCalendarButtonKind.WeekNumber) && DateNavigatorHelper.HasEnabledDatesInView(base.Navigator, buttonDate))
            {
                if (base.Navigator.CalendarView == DateNavigatorCalendarView.Month)
                {
                    DateTime dateTime = buttonDate;
                    this.Move(dateTime);
                }
                base.ProcessMouseDown(buttonDate, buttonKind);
            }
        }

        protected override void ProcessMouseUpCore(DateTime? date)
        {
            if (((date != null) && date.Value.Equals(base.SelectionManager.FocusedDate)) && DateNavigatorHelper.HasEnabledDatesInView(base.Navigator, date.Value))
            {
                if (base.Navigator.CalendarView != DateNavigatorCalendarView.Month)
                {
                    base.ProcessMouseUpCore(date);
                }
                else
                {
                    DateTime dateTime = date.Value;
                    this.Move(dateTime);
                    this.Select(dateTime, true);
                }
            }
        }

        public override bool Select(DateTime dateTime, bool clearSelection)
        {
            bool flag = base.Select(dateTime, clearSelection);
            Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, DateEdit> evaluator = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, DateEdit> local1 = <>c.<>9__1_0;
                evaluator = <>c.<>9__1_0 = x => x.OwnerDateEdit;
            }
            DateEdit input = base.Navigator.With<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, DateEdit>(evaluator);
            if (flag)
            {
                Func<DateEdit, TimePicker> func2 = <>c.<>9__1_1;
                if (<>c.<>9__1_1 == null)
                {
                    Func<DateEdit, TimePicker> local2 = <>c.<>9__1_1;
                    func2 = <>c.<>9__1_1 = x => x.TimePicker;
                }
                if (input.With<DateEdit, TimePicker>(func2) != null)
                {
                    input.TimePicker.SetDate(dateTime);
                }
            }
            if (flag)
            {
                Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> func3 = <>c.<>9__1_2;
                if (<>c.<>9__1_2 == null)
                {
                    Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> local3 = <>c.<>9__1_2;
                    func3 = <>c.<>9__1_2 = x => x.IsLoaded;
                }
                if (base.Navigator.Return<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool>(func3, <>c.<>9__1_3 ??= () => false))
                {
                    Func<DateEdit, bool> func4 = <>c.<>9__1_4;
                    if (<>c.<>9__1_4 == null)
                    {
                        Func<DateEdit, bool> local5 = <>c.<>9__1_4;
                        func4 = <>c.<>9__1_4 = x => !x.IsReadOnly;
                    }
                    if (input.Return<DateEdit, bool>(func4, (<>c.<>9__1_5 ??= () => false)) && (input.PropertyProvider.GetPopupFooterButtons() != PopupFooterButtons.OkCancel))
                    {
                        input.SetDateTime(((IDateEditCalendarBase) base.Navigator).DateTime.Date.Add(input.DateTime.TimeOfDay), UpdateEditorSource.ValueChanging);
                        input.ClosePopup();
                    }
                }
            }
            return flag;
        }

        private void SetSelectedDates()
        {
            List<DateTime> list1 = new List<DateTime>();
            list1.Add(base.Navigator.FocusedDate);
            base.Navigator.SetSelectedDates(list1);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateEditNavigationStrategy.<>c <>9 = new DateEditNavigationStrategy.<>c();
            public static Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, DateEdit> <>9__1_0;
            public static Func<DateEdit, TimePicker> <>9__1_1;
            public static Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> <>9__1_2;
            public static Func<bool> <>9__1_3;
            public static Func<DateEdit, bool> <>9__1_4;
            public static Func<bool> <>9__1_5;

            internal DateEdit <Select>b__1_0(DevExpress.Xpf.Editors.DateNavigator.DateNavigator x) => 
                x.OwnerDateEdit;

            internal TimePicker <Select>b__1_1(DateEdit x) => 
                x.TimePicker;

            internal bool <Select>b__1_2(DevExpress.Xpf.Editors.DateNavigator.DateNavigator x) => 
                x.IsLoaded;

            internal bool <Select>b__1_3() => 
                false;

            internal bool <Select>b__1_4(DateEdit x) => 
                !x.IsReadOnly;

            internal bool <Select>b__1_5() => 
                false;
        }
    }
}

