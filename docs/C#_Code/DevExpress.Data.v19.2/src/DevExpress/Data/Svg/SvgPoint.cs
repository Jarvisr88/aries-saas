namespace DevExpress.Data.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SvgPoint
    {
        private const int MaxFloatPrecision = 7;
        private static string FloatStringFormat;
        public static readonly SvgPoint Empty;
        public static readonly SvgPoint Invalid;
        private double x;
        private double y;
        public static bool operator ==(SvgPoint Point1, SvgPoint Point2);
        public static bool operator !=(SvgPoint Point1, SvgPoint Point2);
        public static SvgPoint operator +(SvgPoint Point1, SvgPoint Point2);
        public static SvgPoint operator -(SvgPoint Point1, SvgPoint Point2);
        public static bool Equals(SvgPoint Point1, SvgPoint Point2);
        public bool IsEmpty { get; }
        public bool IsValid { get; }
        public double X { get; }
        public double Y { get; }
        public SvgPoint(double x, double y);
        private static string GetSeparatorByProvider(IFormatProvider provider);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public override string ToString();
        public string ToString(IFormatProvider provider);
        public string ToString(IFormatProvider provider, string pointsSeparator);
        public string ToStringX();
        public string ToStringY();
        public string ToStringX(IFormatProvider provider);
        public string ToStringY(IFormatProvider provider);
        public void ParseX(string point);
        public void ParseY(string point);
        public void ParseX(string point, IFormatProvider provider);
        public void ParseY(string point, IFormatProvider provider);
        public void Offset(double offsetX, double offsetY);
        public void OffsetX(double offset);
        public void OffsetY(double offset);
        public static SvgPoint Parse(string pointString, IFormatProvider provider);
        public static SvgPoint Parse(string pointString, IFormatProvider provider, string pointsSeparator);
        public static SvgPoint Parse(string pointString, IFormatProvider provider, string[] separatorsList);
        public static SvgPoint Parse(string value1, string value2);
        public static SvgPoint Parse(string value1, string value2, IFormatProvider provider);
        static SvgPoint();
    }
}

