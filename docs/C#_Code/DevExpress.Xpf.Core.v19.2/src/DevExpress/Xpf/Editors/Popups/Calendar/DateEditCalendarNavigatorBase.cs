namespace DevExpress.Xpf.Editors.Popups.Calendar
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class DateEditCalendarNavigatorBase
    {
        private DateEditCalendar calendar;

        public DateEditCalendarNavigatorBase(DateEditCalendar calendar)
        {
            this.calendar = calendar;
        }

        protected virtual bool OnDown() => 
            false;

        protected virtual bool OnEnd() => 
            false;

        protected virtual bool OnEnter() => 
            false;

        protected virtual bool OnHome() => 
            false;

        protected virtual bool OnLeft() => 
            false;

        protected virtual bool OnPageDown() => 
            false;

        protected virtual bool OnPageUp() => 
            false;

        protected virtual bool OnRight() => 
            false;

        protected virtual bool OnUp() => 
            false;

        public virtual bool ProcessKeyDown(KeyEventArgs e)
        {
            Key key = e.Key;
            if (key != Key.Return)
            {
                switch (key)
                {
                    case Key.Space:
                        break;

                    case Key.Prior:
                        return this.OnPageUp();

                    case Key.Next:
                        return this.OnPageDown();

                    case Key.End:
                        return this.OnEnd();

                    case Key.Home:
                        return this.OnHome();

                    case Key.Left:
                        return ((this.Calendar.FlowDirection == FlowDirection.LeftToRight) ? this.OnLeft() : this.OnRight());

                    case Key.Up:
                        return this.OnUp();

                    case Key.Right:
                        return ((this.Calendar.FlowDirection == FlowDirection.LeftToRight) ? this.OnRight() : this.OnLeft());

                    case Key.Down:
                        return this.OnDown();

                    default:
                        return false;
                }
            }
            return this.OnEnter();
        }

        public DateEditCalendar Calendar =>
            this.calendar;
    }
}

