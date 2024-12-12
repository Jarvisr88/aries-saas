namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.DateNavigator.Controls;
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class SingleSelectionNavigationStrategy : NavigationStrategyBase
    {
        public SingleSelectionNavigationStrategy(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator) : base(navigator)
        {
            this.SetSelectedDates();
        }

        public override void CheckSelectedDates()
        {
            this.SetSelectedDates();
            throw new Exception(EditorLocalizer.GetString(EditorStringId.CantModifySelectedDates));
        }

        public override bool Move(DateTime dateTime) => 
            base.Move(dateTime) & this.Select(dateTime, true);

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            base.ProcessKeyDown(e);
            if (!e.Handled)
            {
                switch (e.Key)
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

        public override void ProcessMouseDown(DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            if ((buttonKind != DateNavigatorCalendarButtonKind.WeekNumber) && DateNavigatorHelper.HasEnabledDatesInView(base.Navigator, buttonDate))
            {
                if (base.Navigator.CalendarView != DateNavigatorCalendarView.Month)
                {
                    base.ProcessMouseDown(buttonDate, buttonKind);
                }
                else
                {
                    DateTime dateTime = buttonDate;
                    this.Move(dateTime);
                    this.Select(dateTime, true);
                }
            }
        }

        private void SetSelectedDates()
        {
            List<DateTime> list1 = new List<DateTime>();
            list1.Add(base.Navigator.FocusedDate);
            base.Navigator.SetSelectedDates(list1);
        }
    }
}

