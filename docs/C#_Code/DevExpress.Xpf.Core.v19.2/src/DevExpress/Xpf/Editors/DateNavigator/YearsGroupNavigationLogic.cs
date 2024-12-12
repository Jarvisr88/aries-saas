namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Runtime.CompilerServices;

    public class YearsGroupNavigationLogic : ViewSpecificNavigationLogic
    {
        public override DateTime GetPage(DateTime date, int offset) => 
            this.Move(date, dt => dt.AddYears(offset * 100));

        public override DateTime MoveDown(DateTime date)
        {
            Func<DateTime, DateTime> changeHandler = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<DateTime, DateTime> local1 = <>c.<>9__2_0;
                changeHandler = <>c.<>9__2_0 = dt => dt.AddYears(40);
            }
            return this.Move(date, changeHandler);
        }

        public override DateTime MoveLeft(DateTime date)
        {
            Func<DateTime, DateTime> changeHandler = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<DateTime, DateTime> local1 = <>c.<>9__0_0;
                changeHandler = <>c.<>9__0_0 = dt => dt.AddYears(-10);
            }
            return this.Move(date, changeHandler);
        }

        public override DateTime MoveRight(DateTime date)
        {
            Func<DateTime, DateTime> changeHandler = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<DateTime, DateTime> local1 = <>c.<>9__1_0;
                changeHandler = <>c.<>9__1_0 = dt => dt.AddYears(10);
            }
            return this.Move(date, changeHandler);
        }

        public override DateTime MoveUp(DateTime date)
        {
            Func<DateTime, DateTime> changeHandler = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DateTime, DateTime> local1 = <>c.<>9__3_0;
                changeHandler = <>c.<>9__3_0 = dt => dt.AddYears(-40);
            }
            return this.Move(date, changeHandler);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly YearsGroupNavigationLogic.<>c <>9 = new YearsGroupNavigationLogic.<>c();
            public static Func<DateTime, DateTime> <>9__0_0;
            public static Func<DateTime, DateTime> <>9__1_0;
            public static Func<DateTime, DateTime> <>9__2_0;
            public static Func<DateTime, DateTime> <>9__3_0;

            internal DateTime <MoveDown>b__2_0(DateTime dt) => 
                dt.AddYears(40);

            internal DateTime <MoveLeft>b__0_0(DateTime dt) => 
                dt.AddYears(-10);

            internal DateTime <MoveRight>b__1_0(DateTime dt) => 
                dt.AddYears(10);

            internal DateTime <MoveUp>b__3_0(DateTime dt) => 
                dt.AddYears(-40);
        }
    }
}

