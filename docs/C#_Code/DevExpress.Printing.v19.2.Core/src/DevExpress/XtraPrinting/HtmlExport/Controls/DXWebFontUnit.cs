namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct DXWebFontUnit
    {
        internal static readonly DXWebFontUnit Empty;
        private static readonly DXWebFontUnit Smaller;
        private static readonly DXWebFontUnit Larger;
        private static readonly DXWebFontUnit XXSmall;
        private static readonly DXWebFontUnit XSmall;
        private static readonly DXWebFontUnit Small;
        private static readonly DXWebFontUnit Medium;
        private static readonly DXWebFontUnit Large;
        private static readonly DXWebFontUnit XLarge;
        private static readonly DXWebFontUnit XXLarge;
        private readonly DXWebFontSize type;
        private readonly DXWebUnit value;
        public static implicit operator DXWebFontUnit(int n) => 
            Point(n);

        static DXWebFontUnit()
        {
            Empty = new DXWebFontUnit();
            Smaller = new DXWebFontUnit(DXWebFontSize.Smaller);
            Larger = new DXWebFontUnit(DXWebFontSize.Larger);
            XXSmall = new DXWebFontUnit(DXWebFontSize.XXSmall);
            XSmall = new DXWebFontUnit(DXWebFontSize.XSmall);
            Small = new DXWebFontUnit(DXWebFontSize.Small);
            Medium = new DXWebFontUnit(DXWebFontSize.Medium);
            Large = new DXWebFontUnit(DXWebFontSize.Large);
            XLarge = new DXWebFontUnit(DXWebFontSize.XLarge);
            XXLarge = new DXWebFontUnit(DXWebFontSize.XXLarge);
        }

        public bool IsEmpty =>
            this.type == DXWebFontSize.NotSet;
        public DXWebFontSize Type =>
            this.type;
        public DXWebUnit Unit =>
            this.value;
        public DXWebFontUnit(DXWebFontSize type)
        {
            if ((type < DXWebFontSize.NotSet) || (type > DXWebFontSize.XXLarge))
            {
                throw new ArgumentOutOfRangeException("type");
            }
            this.type = type;
            if (this.type == DXWebFontSize.AsUnit)
            {
                this.value = DXWebUnit.Point(10);
            }
            else
            {
                this.value = DXWebUnit.Empty;
            }
        }

        public DXWebFontUnit(DXWebUnit value)
        {
            this.type = DXWebFontSize.NotSet;
            if (value.IsEmpty)
            {
                this.value = DXWebUnit.Empty;
            }
            else
            {
                this.type = DXWebFontSize.AsUnit;
                this.value = value;
            }
        }

        public DXWebFontUnit(int value)
        {
            this.type = DXWebFontSize.AsUnit;
            this.value = DXWebUnit.Point(value);
        }

        public DXWebFontUnit(double value) : this(new DXWebUnit(value, DXWebUnitType.Point))
        {
        }

        public DXWebFontUnit(double value, DXWebUnitType type) : this(new DXWebUnit(value, type))
        {
        }

        public DXWebFontUnit(string value) : this(value, CultureInfo.CurrentCulture)
        {
        }

        public DXWebFontUnit(string value, CultureInfo culture)
        {
            this.type = DXWebFontSize.NotSet;
            this.value = DXWebUnit.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                char ch = char.ToLowerInvariant(value[0]);
                if (ch == 'l')
                {
                    if (string.Equals(value, "large", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.Large;
                        return;
                    }
                    if (string.Equals(value, "larger", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.Larger;
                        return;
                    }
                }
                else if (ch == 's')
                {
                    if (string.Equals(value, "small", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.Small;
                        return;
                    }
                    if (string.Equals(value, "smaller", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.Smaller;
                        return;
                    }
                }
                else if (ch != 'x')
                {
                    if ((ch == 'm') && string.Equals(value, "medium", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.Medium;
                        return;
                    }
                }
                else
                {
                    if (string.Equals(value, "xx-small", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "xxsmall", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.XXSmall;
                        return;
                    }
                    if (string.Equals(value, "x-small", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "xsmall", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.XSmall;
                        return;
                    }
                    if (string.Equals(value, "x-large", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "xlarge", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.XLarge;
                        return;
                    }
                    if (string.Equals(value, "xx-large", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "xxlarge", StringComparison.OrdinalIgnoreCase))
                    {
                        this.type = DXWebFontSize.XXLarge;
                        return;
                    }
                }
                this.value = new DXWebUnit(value, culture, DXWebUnitType.Point);
                this.type = DXWebFontSize.AsUnit;
            }
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<DXWebFontSize, DXWebUnit>(this.type, this.value);

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is DXWebFontUnit))
            {
                return false;
            }
            DXWebFontUnit unit = (DXWebFontUnit) obj;
            return ((unit.type == this.type) && (unit.value == this.value));
        }

        public static bool operator ==(DXWebFontUnit left, DXWebFontUnit right) => 
            (left.type == right.type) && (left.value == right.value);

        public static bool operator !=(DXWebFontUnit left, DXWebFontUnit right) => 
            (left.type != right.type) || (left.value != right.value);

        public static DXWebFontUnit Parse(string s) => 
            new DXWebFontUnit(s, CultureInfo.InvariantCulture);

        public static DXWebFontUnit Parse(string s, CultureInfo culture) => 
            new DXWebFontUnit(s, culture);

        public static DXWebFontUnit Point(int n) => 
            new DXWebFontUnit(n);

        public override string ToString() => 
            this.ToString((IFormatProvider) CultureInfo.CurrentCulture);

        public string ToString(CultureInfo culture) => 
            this.ToString((IFormatProvider) culture);

        public string ToString(IFormatProvider formatProvider)
        {
            string str = string.Empty;
            if (this.IsEmpty)
            {
                return str;
            }
            DXWebFontSize type = this.type;
            switch (type)
            {
                case DXWebFontSize.AsUnit:
                    return this.value.ToString(formatProvider);

                case DXWebFontSize.Smaller:
                case DXWebFontSize.Larger:
                    break;

                case DXWebFontSize.XXSmall:
                    return "XX-Small";

                case DXWebFontSize.XSmall:
                    return "X-Small";

                default:
                    if (type == DXWebFontSize.XLarge)
                    {
                        return "X-Large";
                    }
                    if (type != DXWebFontSize.XXLarge)
                    {
                        break;
                    }
                    return "XX-Large";
            }
            return this.type.ToString().Replace('_', '-');
        }
    }
}

