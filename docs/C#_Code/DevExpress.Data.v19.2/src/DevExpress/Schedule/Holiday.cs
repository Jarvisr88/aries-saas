namespace DevExpress.Schedule
{
    using System;

    public class Holiday : KnownDateDay
    {
        private string location;

        public Holiday(DateTime date, string displayName) : base(date, displayName)
        {
            this.location = string.Empty;
        }

        public Holiday(DateTime date, string displayName, string location) : base(date, displayName)
        {
            this.location = string.Empty;
            this.location = location;
        }

        public override DateCheckResult CheckDate(DateTime date) => 
            !(base.Date.Date == date.Date) ? DateCheckResult.Unknown : DateCheckResult.Holiday;

        public Holiday Clone() => 
            (Holiday) this.CloneCore();

        protected override object CloneCore() => 
            new Holiday(base.Date, base.DisplayName, this.Location);

        public override bool Equals(object obj)
        {
            Holiday holiday = obj as Holiday;
            return ((holiday != null) ? ((this.Location == holiday.Location) && base.Equals(obj)) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public override bool IsWorkDay(DateTime date) => 
            this.CheckDate(date) == DateCheckResult.Unknown;

        public override WorkDayType Type =>
            WorkDayType.Holiday;

        public string Location
        {
            get => 
                this.location;
            set => 
                this.location = value;
        }
    }
}

