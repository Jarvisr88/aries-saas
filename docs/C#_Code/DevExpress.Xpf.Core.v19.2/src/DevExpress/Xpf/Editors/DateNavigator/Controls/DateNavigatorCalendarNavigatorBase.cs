namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using System;

    public class DateNavigatorCalendarNavigatorBase
    {
        private DateNavigatorCalendar calendar;

        public DateNavigatorCalendarNavigatorBase(DateNavigatorCalendar calendar)
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

        public DateNavigatorCalendar Calendar =>
            this.calendar;
    }
}

