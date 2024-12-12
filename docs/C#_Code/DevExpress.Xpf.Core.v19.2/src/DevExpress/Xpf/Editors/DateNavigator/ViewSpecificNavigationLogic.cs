namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;

    public abstract class ViewSpecificNavigationLogic
    {
        protected ViewSpecificNavigationLogic()
        {
        }

        public abstract DateTime GetPage(DateTime dt, int offset);
        protected virtual DateTime Move(DateTime date, Func<DateTime, DateTime> changeHandler)
        {
            try
            {
                return changeHandler(date);
            }
            catch (ArgumentOutOfRangeException)
            {
                return date;
            }
        }

        public abstract DateTime MoveDown(DateTime dt);
        public abstract DateTime MoveLeft(DateTime dt);
        public abstract DateTime MoveRight(DateTime dt);
        public abstract DateTime MoveUp(DateTime dt);
        public virtual DateTime NextPage(DateTime date, int calendarCount) => 
            this.GetPage(date, calendarCount);

        public virtual DateTime PreviousPage(DateTime date, int calendarCount) => 
            this.GetPage(date, -calendarCount);
    }
}

