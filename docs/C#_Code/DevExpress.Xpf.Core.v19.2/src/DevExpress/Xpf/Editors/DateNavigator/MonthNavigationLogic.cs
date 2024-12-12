namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Runtime.CompilerServices;

    public class MonthNavigationLogic : ViewSpecificNavigationLogic
    {
        public override DateTime GetPage(DateTime date, int offset) => 
            this.Move(date, dt => dt.AddMonths(offset));

        public override DateTime MoveDown(DateTime date)
        {
            Func<DateTime, DateTime> changeHandler = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DateTime, DateTime> local1 = <>c.<>9__3_0;
                changeHandler = <>c.<>9__3_0 = dt => dt.AddDays(7.0);
            }
            return this.Move(date, changeHandler);
        }

        public override DateTime MoveLeft(DateTime date)
        {
            Func<DateTime, DateTime> changeHandler = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<DateTime, DateTime> local1 = <>c.<>9__0_0;
                changeHandler = <>c.<>9__0_0 = dt => dt.AddDays(-1.0);
            }
            return this.Move(date, changeHandler);
        }

        public override DateTime MoveRight(DateTime date)
        {
            Func<DateTime, DateTime> changeHandler = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<DateTime, DateTime> local1 = <>c.<>9__1_0;
                changeHandler = <>c.<>9__1_0 = dt => dt.AddDays(1.0);
            }
            return this.Move(date, changeHandler);
        }

        public override DateTime MoveUp(DateTime date)
        {
            Func<DateTime, DateTime> changeHandler = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<DateTime, DateTime> local1 = <>c.<>9__2_0;
                changeHandler = <>c.<>9__2_0 = dt => dt.AddDays(-7.0);
            }
            return this.Move(date, changeHandler);
        }

        public override DateTime NextPage(DateTime date, int calendarCount) => 
            this.GetPage(date, 1);

        public override DateTime PreviousPage(DateTime date, int calendarCount) => 
            this.GetPage(date, -1);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MonthNavigationLogic.<>c <>9 = new MonthNavigationLogic.<>c();
            public static Func<DateTime, DateTime> <>9__0_0;
            public static Func<DateTime, DateTime> <>9__1_0;
            public static Func<DateTime, DateTime> <>9__2_0;
            public static Func<DateTime, DateTime> <>9__3_0;

            internal DateTime <MoveDown>b__3_0(DateTime dt) => 
                dt.AddDays(7.0);

            internal DateTime <MoveLeft>b__0_0(DateTime dt) => 
                dt.AddDays(-1.0);

            internal DateTime <MoveRight>b__1_0(DateTime dt) => 
                dt.AddDays(1.0);

            internal DateTime <MoveUp>b__2_0(DateTime dt) => 
                dt.AddDays(-7.0);
        }
    }
}

