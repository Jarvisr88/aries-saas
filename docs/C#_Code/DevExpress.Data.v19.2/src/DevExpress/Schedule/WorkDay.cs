namespace DevExpress.Schedule
{
    using System;

    public abstract class WorkDay : ICloneable
    {
        protected WorkDay()
        {
        }

        public abstract DateCheckResult CheckDate(DateTime date);
        protected abstract object CloneCore();
        public abstract bool IsWorkDay(DateTime date);
        object ICloneable.Clone() => 
            this.CloneCore();

        public abstract WorkDayType Type { get; }
    }
}

