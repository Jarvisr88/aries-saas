namespace DevExpress.Xpf.Editors.Popups.Calendar
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DateEditCalendarNavigator : DateEditCalendarNavigatorBase
    {
        public DateEditCalendarNavigator(DateEditCalendar calendar) : base(calendar)
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
                        if ((Grid.GetColumn(current as FrameworkElement) != col) || ((Grid.GetRow(current as FrameworkElement) != row) || (DateEditCalendarBase.GetDateTime(current) == null)))
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
                if (DateEditCalendarBase.GetDateTime(element2) != null)
                {
                    DateTime dateTime = (DateTime) DateEditCalendarBase.GetDateTime(element2);
                    if (relation(dt, dateTime, bound))
                    {
                        bound = dateTime;
                        element = element2 as FrameworkElement;
                    }
                }
            }
            return element;
        }

        protected virtual FrameworkElement FindCellWithGreaterDate()
        {
            FrameworkElement element = this.FindFocusedCell();
            return ((element != null) ? this.FindCellWithDate((DateTime) DateEditCalendarBase.GetDateTime(element), new DateRelation(this.IsNearGreater), base.Calendar.GetMaxValue()) : null);
        }

        protected virtual FrameworkElement FindCellWithLessDate()
        {
            FrameworkElement element = this.FindFocusedCell();
            return ((element != null) ? this.FindCellWithDate((DateTime) DateEditCalendarBase.GetDateTime(element), new DateRelation(this.IsNearLess), base.Calendar.GetMinValue()) : null);
        }

        protected virtual FrameworkElement FindDownCellFromFocused()
        {
            FrameworkElement element = this.FindFocusedCell();
            if (element == null)
            {
                return null;
            }
            FrameworkElement element2 = this.FindCell(Grid.GetRow(element) + 1, Grid.GetColumn(element));
            return ((element2 != null) ? ((((DateTime) DateEditCalendarBase.GetDateTime(element2)) <= base.Calendar.GetMaxValue()) ? element2 : null) : null);
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
            if (element == null)
            {
                return null;
            }
            FrameworkElement element2 = this.FindCell(Grid.GetRow(element) - 1, Grid.GetColumn(element));
            return ((element2 != null) ? ((((DateTime) DateEditCalendarBase.GetDateTime(element2)) >= base.Calendar.GetMinValue()) ? element2 : null) : null);
        }

        protected virtual DateTime GetNextDate(FrameworkElement focusedCell, FrameworkElement nextCell, int upDownDir, int leftRightDir)
        {
            if (nextCell != null)
            {
                return (DateTime) DateEditCalendarBase.GetDateTime(nextCell);
            }
            DateTime dateTime = (DateTime) DateEditCalendarBase.GetDateTime(focusedCell);
            switch (base.Calendar.ActiveContent.State)
            {
                case DateEditCalendarState.Month:
                    return base.Calendar.AddDate(dateTime, 0, 0, (upDownDir != 0) ? (7 * upDownDir) : leftRightDir);

                case DateEditCalendarState.Year:
                    return base.Calendar.AddDate(dateTime, 0, (upDownDir != 0) ? (4 * upDownDir) : leftRightDir, 0);

                case DateEditCalendarState.Years:
                    return base.Calendar.AddDate(dateTime, (upDownDir != 0) ? (4 * upDownDir) : leftRightDir, 0, 0);

                case DateEditCalendarState.YearsGroup:
                    return base.Calendar.AddDate(dateTime, (upDownDir != 0) ? (40 * upDownDir) : leftRightDir, 0, 0);
            }
            return dateTime;
        }

        protected virtual bool IsNearGreater(DateTime current, DateTime dt, DateTime bound) => 
            (dt > current) && (dt <= bound);

        protected virtual bool IsNearLess(DateTime current, DateTime dt, DateTime bound) => 
            (dt < current) && (dt >= bound);

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
                DateEditCalendar.SetCellFocused(element, false);
                if (!DateEditCalendar.GetCellInactive(element2))
                {
                    DateEditCalendar.SetCellFocused(element2, true);
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
            FrameworkElement sender = this.FindFocusedCell();
            if (sender == null)
            {
                return false;
            }
            base.Calendar.OnCellButtonClick(sender, new RoutedEventArgs());
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
            if (base.Calendar.GetMaxValue() != dt)
            {
                base.Calendar.SetNewContent(dt, DateEditCalendarTransferType.ShiftLeft);
            }
        }

        protected virtual void ShiftRight(DateTime dt)
        {
            if (base.Calendar.GetMinValue() != dt)
            {
                base.Calendar.SetNewContent(dt, DateEditCalendarTransferType.ShiftRight);
            }
        }

        protected delegate bool DateRelation(DateTime current, DateTime dt, DateTime bound);

        protected delegate FrameworkElement FindNextCell();

        protected delegate void ShiftMethod(DateTime dt);
    }
}

