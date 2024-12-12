namespace DevExpress.Schedule
{
    using System;

    public class ExactWorkDay : KnownDateDay
    {
        public ExactWorkDay(DateTime date, string displayName) : base(date, displayName)
        {
        }

        public override DateCheckResult CheckDate(DateTime date) => 
            !(base.Date.Date == date.Date) ? DateCheckResult.Unknown : DateCheckResult.WorkDay;

        public ExactWorkDay Clone() => 
            (ExactWorkDay) this.CloneCore();

        protected override object CloneCore() => 
            new ExactWorkDay(base.Date, base.DisplayName);

        public override bool Equals(object obj) => 
            (obj is ExactWorkDay) ? base.Equals(obj) : false;

        public override int GetHashCode() => 
            base.GetHashCode();

        public override bool IsWorkDay(DateTime date) => 
            this.CheckDate(date) == DateCheckResult.WorkDay;

        public override WorkDayType Type =>
            WorkDayType.ExactWorkDay;
    }
}

