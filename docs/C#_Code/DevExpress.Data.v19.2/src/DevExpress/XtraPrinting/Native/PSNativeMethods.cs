namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;

    public class PSNativeMethods
    {
        private static IAspDetector aspDetector = new AspDetector();

        public static Array CombineCollections(ICollection items1, ICollection items2, System.Type type)
        {
            Array array = Array.CreateInstance(type, (int) (items1.Count + items2.Count));
            items1.CopyTo(array, 0);
            items2.CopyTo(array, items1.Count);
            return array;
        }

        public static void ForceCreateHandle(Control control)
        {
            IntPtr handle = control.Handle;
        }

        public static SizeF GetResolutionImageSize(Image img) => 
            GetResolutionImageSize(img, 96f);

        public static SizeF GetResolutionImageSize(Image img, float dpi)
        {
            SizeF ef;
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }
            Image image = img;
            lock (image)
            {
                float safeResolution = GraphicsDpi.GetSafeResolution(img.HorizontalResolution);
                float num2 = GraphicsDpi.GetSafeResolution(img.VerticalResolution);
                ef = new SizeF(((float) img.Width) / safeResolution, ((float) img.Height) / num2);
            }
            return GraphicsUnitConverter.Convert(ef, (float) 1f, dpi);
        }

        public static bool IsFloatType(System.Type type) => 
            typeof(decimal).Equals(type) || (typeof(float).Equals(type) || typeof(double).Equals(type));

        public static bool IsIntegerType(System.Type type) => 
            typeof(short).Equals(type) || (typeof(int).Equals(type) || (typeof(long).Equals(type) || (typeof(ushort).Equals(type) || (typeof(uint).Equals(type) || (typeof(ulong).Equals(type) || (typeof(byte).Equals(type) || typeof(sbyte).Equals(type)))))));

        public static bool IsNaN(float value)
        {
            try
            {
                return float.IsNaN(value);
            }
            catch
            {
                return true;
            }
        }

        public static bool IsNullableNumericalType(System.Type type) => 
            type.IsGenericType && ((type.GetGenericTypeDefinition() == typeof(Nullable<>)) && IsNumericalType(Nullable.GetUnderlyingType(type)));

        public static bool IsNumericalType(System.Type type) => 
            IsIntegerType(type) || IsFloatType(type);

        public static void SetAspDetector(IAspDetector detector)
        {
            Guard.ArgumentNotNull(detector, "detector");
            aspDetector = detector;
        }

        public static PointF TranslatePointF(PointF val, PointF pos) => 
            new PointF(val.X + pos.X, val.Y + pos.Y);

        public static PointF TranslatePointF(PointF val, float dx, float dy) => 
            new PointF(val.X + dx, val.Y + dy);

        public static Color ValidateBackgrColor(Color color) => 
            ((color == DXSystemColors.Window) || DXColor.IsTransparentColor(color)) ? DXColor.White : (!DXColor.IsSemitransparentColor(color) ? color : DXColor.Blend(color, Color.White));

        public static bool ValueInsideBounds(float value, float lowBound, float highBound) => 
            !FloatsComparer.Default.FirstLessSecond((double) value, (double) lowBound) && FloatsComparer.Default.FirstLessSecond((double) value, (double) highBound);

        public static bool HasHttpContext
        {
            get
            {
                AspDetector aspDetector = PSNativeMethods.aspDetector as AspDetector;
                return ((aspDetector == null) ? PSNativeMethods.aspDetector.AspIsRunning : aspDetector.HasHttpContext);
            }
        }

        public static bool AspIsRunning =>
            aspDetector.AspIsRunning;

        private class AspDetector : PSNativeMethods.IAspDetector
        {
            public bool HasHttpContext =>
                HttpContextAccessor.Current != null;

            public bool AspIsRunning =>
                !Environment.UserInteractive || this.HasHttpContext;
        }

        public interface IAspDetector
        {
            bool AspIsRunning { get; }
        }
    }
}

