namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class DateTimeIntervalFactory : IntervalFactory
    {
        protected DateTimeIntervalFactory()
        {
        }

        protected bool Equals(DateTimeIntervalFactory other) => 
            Equals(base.GetType(), other.GetType());

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? this.Equals((DateTimeIntervalFactory) obj) : true) : false;

        public string FormatLongText(object current)
        {
            string longTextFormat = this.LongTextFormat;
            string format = longTextFormat;
            if (longTextFormat == null)
            {
                string local1 = longTextFormat;
                format = this.DefaultLongTextFormat;
            }
            return this.FormatTextInternal(current, format);
        }

        public string FormatMiddleText(object current)
        {
            string textFormat = this.TextFormat;
            string format = textFormat;
            if (textFormat == null)
            {
                string local1 = textFormat;
                format = this.DefaultTextFormat;
            }
            return this.FormatTextInternal(current, format);
        }

        public string FormatShortText(object current)
        {
            string shortTextFormat = this.ShortTextFormat;
            string format = shortTextFormat;
            if (shortTextFormat == null)
            {
                string local1 = shortTextFormat;
                format = this.DefaultShortTextFormat;
            }
            return this.FormatTextInternal(current, format);
        }

        public override bool FormatText(object current, out string text, double fontSize, double length)
        {
            text = string.Empty;
            if (length < (this.MinLength * fontSize))
            {
                string shortTextFormat = this.ShortTextFormat;
                string format = shortTextFormat;
                if (shortTextFormat == null)
                {
                    string local1 = shortTextFormat;
                    format = this.DefaultShortTextFormat;
                }
                text = this.FormatTextInternal(current, format);
                return false;
            }
            if (length < (this.ShortTextMaxLength * fontSize))
            {
                string shortTextFormat = this.ShortTextFormat;
                string format = shortTextFormat;
                if (shortTextFormat == null)
                {
                    string local2 = shortTextFormat;
                    format = this.DefaultShortTextFormat;
                }
                text = this.FormatTextInternal(current, format);
            }
            else if (length < (this.TextMaxLength * fontSize))
            {
                string textFormat = this.TextFormat;
                string format = textFormat;
                if (textFormat == null)
                {
                    string local3 = textFormat;
                    format = this.DefaultTextFormat;
                }
                text = this.FormatTextInternal(current, format);
            }
            else
            {
                string longTextFormat = this.LongTextFormat;
                string format = longTextFormat;
                if (longTextFormat == null)
                {
                    string local4 = longTextFormat;
                    format = this.DefaultLongTextFormat;
                }
                text = this.FormatTextInternal(current, format);
            }
            return true;
        }

        protected virtual string FormatTextInternal(object current, string format) => 
            string.Format(format, current);

        public override int GetHashCode() => 
            base.GetType().GetHashCode();

        public override object Snap(object value) => 
            (value != null) ? this.SnapInternal(this.ToDateTime(value)) : value;

        protected abstract DateTime SnapInternal(DateTime dt);
        protected DateTime ToDateTime(object value) => 
            Convert.ToDateTime(value);

        protected abstract string DefaultShortTextFormat { get; }

        protected abstract string DefaultTextFormat { get; }

        protected abstract string DefaultLongTextFormat { get; }

        protected virtual double TextMaxLength =>
            10.0 * this.MinLength;

        protected virtual double ShortTextMaxLength =>
            2.0 * this.MinLength;

        protected virtual double MinLength =>
            2.0;
    }
}

