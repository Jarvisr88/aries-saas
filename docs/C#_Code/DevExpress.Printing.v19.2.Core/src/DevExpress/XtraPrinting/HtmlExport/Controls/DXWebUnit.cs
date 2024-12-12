namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct DXWebUnit
    {
        public static readonly DXWebUnit Empty;
        internal const int MaxValue = 0x7fff;
        internal const int MinValue = -32768;
        private readonly DXWebUnitType type;
        private readonly double value;
        static DXWebUnit()
        {
            Empty = new DXWebUnit();
        }

        public static bool operator ==(DXWebUnit left, DXWebUnit right) => 
            (left.type == right.type) && (left.value == right.value);

        public static bool operator !=(DXWebUnit left, DXWebUnit right) => 
            (left.type != right.type) || !(left.value == right.value);

        private static string GetStringFromType(DXWebUnitType type)
        {
            switch (type)
            {
                case DXWebUnitType.Pixel:
                    return "px";

                case DXWebUnitType.Point:
                    return "pt";

                case DXWebUnitType.Pica:
                    return "pc";

                case DXWebUnitType.Inch:
                    return "in";

                case DXWebUnitType.Mm:
                    return "mm";

                case DXWebUnitType.Cm:
                    return "cm";

                case DXWebUnitType.Percentage:
                    return "%";

                case DXWebUnitType.Em:
                    return "em";

                case DXWebUnitType.Ex:
                    return "ex";
            }
            return string.Empty;
        }

        private static DXWebUnitType GetTypeFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DXWebUnitType.Pixel;
            }
            if (value.Equals("px"))
            {
                return DXWebUnitType.Pixel;
            }
            if (value.Equals("pt"))
            {
                return DXWebUnitType.Point;
            }
            if (value.Equals("%"))
            {
                return DXWebUnitType.Percentage;
            }
            if (value.Equals("pc"))
            {
                return DXWebUnitType.Pica;
            }
            if (value.Equals("in"))
            {
                return DXWebUnitType.Inch;
            }
            if (value.Equals("mm"))
            {
                return DXWebUnitType.Mm;
            }
            if (value.Equals("cm"))
            {
                return DXWebUnitType.Cm;
            }
            if (value.Equals("em"))
            {
                return DXWebUnitType.Em;
            }
            if (!value.Equals("ex"))
            {
                throw new ArgumentOutOfRangeException("value");
            }
            return DXWebUnitType.Ex;
        }

        public static DXWebUnit Parse(string s) => 
            new DXWebUnit(s, CultureInfo.CurrentCulture);

        public static DXWebUnit Parse(string s, CultureInfo culture) => 
            new DXWebUnit(s, culture);

        public static DXWebUnit Percentage(double n) => 
            new DXWebUnit(n, DXWebUnitType.Percentage);

        public static DXWebUnit Pixel(int n) => 
            new DXWebUnit(n);

        public static DXWebUnit Point(int n) => 
            new DXWebUnit((double) n, DXWebUnitType.Point);

        public static implicit operator DXWebUnit(int n) => 
            Pixel(n);

        public DXWebUnit(int value)
        {
            if ((value < -32768) || (value > 0x7fff))
            {
                throw new ArgumentOutOfRangeException("value");
            }
            this.value = value;
            this.type = DXWebUnitType.Pixel;
        }

        public DXWebUnit(double value)
        {
            if ((value < -32768.0) || (value > 32767.0))
            {
                throw new ArgumentOutOfRangeException("value");
            }
            this.value = (int) value;
            this.type = DXWebUnitType.Pixel;
        }

        public DXWebUnit(double value, DXWebUnitType type)
        {
            if ((value < -32768.0) || (value > 32767.0))
            {
                throw new ArgumentOutOfRangeException("value");
            }
            this.value = (type != DXWebUnitType.Pixel) ? value : ((double) ((int) value));
            this.type = type;
        }

        public DXWebUnit(string value) : this(value, CultureInfo.CurrentCulture, DXWebUnitType.Pixel)
        {
        }

        public DXWebUnit(string value, CultureInfo culture) : this(value, culture, DXWebUnitType.Pixel)
        {
        }

        internal DXWebUnit(string value, CultureInfo culture, DXWebUnitType defaultType)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.value = 0.0;
                this.type = (DXWebUnitType) 0;
            }
            else
            {
                culture ??= CultureInfo.CurrentCulture;
                string str = value.Trim().ToLowerInvariant();
                int length = str.Length;
                int num2 = -1;
                int num3 = 0;
                while (true)
                {
                    if (num3 < length)
                    {
                        char ch = str[num3];
                        if ((((ch >= '0') && (ch <= '9')) || (ch == '-')) || ((ch == '.') || (ch == ',')))
                        {
                            num2 = num3;
                            num3++;
                            continue;
                        }
                    }
                    if (num2 == -1)
                    {
                        throw new FormatException("UnitParseNoDigits");
                    }
                    this.type = (num2 >= (length - 1)) ? defaultType : GetTypeFromString(str.Substring(num2 + 1).Trim());
                    string str2 = str.Substring(0, num2 + 1);
                    try
                    {
                        this.value = Convert.ToSingle(str2, culture);
                        if (this.type == DXWebUnitType.Pixel)
                        {
                            this.value = (int) this.value;
                        }
                    }
                    catch
                    {
                        throw new FormatException("UnitParseNumericPart");
                    }
                    if ((this.value < -32768.0) || (this.value > 32767.0))
                    {
                        throw new ArgumentOutOfRangeException("value");
                    }
                    return;
                }
            }
        }

        public bool IsEmpty =>
            this.type == ((DXWebUnitType) 0);
        public DXWebUnitType Type =>
            this.IsEmpty ? DXWebUnitType.Pixel : this.type;
        public double Value =>
            this.value;
        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<DXWebUnitType, double>(this.type, this.value);

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is DXWebUnit))
            {
                return false;
            }
            DXWebUnit unit = (DXWebUnit) obj;
            return ((unit.type == this.type) && (unit.value == this.value));
        }

        public override string ToString() => 
            this.ToString((IFormatProvider) CultureInfo.CurrentCulture);

        public string ToString(CultureInfo culture) => 
            this.ToString((IFormatProvider) culture);

        public string ToString(IFormatProvider formatProvider)
        {
            if (this.IsEmpty)
            {
                return string.Empty;
            }
            string str = (this.type != DXWebUnitType.Pixel) ? ((float) this.value).ToString(formatProvider) : ((int) this.value).ToString(formatProvider);
            return (str + GetStringFromType(this.type));
        }
    }
}

