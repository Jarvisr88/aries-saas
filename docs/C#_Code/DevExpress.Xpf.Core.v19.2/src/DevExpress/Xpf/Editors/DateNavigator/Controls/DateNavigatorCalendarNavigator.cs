namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Xpf.Editors.DateNavigator;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DateNavigatorCalendarNavigator : DateNavigatorCalendarNavigatorBase
    {
        public DateNavigatorCalendarNavigator(DateNavigatorCalendar calendar) : base(calendar)
        {
        }

        protected virtual FrameworkElement FindCell(int row, int col)
        {
            FrameworkElement element2;
            if (base.Calendar.ActiveContent.ContentGrid == null)
            {
                return null;
            }
            using (IEnumerator enumerator = base.Calendar.ActiveContent.ContentGrid.Children.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        UIElement current = (UIElement) enumerator.Current;
                        if ((Grid.GetColumn(current as FrameworkElement) != col) || (Grid.GetRow(current as FrameworkElement) != row))
                        {
                            continue;
                        }
                        element2 = current as FrameworkElement;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return element2;
        }

        protected virtual FrameworkElement FindCellWithDate(DateTime dt, DateRelation relation, DateTime startBound)
        {
            DateTime bound = startBound;
            FrameworkElement element = null;
            if (base.Calendar.ActiveContent.ContentGrid == null)
            {
                return null;
            }
            foreach (UIElement element2 in base.Calendar.ActiveContent.ContentGrid.Children)
            {
                DateTime dateTime = DateNavigatorCalendar.GetDateTime(element2);
                if (relation(dt, dateTime, bound))
                {
                    bound = dateTime;
                    element = element2 as FrameworkElement;
                }
            }
            return element;
        }

        protected virtual FrameworkElement FindCellWithGreaterDate()
        {
            FrameworkElement element = this.FindFocusedCell();
            return ((element != null) ? this.FindCellWithDate(DateNavigatorCalendar.GetDateTime(element), new DateRelation(this.IsNearGreater), DateTime.MaxValue) : null);
        }

        protected virtual FrameworkElement FindCellWithLessDate()
        {
            FrameworkElement element = this.FindFocusedCell();
            return ((element != null) ? this.FindCellWithDate(DateNavigatorCalendar.GetDateTime(element), new DateRelation(this.IsNearLess), DateTime.MinValue) : null);
        }

        protected virtual FrameworkElement FindDownCellFromFocused()
        {
            FrameworkElement element = this.FindFocusedCell();
            return ((element != null) ? this.FindCell(Grid.GetRow(element) + 1, Grid.GetColumn(element)) : null);
        }

        protected virtual FrameworkElement FindFocusedCell() => 
            (base.Calendar.ActiveContent.ContentGrid != null) ? base.Calendar.ActiveContent.GetFocusedCell() : null;

        protected virtual FrameworkElement FindLeftCellFromFocused()
        {
            FrameworkElement element = this.FindFocusedCell();
            return ((element != null) ? this.FindCell(Grid.GetRow(element), Grid.GetColumn(element) - 1) : null);
        }

        protected virtual FrameworkElement FindRightCellFromFocused()
        {
            FrameworkElement element = this.FindFocusedCell();
            return ((element != null) ? this.FindCell(Grid.GetRow(element), Grid.GetColumn(element) + 1) : null);
        }

        protected virtual FrameworkElement FindUpCellFromFocused()
        {
            FrameworkElement element = this.FindFocusedCell();
            return ((element != null) ? this.FindCell(Grid.GetRow(element) - 1, Grid.GetColumn(element)) : null);
        }

        protected virtual DateTime GetNextDate(FrameworkElement focusedCell, FrameworkElement nextCell, int upDownDir, int leftRightDir)
        {
            if (nextCell != null)
            {
                return DateNavigatorCalendar.GetDateTime(nextCell);
            }
            DateTime dateTime = DateNavigatorCalendar.GetDateTime(focusedCell);
            switch (base.Calendar.ActiveContent.State)
            {
                case DateNavigatorCalendarView.Month:
                    return base.Calendar.AddDate(dateTime, 0, 0, (upDownDir != 0) ? (7 * upDownDir) : leftRightDir);

                case DateNavigatorCalendarView.Year:
                    return base.Calendar.AddDate(dateTime, 0, (upDownDir != 0) ? (4 * upDownDir) : leftRightDir, 0);

                case DateNavigatorCalendarView.Years:
                    return base.Calendar.AddDate(dateTime, (upDownDir != 0) ? (4 * upDownDir) : leftRightDir, 0, 0);

                case DateNavigatorCalendarView.YearsRange:
                    return base.Calendar.AddDate(dateTime, (upDownDir != 0) ? (40 * upDownDir) : leftRightDir, 0, 0);
            }
            return dateTime;
        }

        protected virtual bool IsNearGreater(DateTime current, DateTime dt, DateTime bound) => 
            (dt > current) && (dt < bound);

        protected virtual bool IsNearLess(DateTime current, DateTime dt, DateTime bound) => 
            (dt < current) && (dt > bound);

        protected virtual bool OnArrowKey(FindNextCell method, int upDownDir, int leftRightDir)
        {
            FrameworkElement element = this.FindFocusedCell();
            if (element == null)
            {
                return false;
            }
            FrameworkElement element2 = method();
            if (element2 != null)
            {
                DateNavigatorCalendar.SetCellFocused(element, false);
                if (!DateNavigatorCalendar.GetCellState(element2).HasFlag(DateNavigatorCalendarCellState.Inactive))
                {
                    DateNavigatorCalendar.SetCellFocused(element2, true);
                    return true;
                }
            }
            this.Shift(this.GetNextDate(element, element2, upDownDir, leftRightDir), -((upDownDir != 0) ? upDownDir : leftRightDir));
            return true;
        }

        protected override bool OnDown() => 
            this.OnArrowKey(new FindNextCell(this.FindDownCellFromFocused), 1, 0);

        protected override bool OnEnter()
        {
            FrameworkElement element = this.FindFocusedCell();
            if (element == null)
            {
                return false;
            }
            base.Calendar.OnButtonClick(DateNavigatorCalendar.GetDateTime(element), DateNavigatorCalendarButtonKind.Date);
            return true;
        }

        protected override bool OnLeft() => 
            this.OnArrowKey(new FindNextCell(this.FindCellWithLessDate), 0, -1);

        protected override bool OnRight() => 
            this.OnArrowKey(new FindNextCell(this.FindCellWithGreaterDate), 0, 1);

        protected override bool OnUp() => 
            this.OnArrowKey(new FindNextCell(this.FindUpCellFromFocused), -1, 0);

        protected virtual void Shift(DateTime dt, int dir)
        {
            if (dir > 0)
            {
                this.ShiftRight(dt);
            }
            else
            {
                this.ShiftLeft(dt);
            }
        }

        protected virtual void ShiftLeft(DateTime dt)
        {
            base.Calendar.SetNewContent(dt, DateNavigatorCalendarTransferType.ShiftLeft);
        }

        protected virtual void ShiftRight(DateTime dt)
        {
            base.Calendar.SetNewContent(dt, DateNavigatorCalendarTransferType.ShiftRight);
        }

        protected delegate bool DateRelation(DateTime current, DateTime dt, DateTime bound);

        protected delegate FrameworkElement FindNextCell();

        protected delegate void ShiftMethod(DateTime dt);
    }
}

