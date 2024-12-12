namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    internal class BackgroundCalculator
    {
        private static System.Drawing.Color[][] colorTable;

        static BackgroundCalculator()
        {
            System.Drawing.Color[] colorArray1 = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0x99, 0xff), System.Drawing.Color.FromArgb(9, 0x4a, 0xb2) };
            System.Drawing.Color[][] colorArrayArray1 = new System.Drawing.Color[0x38][];
            colorArrayArray1[0] = colorArray1;
            colorArrayArray1[1] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0, 0x4c), System.Drawing.Color.FromArgb(140, 0, 0x95) };
            colorArrayArray1[2] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0, 0x99), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[3] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0, 0), System.Drawing.Color.FromArgb(0x7b, 0, 0) };
            colorArrayArray1[4] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0, 0xff), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[5] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0xff, 0x4c), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorArrayArray1[6] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0xff, 0x99), System.Drawing.Color.FromArgb(0x12, 0x80, 0x23) };
            colorArrayArray1[7] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0xff, 0), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorArrayArray1[8] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0xff, 0xff), System.Drawing.Color.FromArgb(0, 130, 0x99) };
            colorArrayArray1[9] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0x4c, 0x4c), System.Drawing.Color.FromArgb(0x7b, 0, 0) };
            colorArrayArray1[10] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0x4c, 0x99), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[11] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0x4c, 0x99), System.Drawing.Color.FromArgb(140, 0, 0x95) };
            colorArrayArray1[12] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0x4c, 0), System.Drawing.Color.FromArgb(210, 0x47, 0x26) };
            colorArrayArray1[13] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0x4c, 0xff), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[14] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0x99, 0xff), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[15] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0, 0x4c), System.Drawing.Color.FromArgb(0xac, 0x19, 0x3d) };
            colorArrayArray1[0x10] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0, 0x99), System.Drawing.Color.FromArgb(140, 0, 0x95) };
            colorArrayArray1[0x11] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0, 0), System.Drawing.Color.FromArgb(0x7b, 0, 0) };
            colorArrayArray1[0x12] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0, 0xff), System.Drawing.Color.FromArgb(140, 0, 0x95) };
            colorArrayArray1[0x13] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0xff, 0x4c), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorArrayArray1[20] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0xff, 0x99), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorArrayArray1[0x15] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x99, 0xff, 0xff), System.Drawing.Color.FromArgb(0, 130, 0x99) };
            colorArrayArray1[0x16] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0x4c, 0x4c), System.Drawing.Color.FromArgb(0, 130, 0x99) };
            colorArrayArray1[0x17] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0x4c, 0x99), System.Drawing.Color.FromArgb(9, 0x4a, 0xb2) };
            colorArrayArray1[0x18] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0x4c, 0), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorArrayArray1[0x19] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0x4c, 0xff), System.Drawing.Color.FromArgb(9, 0x4a, 0xb2) };
            colorArrayArray1[0x1a] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0x99, 0x4c), System.Drawing.Color.FromArgb(0x12, 0x80, 0x23) };
            colorArrayArray1[0x1b] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0x4c, 0xff), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[0x1c] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0x99, 0x99), System.Drawing.Color.FromArgb(0, 130, 0x99) };
            colorArrayArray1[0x1d] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0x99, 0), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorArrayArray1[30] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0x99, 0xff), System.Drawing.Color.FromArgb(9, 0x4a, 0xb2) };
            colorArrayArray1[0x1f] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0, 0x4c), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[0x20] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0, 0x99), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[0x21] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0, 0xff), System.Drawing.Color.FromArgb(0x51, 0x33, 0xab) };
            colorArrayArray1[0x22] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0xff, 0x4c), System.Drawing.Color.FromArgb(0x12, 0x80, 0x23) };
            colorArrayArray1[0x23] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0xff, 0x99), System.Drawing.Color.FromArgb(0x12, 0x80, 0x23) };
            colorArrayArray1[0x24] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0xff, 0), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorArrayArray1[0x25] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 0xff, 0xff), System.Drawing.Color.FromArgb(0, 130, 0x99) };
            colorArrayArray1[0x26] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0x4c, 0x4c), System.Drawing.Color.FromArgb(0x7b, 0, 0) };
            colorArrayArray1[0x27] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0x4c, 0x99), System.Drawing.Color.FromArgb(0xac, 0x19, 0x3d) };
            colorArrayArray1[40] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0x4c, 0), System.Drawing.Color.FromArgb(210, 0x47, 0x26) };
            colorArrayArray1[0x29] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0x4c, 0xff), System.Drawing.Color.FromArgb(140, 0, 0x95) };
            colorArrayArray1[0x2a] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0x99, 0x4c), System.Drawing.Color.FromArgb(210, 0x47, 0x26) };
            colorArrayArray1[0x2b] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0x99, 0x99), System.Drawing.Color.FromArgb(0x7b, 0, 0) };
            colorArrayArray1[0x2c] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0x99, 0), System.Drawing.Color.FromArgb(210, 0x47, 0x26) };
            colorArrayArray1[0x2d] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0x99, 0xff), System.Drawing.Color.FromArgb(140, 0, 0x95) };
            colorArrayArray1[0x2e] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0, 0x4c), System.Drawing.Color.FromArgb(0xac, 0x19, 0x3d) };
            colorArrayArray1[0x2f] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0, 0x99), System.Drawing.Color.FromArgb(0xac, 0x19, 0x3d) };
            colorArrayArray1[0x30] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0, 0), System.Drawing.Color.FromArgb(0x7b, 0, 0) };
            colorArrayArray1[0x31] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0, 0xff), System.Drawing.Color.FromArgb(140, 0, 0x95) };
            colorArrayArray1[50] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0xff, 0x4c), System.Drawing.Color.FromArgb(0x59, 0x59, 0x59) };
            colorArrayArray1[0x33] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0xff, 0x99), System.Drawing.Color.FromArgb(0x59, 0x59, 0x59) };
            colorArrayArray1[0x34] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0xff, 0xff, 0), System.Drawing.Color.FromArgb(0x59, 0x59, 0x59) };
            colorArrayArray1[0x35] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0x99, 0x4c), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorArrayArray1[0x36] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0x99, 0x99), System.Drawing.Color.FromArgb(0, 130, 0x99) };
            colorArrayArray1[0x37] = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0x4c, 0x99, 0), System.Drawing.Color.FromArgb(0, 0x8a, 0) };
            colorTable = colorArrayArray1;
        }

        [IteratorStateMachine(typeof(<EveryPixel>d__4))]
        private static IEnumerable<System.Drawing.Color> EveryPixel(Bitmap bmp)
        {
            <EveryPixel>d__4 d__1 = new <EveryPixel>d__4(-2);
            d__1.<>3__bmp = bmp;
            return d__1;
        }

        internal static System.Windows.Media.Color GetBestMatch(Bitmap bmp)
        {
            Dictionary<System.Drawing.Color, int> source = new Dictionary<System.Drawing.Color, int>();
            foreach (System.Drawing.Color[] colorArray2 in colorTable)
            {
                source[colorArray2[0]] = 0;
            }
            int count = 0;
            foreach (System.Drawing.Color color2 in EveryPixel(bmp))
            {
                int num4 = count;
                count = num4 + 1;
                double num3 = ((double) color2.A) / 255.0;
                System.Drawing.Color withAlpha = System.Drawing.Color.FromArgb((int) (color2.R * num3), (int) (color2.G * num3), (int) (color2.B * num3));
                if (RgbDistance(RemoveGray(withAlpha), System.Drawing.Color.FromArgb(0, 0, 0)) >= 10.0)
                {
                    var func1 = <>c.<>9__6_1;
                    if (<>c.<>9__6_1 == null)
                    {
                        var local1 = <>c.<>9__6_1;
                        func1 = <>c.<>9__6_1 = i => i.d;
                    }
                    var list2 = (from c in source.Keys select new { 
                        d = LabDistance(c, withAlpha),
                        c = c
                    }).OrderBy(func1).ToList();
                    System.Drawing.Color color4 = (from c in source.Keys
                        orderby LabDistance(c, withAlpha)
                        select c).First<System.Drawing.Color>();
                    source[color4] += 1;
                }
            }
            if (!source.Values.Any<int>(v => (v > (count * 0.05))))
            {
                return DefaultGrayColor;
            }
            Func<KeyValuePair<System.Drawing.Color, int>, int> keySelector = <>c.<>9__6_4;
            if (<>c.<>9__6_4 == null)
            {
                Func<KeyValuePair<System.Drawing.Color, int>, int> local2 = <>c.<>9__6_4;
                keySelector = <>c.<>9__6_4 = p => p.Value;
            }
            List<KeyValuePair<System.Drawing.Color, int>> test = source.OrderByDescending<KeyValuePair<System.Drawing.Color, int>, int>(keySelector).ToList<KeyValuePair<System.Drawing.Color, int>>();
            List<double> list = (from v in test select ((double) v.Value) / ((double) test.Count<KeyValuePair<System.Drawing.Color, int>>())).ToList<double>();
            double num = list[0] - list[1];
            Func<KeyValuePair<System.Drawing.Color, int>, int> func3 = <>c.<>9__6_6;
            if (<>c.<>9__6_6 == null)
            {
                Func<KeyValuePair<System.Drawing.Color, int>, int> local3 = <>c.<>9__6_6;
                func3 = <>c.<>9__6_6 = p => p.Value;
            }
            System.Drawing.Color left = source.OrderBy<KeyValuePair<System.Drawing.Color, int>, int>(func3).Last<KeyValuePair<System.Drawing.Color, int>>().Key;
            System.Drawing.Color color = colorTable.First<System.Drawing.Color[]>(p => (p[0] == left))[1];
            return System.Windows.Media.Color.FromRgb(color.R, color.G, color.B);
        }

        private static double LabDistance(System.Drawing.Color rgb1, System.Drawing.Color rgb2)
        {
            Lab item = new Lab();
            Lab lab2 = new Lab();
            Rgb color = new Rgb();
            color.R = rgb1.R;
            color.G = rgb1.G;
            color.B = rgb1.B;
            LabConverter.ToColorSpace(color, item);
            Rgb rgb4 = new Rgb();
            rgb4.R = rgb2.R;
            rgb4.G = rgb2.G;
            rgb4.B = rgb2.B;
            LabConverter.ToColorSpace(rgb4, lab2);
            return ((Math.Pow(lab2.L - item.L, 2.0) + Math.Pow(lab2.A - item.A, 2.0)) + Math.Pow(lab2.B - item.B, 2.0));
        }

        private static System.Drawing.Color Normalize(int r, int g, int b)
        {
            int num = Math.Max(Math.Max(r, g), b);
            return System.Drawing.Color.FromArgb((0xff * r) / num, (0xff * g) / num, (0xff * b) / num);
        }

        private static System.Drawing.Color RemoveGray(System.Drawing.Color color)
        {
            int num = Math.Min(Math.Min(color.R, color.G), color.B);
            return System.Drawing.Color.FromArgb(color.R - num, color.G - num, color.B - num);
        }

        private static double RgbDistance(System.Drawing.Color rgb1, System.Drawing.Color rgb2) => 
            (Math.Pow((double) (rgb2.R - rgb1.R), 2.0) + Math.Pow((double) (rgb2.G - rgb1.G), 2.0)) + Math.Pow((double) (rgb2.B - rgb1.B), 2.0);

        public static System.Windows.Media.Color DefaultGrayColor =>
            System.Windows.Media.Color.FromRgb(0x59, 0x59, 0x59);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BackgroundCalculator.<>c <>9 = new BackgroundCalculator.<>c();
            public static Func<<>f__AnonymousType9<double, System.Drawing.Color>, double> <>9__6_1;
            public static Func<KeyValuePair<System.Drawing.Color, int>, int> <>9__6_4;
            public static Func<KeyValuePair<System.Drawing.Color, int>, int> <>9__6_6;

            internal double <GetBestMatch>b__6_1(<>f__AnonymousType9<double, System.Drawing.Color> i) => 
                i.d;

            internal int <GetBestMatch>b__6_4(KeyValuePair<System.Drawing.Color, int> p) => 
                p.Value;

            internal int <GetBestMatch>b__6_6(KeyValuePair<System.Drawing.Color, int> p) => 
                p.Value;
        }

        [CompilerGenerated]
        private sealed class <EveryPixel>d__4 : IEnumerable<System.Drawing.Color>, IEnumerable, IEnumerator<System.Drawing.Color>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private System.Drawing.Color <>2__current;
            private int <>l__initialThreadId;
            private Bitmap bmp;
            public Bitmap <>3__bmp;
            private int <i>5__1;
            private int <j>5__2;

            [DebuggerHidden]
            public <EveryPixel>d__4(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num2;
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<i>5__1 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    num2 = this.<j>5__2 + 1;
                    this.<j>5__2 = num2;
                    goto TR_0007;
                }
            TR_0003:
                if (this.<i>5__1 >= this.bmp.Width)
                {
                    return false;
                }
                this.<j>5__2 = 0;
            TR_0007:
                while (true)
                {
                    if (this.<j>5__2 >= this.bmp.Height)
                    {
                        num2 = this.<i>5__1 + 1;
                        this.<i>5__1 = num2;
                        break;
                    }
                    this.<>2__current = this.bmp.GetPixel(this.<i>5__1, this.<j>5__2);
                    this.<>1__state = 1;
                    return true;
                }
                goto TR_0003;
            }

            [DebuggerHidden]
            IEnumerator<System.Drawing.Color> IEnumerable<System.Drawing.Color>.GetEnumerator()
            {
                BackgroundCalculator.<EveryPixel>d__4 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new BackgroundCalculator.<EveryPixel>d__4(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.bmp = this.<>3__bmp;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Drawing.Color>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            System.Drawing.Color IEnumerator<System.Drawing.Color>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private class Lab
        {
            public double L;
            public double A;
            public double B;
        }

        private static class LabConverter
        {
            private static double CubicRoot(double n) => 
                Math.Pow(n, 0.33333333333333331);

            private static double PivotXyz(double n) => 
                (n > 0.008856) ? CubicRoot(n) : (((903.3 * n) + 16.0) / 116.0);

            internal static BackgroundCalculator.Rgb ToColor(BackgroundCalculator.Lab item)
            {
                double num = (item.L + 16.0) / 116.0;
                double num2 = (item.A / 500.0) + num;
                double num3 = num - (item.B / 200.0);
                BackgroundCalculator.Xyz whiteReference = BackgroundCalculator.XyzConverter.WhiteReference;
                double num4 = (num2 * num2) * num2;
                double num5 = (num3 * num3) * num3;
                BackgroundCalculator.Xyz xyz1 = new BackgroundCalculator.Xyz();
                BackgroundCalculator.Xyz xyz3 = new BackgroundCalculator.Xyz();
                xyz3.X = whiteReference.X * ((num4 > 0.008856) ? num4 : ((num2 - 0.13793103448275862) / 7.787));
                BackgroundCalculator.Xyz local3 = xyz3;
                BackgroundCalculator.Xyz local4 = xyz3;
                local4.Y = whiteReference.Y * ((item.L > 7.9996247999999985) ? Math.Pow((item.L + 16.0) / 116.0, 3.0) : (item.L / 903.3));
                BackgroundCalculator.Xyz local1 = local4;
                BackgroundCalculator.Xyz local2 = local4;
                local2.Z = whiteReference.Z * ((num5 > 0.008856) ? num5 : ((num3 - 0.13793103448275862) / 7.787));
                return local2.ToRgb();
            }

            internal static void ToColorSpace(BackgroundCalculator.Rgb color, BackgroundCalculator.Lab item)
            {
                BackgroundCalculator.Xyz xyz = new BackgroundCalculator.Xyz();
                xyz.Initialize(color);
                BackgroundCalculator.Xyz whiteReference = BackgroundCalculator.XyzConverter.WhiteReference;
                double num = PivotXyz(xyz.X / whiteReference.X);
                double num2 = PivotXyz(xyz.Y / whiteReference.Y);
                item.L = Math.Max((double) 0.0, (double) ((116.0 * num2) - 16.0));
                item.A = 500.0 * (num - num2);
                item.B = 200.0 * (num2 - PivotXyz(xyz.Z / whiteReference.Z));
            }
        }

        private class Rgb
        {
            public void Initialize(BackgroundCalculator.Rgb color)
            {
                BackgroundCalculator.RgbConverter.ToColorSpace(color, this);
            }

            public BackgroundCalculator.Rgb ToRgb() => 
                BackgroundCalculator.RgbConverter.ToColor(this);

            public double R { get; set; }

            public double G { get; set; }

            public double B { get; set; }
        }

        private static class RgbConverter
        {
            internal static BackgroundCalculator.Rgb ToColor(BackgroundCalculator.Rgb item) => 
                item;

            internal static void ToColorSpace(BackgroundCalculator.Rgb color, BackgroundCalculator.Rgb item)
            {
                item.R = color.R;
                item.G = color.G;
                item.B = color.B;
            }
        }

        private class Xyz
        {
            public void Initialize(BackgroundCalculator.Rgb color)
            {
                BackgroundCalculator.XyzConverter.ToColorSpace(color, this);
            }

            public BackgroundCalculator.Rgb ToRgb() => 
                BackgroundCalculator.XyzConverter.ToColor(this);

            public double X { get; set; }

            public double Y { get; set; }

            public double Z { get; set; }
        }

        private static class XyzConverter
        {
            internal const double Epsilon = 0.008856;
            internal const double Kappa = 903.3;

            static XyzConverter()
            {
                BackgroundCalculator.Xyz xyz1 = new BackgroundCalculator.Xyz();
                xyz1.X = 95.047;
                xyz1.Y = 100.0;
                xyz1.Z = 108.883;
                WhiteReference = xyz1;
            }

            internal static double CubicRoot(double n) => 
                Math.Pow(n, 0.33333333333333331);

            private static double PivotRgb(double n) => 
                ((n > 0.04045) ? Math.Pow((n + 0.055) / 1.055, 2.4) : (n / 12.92)) * 100.0;

            internal static BackgroundCalculator.Rgb ToColor(BackgroundCalculator.Xyz item)
            {
                double num = item.X / 100.0;
                double num2 = item.Y / 100.0;
                double num3 = item.Z / 100.0;
                double x = ((num * 3.2406) + (num2 * -1.5372)) + (num3 * -0.4986);
                double num5 = ((num * -0.9689) + (num2 * 1.8758)) + (num3 * 0.0415);
                double num6 = ((num * 0.0557) + (num2 * -0.204)) + (num3 * 1.057);
                x = (x > 0.0031308) ? ((1.055 * Math.Pow(x, 0.41666666666666669)) - 0.055) : (12.92 * x);
                num5 = (num5 > 0.0031308) ? ((1.055 * Math.Pow(num5, 0.41666666666666669)) - 0.055) : (12.92 * num5);
                num6 = (num6 > 0.0031308) ? ((1.055 * Math.Pow(num6, 0.41666666666666669)) - 0.055) : (12.92 * num6);
                BackgroundCalculator.Rgb rgb1 = new BackgroundCalculator.Rgb();
                rgb1.R = ToRgb(x);
                rgb1.G = ToRgb(num5);
                rgb1.B = ToRgb(num6);
                return rgb1;
            }

            internal static void ToColorSpace(BackgroundCalculator.Rgb color, BackgroundCalculator.Xyz item)
            {
                double num = PivotRgb(color.R / 255.0);
                double num2 = PivotRgb(color.G / 255.0);
                double num3 = PivotRgb(color.B / 255.0);
                item.X = ((num * 0.4124) + (num2 * 0.3576)) + (num3 * 0.1805);
                item.Y = ((num * 0.2126) + (num2 * 0.7152)) + (num3 * 0.0722);
                item.Z = ((num * 0.0193) + (num2 * 0.1192)) + (num3 * 0.9505);
            }

            private static double ToRgb(double n)
            {
                double num = 255.0 * n;
                return ((num >= 0.0) ? ((num <= 255.0) ? num : 255.0) : 0.0);
            }

            internal static BackgroundCalculator.Xyz WhiteReference { get; private set; }
        }
    }
}

