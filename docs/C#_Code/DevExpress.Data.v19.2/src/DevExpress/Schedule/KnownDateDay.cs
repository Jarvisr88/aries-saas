namespace DevExpress.Schedule
{
    using System;

    public abstract class KnownDateDay : WorkDay
    {
        private readonly DateTime date;
        private string displayName;

        protected KnownDateDay(DateTime date, string displayName)
        {
            this.date = date;
            this.displayName = displayName;
        }

        public override bool Equals(object obj)
        {
            KnownDateDay day = obj as KnownDateDay;
            return ((day != null) ? ((this.Date == day.Date) && (this.DisplayName == day.DisplayName)) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public string DisplayName
        {
            get => 
                this.displayName;
            set => 
                this.displayName = value;
        }

        public DateTime Date =>
            this.date;
    }
}

