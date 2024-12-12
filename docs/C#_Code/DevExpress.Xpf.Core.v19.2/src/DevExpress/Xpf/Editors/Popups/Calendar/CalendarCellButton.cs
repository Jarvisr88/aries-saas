namespace DevExpress.Xpf.Editors.Popups.Calendar
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class CalendarCellButton : Button
    {
        static CalendarCellButton()
        {
            Type forType = typeof(CalendarCellButton);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState(false);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, DateEditCalendar.CellInactiveProperty) || (ReferenceEquals(e.Property, DateEditCalendar.CellTodayProperty) || ReferenceEquals(e.Property, DateEditCalendar.CellFocusedProperty)))
            {
                this.UpdateVisualState(true);
            }
        }

        protected virtual void UpdateVisualState(bool useTransitions)
        {
            if (DateEditCalendar.GetCellFocused(this))
            {
                VisualStateManager.GoToState(this, "CellFocusedState", true);
            }
            else if (DateEditCalendar.GetCellToday(this))
            {
                VisualStateManager.GoToState(this, "CellTodayState", true);
            }
            else if (DateEditCalendar.GetCellInactive(this))
            {
                VisualStateManager.GoToState(this, "CellInactiveState", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "CellNormalState", true);
            }
        }
    }
}

