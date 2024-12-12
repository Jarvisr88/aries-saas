namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class DateTimePickerData
    {
        protected bool Equals(DateTimePickerData other)
        {
            switch (this.DateTimePart)
            {
                case DevExpress.Xpf.Editors.DateTimePart.Day:
                    return (this.Value.Day.Equals(other.Value.Day) && (this.Value.Month.Equals(other.Value.Month) && this.Value.Year.Equals(other.Value.Year)));

                case DevExpress.Xpf.Editors.DateTimePart.Month:
                    return this.Value.Month.Equals(other.Value.Month);

                case DevExpress.Xpf.Editors.DateTimePart.Year:
                    return this.Value.Year.Equals(other.Value.Year);

                case DevExpress.Xpf.Editors.DateTimePart.Hour12:
                    return (this.Value.Hour % 12).Equals((int) (other.Value.Hour % 12));

                case DevExpress.Xpf.Editors.DateTimePart.Hour24:
                    return this.Value.Hour.Equals(other.Value.Hour);

                case DevExpress.Xpf.Editors.DateTimePart.Minute:
                    return this.Value.Minute.Equals(other.Value.Minute);

                case DevExpress.Xpf.Editors.DateTimePart.Second:
                    return this.Value.Second.Equals(other.Value.Second);

                case DevExpress.Xpf.Editors.DateTimePart.Millisecond:
                    return this.Value.Hour.Equals(other.Value.Millisecond);

                case DevExpress.Xpf.Editors.DateTimePart.Period:
                case DevExpress.Xpf.Editors.DateTimePart.PeriodOfEra:
                    return false;

                case DevExpress.Xpf.Editors.DateTimePart.AmPm:
                    return (((this.Value.Hour <= 11) || (other.Value.Hour <= 11)) ? ((this.Value.Hour <= 11) && (other.Value.Hour <= 11)) : true);
            }
            return this.Value.Equals(other.Value);
        }

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((DateTimePickerData) obj) : false) : true) : false;

        public override int GetHashCode() => 
            this.Value.GetHashCode();

        public string Text { get; set; }

        public DateTime Value { get; set; }

        public DevExpress.Xpf.Editors.DateTimePart DateTimePart { get; set; }
    }
}

