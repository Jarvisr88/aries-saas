namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public static class SparklineMathUtils
    {
        public static double? ConvertToDouble(object value, out SparklineScaleType scaleType)
        {
            if (value == null)
            {
                scaleType = SparklineScaleType.Unknown;
                return null;
            }
            Type type = value.GetType();
            if (type == typeof(string))
            {
                double num;
                DateTime time;
                if (double.TryParse(value.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out num))
                {
                    scaleType = SparklineScaleType.Numeric;
                    return new double?(num);
                }
                if (DateTime.TryParse(value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                {
                    scaleType = SparklineScaleType.DateTime;
                    return new double?((double) time.Ticks);
                }
            }
            if (type == typeof(DateTime))
            {
                scaleType = SparklineScaleType.DateTime;
                return new double?((double) ((DateTime) value).Ticks);
            }
            if (type == typeof(TimeSpan))
            {
                scaleType = SparklineScaleType.TimeSpan;
                return new double?((double) ((TimeSpan) value).Ticks);
            }
            if (type == typeof(char))
            {
                scaleType = SparklineScaleType.Numeric;
                return new double?((double) Convert.ToInt32(value));
            }
            if ((type == typeof(double)) || ((type == typeof(float)) || ((type == typeof(int)) || ((type == typeof(uint)) || ((type == typeof(long)) || ((type == typeof(ulong)) || ((type == typeof(decimal)) || ((type == typeof(short)) || ((type == typeof(ushort)) || ((type == typeof(byte)) || (type == typeof(sbyte))))))))))))
            {
                scaleType = SparklineScaleType.Numeric;
                return new double?(Convert.ToDouble(value));
            }
            scaleType = SparklineScaleType.Unknown;
            return null;
        }

        public static object ConvertToNative(double value, SparklineScaleType scaleType)
        {
            switch (scaleType)
            {
                case SparklineScaleType.DateTime:
                    return new DateTime(Convert.ToInt64(value));

                case SparklineScaleType.TimeSpan:
                    return new TimeSpan(Convert.ToInt64(value));
            }
            return value;
        }

        public static bool IsValidDouble(double value) => 
            !double.IsNaN(value) && !double.IsInfinity(value);

        public static int Round(double value) => 
            (int) Math.Round(value);
    }
}

