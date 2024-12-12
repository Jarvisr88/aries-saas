namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class IntervalFactory
    {
        protected IntervalFactory()
        {
        }

        public virtual string FormatLabelText(object value)
        {
            object[] args = new object[] { value };
            return string.Format(CultureInfo.CurrentCulture, this.LabelTextFormat, args);
        }

        public abstract bool FormatText(object current, out string text, double fontSize, double length);
        public virtual string GetLongestText(object current)
        {
            string longestTextFormat = this.LongestTextFormat;
            string format = longestTextFormat;
            if (longestTextFormat == null)
            {
                string local1 = longestTextFormat;
                format = this.DefaultLongestTextFormat;
            }
            object[] args = new object[] { current };
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        public abstract object GetNextValue(object value);
        public abstract object Snap(object value);

        public virtual string ShortTextFormat { get; set; }

        public virtual string TextFormat { get; set; }

        public virtual string LongTextFormat { get; set; }

        public virtual string LongestTextFormat { get; set; }

        protected virtual string DefaultLongestTextFormat =>
            string.Empty;

        public virtual string LabelTextFormat =>
            string.Empty;
    }
}

