namespace DevExpress.Utils.Filtering
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public sealed class FilterDateTimeRangeAttribute : BaseFilterRangeAttribute
    {
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly FilterDateTimeRangeAttribute Implicit = new FilterDateTimeRangeAttribute();

        public FilterDateTimeRangeAttribute() : this(null, null)
        {
        }

        public FilterDateTimeRangeAttribute(string minOrMinMember, string maxOrMaxMember) : base(minOrMinMember, maxOrMaxMember)
        {
        }

        protected override bool TryParse(string str, out object value)
        {
            DateTime time;
            TimeSpan span;
            if (DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out time))
            {
                value = time;
                return true;
            }
            if (!TimeSpan.TryParse(str, CultureInfo.InvariantCulture, out span))
            {
                return base.TryParse(str, out value);
            }
            value = span;
            return true;
        }

        public bool IsImplicit =>
            ReferenceEquals(this, Implicit);

        public DateTimeRangeUIEditorType EditorType { get; set; }

        public object Minimum
        {
            get => 
                base.Minimum;
            set => 
                base.Minimum = value;
        }

        public object Maximum
        {
            get => 
                base.Maximum;
            set => 
                base.Maximum = value;
        }
    }
}

