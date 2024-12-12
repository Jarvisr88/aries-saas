namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Xpf.Editors.DateNavigator;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DateNavigatorCalendarCellButton : Button
    {
        public static readonly DependencyProperty CalendarViewProperty;

        static DateNavigatorCalendarCellButton()
        {
            Type ownerType = typeof(DateNavigatorCalendarCellButton);
            CalendarViewProperty = DependencyPropertyManager.Register("CalendarView", typeof(DateNavigatorCalendarView), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButton) d).PropertyChangedCalendarView((DateNavigatorCalendarView) e.OldValue)));
        }

        public DateNavigatorCalendarCellButton()
        {
            this.SetDefaultStyleKey(typeof(DateNavigatorCalendarCellButton));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState(false);
            this.ElementContent = base.GetTemplateChild("PART_Content") as DateNavigatorCalendarCellButtonContent;
            this.UpdateElementContentState();
            this.UpdateElementContentText();
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            this.UpdateElementContentText();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, DateNavigatorCalendar.CellStateProperty))
            {
                this.UpdateVisualState(true);
                this.UpdateElementContentState();
            }
        }

        protected virtual void PropertyChangedCalendarView(DateNavigatorCalendarView oldValue)
        {
        }

        private void UpdateElementContentState()
        {
            if (this.ElementContent != null)
            {
                DateNavigatorCalendar.SetCellState(this.ElementContent, DateNavigatorCalendar.GetCellState(this));
            }
        }

        private void UpdateElementContentText()
        {
            if (this.ElementContent != null)
            {
                this.ElementContent.Text = base.Content?.ToString();
            }
        }

        protected virtual void UpdateVisualState(bool useTransitions)
        {
            DateNavigatorCalendarCellState cellState = DateNavigatorCalendar.GetCellState(this);
            VisualStateManager.GoToState(this, cellState.HasFlag(DateNavigatorCalendarCellState.Special) ? "CellStateSpecial" : "CellStateNotSpecial", true);
            VisualStateManager.GoToState(this, cellState.HasFlag(DateNavigatorCalendarCellState.Holiday) ? "CellStateHoliday" : "CellStateNotHoliday", true);
            VisualStateManager.GoToState(this, cellState.HasFlag(DateNavigatorCalendarCellState.Selected) ? "CellStateSelected" : "CellStateNotSelected", true);
            VisualStateManager.GoToState(this, cellState.HasFlag(DateNavigatorCalendarCellState.Today) ? "CellStateToday" : "CellStateNotToday", true);
            VisualStateManager.GoToState(this, cellState.HasFlag(DateNavigatorCalendarCellState.Focused) ? "CellStateFocused" : "CellStateNotFocused", true);
            if (cellState.HasFlag(DateNavigatorCalendarCellState.Disabled))
            {
                VisualStateManager.GoToState(this, "CellStateActive", true);
                VisualStateManager.GoToState(this, "CellStateDisabled", true);
            }
            else
            {
                VisualStateManager.GoToState(this, cellState.HasFlag(DateNavigatorCalendarCellState.Inactive) ? "CellStateInactive" : "CellStateActive", true);
                VisualStateManager.GoToState(this, "CellStateEnabled", true);
            }
        }

        public DateNavigatorCalendarView CalendarView
        {
            get => 
                (DateNavigatorCalendarView) base.GetValue(CalendarViewProperty);
            set => 
                base.SetValue(CalendarViewProperty, value);
        }

        protected DateNavigatorCalendarCellButtonContent ElementContent { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateNavigatorCalendarCellButton.<>c <>9 = new DateNavigatorCalendarCellButton.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButton) d).PropertyChangedCalendarView((DateNavigatorCalendarView) e.OldValue);
            }
        }
    }
}

