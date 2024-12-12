namespace DevExpress.Utils.Serializing
{
    using DevExpress.Emf;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class PrintingSystemXmlSerializer : CompactXmlSerializer
    {
        private const string Name = "PreviewSerializer";
        public static readonly ObjectConverterImplementation ObjectConverterInstance = new PrintingSystemObjectConverterImplementation();

        static PrintingSystemXmlSerializer()
        {
            RegisterConverter(PointConverter.Instance);
            RegisterConverter(SizeConverter.Instance);
            RegisterConverter(RectangleConverter.Instance);
            RegisterConverter(PointFConverter.Instance);
            RegisterConverter(SizeFConverter.Instance);
            RegisterConverter(RectangleFConverter.Instance);
            RegisterConverter(ImageConverter.Instance);
            RegisterConverter(BitmapConverter.Instance);
            RegisterConverter(MetafileConverter.Instance);
            RegisterConverter(EmfMetafileConverter.Instance);
            RegisterConverter(DBNullConverter.Instance);
            RegisterConverter(ColorConverter.Instance);
            RegisterConverter(ImageSizeModeConverter.Instance);
            RegisterConverter(DateTimeConverter.Instance);
            RegisterConverter(GuidConverter.Instance);
            RegisterConverter(FontConverter.Instance);
            RegisterConverter(MarginsConverter.Instance);
        }

        protected override XmlWriterSettings CreateXmlWriterSettings()
        {
            XmlWriterSettings settings = base.CreateXmlWriterSettings();
            settings.OmitXmlDeclaration = false;
            return settings;
        }

        protected override IXtraPropertyCollection DeserializeCore(Stream stream, string appName, IList objects) => 
            new DeserializationVirtualXtraPropertyCollection(this.CreateReader(stream), this);

        internal XtraPropertyInfo DeserializeObject(Stream stream)
        {
            XmlReader tr = this.CreateReader(stream);
            return this.ReadInfo(tr, false);
        }

        protected override ObjectConverterImplementation GetDefaultObjectConverterImplementation()
        {
            ObjectConverterImplementation ocImplTo = new ObjectConverterImplementation();
            ObjectConverterInstance.CopyConvertersTo(ocImplTo);
            ObjectConverter.Instance.CopyConvertersTo(ocImplTo);
            return ocImplTo;
        }

        protected override string ObjToString(object val) => 
            base.ObjectConverterImpl.ObjectToString(val);

        internal XtraPropertyInfo ReadInfo(XmlReader tr) => 
            base.ReadInfoCore(tr);

        internal XtraPropertyInfo ReadInfo(XmlReader tr, bool skipZeroDepth)
        {
            tr.MoveToContent();
            return base.ReadInfoCore(tr, skipZeroDepth);
        }

        public static void RegisterConverter(IOneTypeObjectConverter converter)
        {
            ObjectConverterInstance.RegisterConverter(converter);
        }

        internal void SerializeObject(Stream stream, XtraPropertyInfo p)
        {
            using (XmlWriter writer = base.CreateXmlTextWriter(stream))
            {
                this.SerializeContentPropertyCore(writer, p);
                writer.Flush();
            }
        }

        public static void UnregisterConverter(Type type)
        {
            ObjectConverterInstance.UnregisterConverter(type);
        }

        protected override string SerializerName =>
            "PreviewSerializer";

        private class BitmapConverter : PrintingSystemXmlSerializer.ImageConverter
        {
            public static readonly PrintingSystemXmlSerializer.BitmapConverter Instance = new PrintingSystemXmlSerializer.BitmapConverter();

            private BitmapConverter()
            {
            }

            public override System.Type Type =>
                typeof(Bitmap);
        }

        public class ColorConverter : StructStringConverter
        {
            private static readonly Dictionary<int, KeyValuePair<string, Color>> ArgbToPair;
            private static readonly Dictionary<string, Color> PredefinedColors;
            public static readonly PrintingSystemXmlSerializer.ColorConverter Instance = new PrintingSystemXmlSerializer.ColorConverter();
            private const string HexPrefix = "0x";

            static ColorConverter()
            {
                IEnumerable<Color> source = from p in typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static)
                    where p.PropertyType == typeof(Color)
                    select (Color) p.GetValue(null);
                Dictionary<int, KeyValuePair<string, Color>> dictionary = new Dictionary<int, KeyValuePair<string, Color>>(source.Count<Color>());
                Dictionary<string, Color> dictionary2 = new Dictionary<string, Color>(source.Count<Color>());
                foreach (Color color in source)
                {
                    int key = color.ToArgb();
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, new KeyValuePair<string, Color>(color.Name, color));
                    }
                    dictionary2.Add(color.Name, color);
                }
                ArgbToPair = dictionary;
                PredefinedColors = dictionary2;
            }

            private static string ByteToHex(byte value) => 
                value.ToString();

            protected override object CreateObject(string[] values)
            {
                Color color;
                if (values.Length == 4)
                {
                    return GetColor(NumToByte(values[0]), NumToByte(values[1]), NumToByte(values[2]), NumToByte(values[3]));
                }
                if (values.Length == 3)
                {
                    return GetColor(NumToByte(values[0]), NumToByte(values[1]), NumToByte(values[2]));
                }
                if (values.Length != 1)
                {
                    throw new NotSupportedException();
                }
                string key = values[0];
                if (PredefinedColors.TryGetValue(key, out color))
                {
                    return color;
                }
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Color));
                if (converter == null)
                {
                    throw new NotSupportedException();
                }
                return converter.ConvertFromString(key);
            }

            private static Color GetColor(byte r, byte g, byte b) => 
                GetColor(0xff, r, g, b);

            private static Color GetColor(byte a, byte r, byte g, byte b)
            {
                Color color = DXColor.FromArgb(a, r, g, b);
                TryFindPredefinedColor(ref color);
                return color;
            }

            protected override string[] GetValues(object obj)
            {
                string str;
                Color color = (Color) obj;
                if (color.IsNamedColor && PredefinedColors.ContainsKey(color.Name))
                {
                    return new string[] { color.Name };
                }
                if (TryFindPredefinedColorName(color, out str))
                {
                    return new string[] { str };
                }
                return new string[] { ByteToHex(color.A), ByteToHex(color.R), ByteToHex(color.G), ByteToHex(color.B) };
            }

            private static byte NumToByte(string value)
            {
                int fromBase = 10;
                if (value.StartsWith("0x"))
                {
                    fromBase = 0x10;
                    value = value.Remove(0, "0x".Length);
                }
                return Convert.ToByte(value.Trim(), fromBase);
            }

            private static bool TryFindPredefinedColor(ref Color color)
            {
                KeyValuePair<string, Color> result = new KeyValuePair<string, Color>();
                bool flag = TryFindPredefinedColorAndName(color, out result);
                if (flag)
                {
                    color = result.Value;
                }
                return flag;
            }

            private static bool TryFindPredefinedColorAndName(Color color, out KeyValuePair<string, Color> result)
            {
                int key = color.ToArgb();
                return ArgbToPair.TryGetValue(key, out result);
            }

            private static bool TryFindPredefinedColorName(Color color, out string name)
            {
                name = null;
                KeyValuePair<string, Color> result = new KeyValuePair<string, Color>();
                bool flag = TryFindPredefinedColorAndName(color, out result);
                if (flag)
                {
                    name = result.Key;
                }
                return flag;
            }

            public override System.Type Type =>
                typeof(Color);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly PrintingSystemXmlSerializer.ColorConverter.<>c <>9 = new PrintingSystemXmlSerializer.ColorConverter.<>c();

                internal bool <.cctor>b__2_0(PropertyInfo p) => 
                    p.PropertyType == typeof(Color);

                internal Color <.cctor>b__2_1(PropertyInfo p) => 
                    (Color) p.GetValue(null);
            }
        }

        private class DateTimeConverter : IOneTypeObjectConverter
        {
            public static readonly IOneTypeObjectConverter Instance = new PrintingSystemXmlSerializer.DateTimeConverter();

            public object FromString(string str)
            {
                string s = str.Trim();
                if (s.Length == 0)
                {
                    return DateTime.MinValue;
                }
                CultureInfo invariantCulture = CultureInfo.InvariantCulture;
                DateTimeFormatInfo format = (DateTimeFormatInfo) invariantCulture.GetFormat(typeof(DateTimeFormatInfo));
                return ((format != null) ? DateTime.Parse(s, format) : DateTime.Parse(s, invariantCulture));
            }

            public string ToString(object obj)
            {
                DateTime time = (DateTime) obj;
                if (time == DateTime.MinValue)
                {
                    return string.Empty;
                }
                CultureInfo invariantCulture = CultureInfo.InvariantCulture;
                return ((time.TimeOfDay.TotalSeconds == 0.0) ? time.ToString("yyyy-MM-dd", invariantCulture) : time.ToString(invariantCulture));
            }

            public System.Type Type =>
                typeof(DateTime);
        }

        private class DBNullConverter : IOneTypeObjectConverter
        {
            public static readonly PrintingSystemXmlSerializer.DBNullConverter Instance = new PrintingSystemXmlSerializer.DBNullConverter();

            private DBNullConverter()
            {
            }

            public object FromString(string str) => 
                DBNull.Value;

            public string ToString(object obj) => 
                "Null";

            public System.Type Type =>
                typeof(DBNull);
        }

        private class EmfMetafileConverter : IOneTypeObjectConverter
        {
            public static readonly PrintingSystemXmlSerializer.EmfMetafileConverter Instance = new PrintingSystemXmlSerializer.EmfMetafileConverter();

            private EmfMetafileConverter()
            {
            }

            public object FromString(string str) => 
                EmfMetafile.Create(Convert.FromBase64String(str));

            public string ToString(object obj)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    new EmfPlusWriter((EmfMetafile) obj).Write(stream);
                    return Convert.ToBase64String(stream.GetBuffer(), 0, (int) stream.Length);
                }
            }

            public virtual System.Type Type =>
                typeof(EmfMetafile);
        }

        public class FontConverter : StructStringConverter
        {
            public static readonly PrintingSystemXmlSerializer.FontConverter Instance = new PrintingSystemXmlSerializer.FontConverter();

            private Font CreateFontFromStringManually(string[] values)
            {
                string str;
                GraphicsUnit point;
                Dictionary<string, string> dictionary = this.PrepareFontProperties(values);
                if (!dictionary.TryGetValue("Units", out str) || !TryGetUnitName(str, out point))
                {
                    point = GraphicsUnit.Point;
                }
                FontStyle regular = FontStyle.Regular;
                if (dictionary.TryGetValue("Style", out str))
                {
                    regular = (FontStyle) Enum.Parse(typeof(FontStyle), str);
                }
                byte gdiCharSet = 1;
                if (dictionary.TryGetValue("GdiCharSet", out str) || dictionary.TryGetValue("charSet", out str))
                {
                    gdiCharSet = (byte) Convert.ToInt32(str, CultureInfo.InvariantCulture);
                }
                bool gdiVerticalFont = false;
                if (dictionary.TryGetValue("GdiVerticalFont", out str))
                {
                    gdiVerticalFont = Convert.ToBoolean(str);
                }
                return new Font(new FontFamily(dictionary["Name"]), Convert.ToSingle(dictionary["Size"], CultureInfo.InvariantCulture), regular, point, gdiCharSet, gdiVerticalFont);
            }

            protected override object CreateObject(string[] values)
            {
                string str = (values.Length != 0) ? values[values.Length - 1] : null;
                if (str != null)
                {
                    char[] trimChars = new char[] { ' ' };
                    str = str.TrimStart(trimChars);
                }
                bool flag = (str != null) && str.StartsWith("charSet=", StringComparison.Ordinal);
                string str2 = string.Join(",", values, 0, flag ? (values.Length - 1) : values.Length);
                TypeConverter fontTypeConverter = GetFontTypeConverter();
                if (!fontTypeConverter.CanConvertFrom(typeof(string)))
                {
                    return this.CreateFontFromStringManually(values);
                }
                Font font = (Font) fontTypeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, str2);
                if (!flag)
                {
                    return font;
                }
                using (font)
                {
                    return new Font(font.FontFamily, font.Size, font.Style, font.Unit, this.GetCharSet(str));
                }
            }

            private byte GetCharSet(string value)
            {
                int startIndex = value.IndexOf("=") + 1;
                return Convert.ToByte(value.Substring(startIndex, value.Length - startIndex));
            }

            internal static TypeConverter GetFontTypeConverter() => 
                TypeDescriptor.GetConverter(typeof(Font));

            protected override string[] GetValues(object obj)
            {
                string str = (string) GetFontTypeConverter().ConvertTo(null, CultureInfo.InvariantCulture, obj, typeof(string));
                int gdiCharSet = ((Font) obj).GdiCharSet;
                string[] textArray1 = new string[] { (gdiCharSet == 1) ? str : (str + ", charSet=" + gdiCharSet) };
                return new string[] { ((gdiCharSet == 1) ? str : (str + ", charSet=" + gdiCharSet)) };
            }

            private static Tuple<string, string> ParseSizeTokens(string text)
            {
                string str = null;
                string str2 = null;
                text = text.Trim();
                int length = text.Length;
                if (length > 0)
                {
                    int num2 = 0;
                    while (true)
                    {
                        if ((num2 >= length) || char.IsLetter(text[num2]))
                        {
                            char ch = CultureInfo.InvariantCulture.TextInfo.ListSeparator[0];
                            char[] trimChars = new char[] { ch, ' ' };
                            if (num2 > 0)
                            {
                                str = text.Substring(0, num2).Trim(trimChars);
                            }
                            if (num2 < length)
                            {
                                str2 = text.Substring(num2).TrimEnd(trimChars);
                            }
                            break;
                        }
                        num2++;
                    }
                }
                string text1 = str2;
                if (str2 == null)
                {
                    string local1 = str2;
                    text1 = "pt";
                }
                return Tuple.Create<string, string>(str, text1);
            }

            private Dictionary<string, string> PrepareFontProperties(string[] values)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                int num = 0;
                foreach (string str in values)
                {
                    char[] trimChars = new char[] { '[', ']' };
                    string text = str.Trim(trimChars);
                    if (text.StartsWith("Font: "))
                    {
                        text = text.Substring("Font: ".Length);
                    }
                    char[] separator = new char[] { '=' };
                    string[] strArray2 = text.Split(separator);
                    if ((strArray2 != null) && (strArray2.Length == 2))
                    {
                        dictionary.Add(strArray2[0].Trim(), strArray2[1].Trim());
                    }
                    else if (num == 0)
                    {
                        dictionary["Name"] = text;
                    }
                    else if (num == 1)
                    {
                        Tuple<string, string> tuple = ParseSizeTokens(text);
                        dictionary["Size"] = tuple.Item1;
                        dictionary["Units"] = tuple.Item2;
                    }
                    num++;
                }
                return dictionary;
            }

            private static bool TryGetUnitName(string value, out GraphicsUnit unit)
            {
                int result = -1;
                if (int.TryParse(value, out result))
                {
                    unit = (GraphicsUnit) result;
                    return true;
                }
                foreach (DevExpress.XtraPrinting.Native.UnitName name in DevExpress.XtraPrinting.Native.UnitName.names)
                {
                    if (name.name == value)
                    {
                        unit = name.unit;
                        return true;
                    }
                }
                unit = GraphicsUnit.Point;
                return false;
            }

            public override System.Type Type =>
                typeof(Font);
        }

        private class GuidConverter : IOneTypeObjectConverter
        {
            public static readonly IOneTypeObjectConverter Instance = new PrintingSystemXmlSerializer.GuidConverter();

            public object FromString(string str) => 
                Guid.Parse(str);

            public string ToString(object obj) => 
                ((Guid) obj).ToString("D");

            public System.Type Type =>
                typeof(Guid);
        }

        private class ImageConverter : IOneTypeObjectConverter
        {
            public static readonly PrintingSystemXmlSerializer.ImageConverter Instance = new PrintingSystemXmlSerializer.ImageConverter();

            protected ImageConverter()
            {
            }

            public object FromString(string str) => 
                PSConvert.ImageFromArray(Convert.FromBase64String(str));

            public string ToString(object obj) => 
                Convert.ToBase64String(PSConvert.ImageToArray((Image) obj));

            public virtual System.Type Type =>
                typeof(Image);
        }

        public class ImageFormatConverter : IOneTypeObjectConverter
        {
            public static readonly PrintingSystemXmlSerializer.ImageFormatConverter Instance = new PrintingSystemXmlSerializer.ImageFormatConverter();

            public object FromString(string str)
            {
                if (str != null)
                {
                    string b = str.Trim();
                    using (IEnumerator<PropertyInfo> enumerator = this.GetProperties().GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            PropertyInfo current = enumerator.Current;
                            if (string.Equals(current.Name, b, StringComparison.OrdinalIgnoreCase))
                            {
                                return current.GetValue(null, null);
                            }
                        }
                    }
                }
                return null;
            }

            [IteratorStateMachine(typeof(<GetProperties>d__5))]
            private IEnumerable<PropertyInfo> GetProperties()
            {
                <GetProperties>d__5 d__1 = new <GetProperties>d__5(-2);
                d__1.<>4__this = this;
                return d__1;
            }

            public string ToString(object obj)
            {
                if (obj is ImageFormat)
                {
                    using (IEnumerator<PropertyInfo> enumerator = this.GetProperties().GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            PropertyInfo current = enumerator.Current;
                            if (current.GetValue(null, null).Equals(obj))
                            {
                                return current.Name;
                            }
                        }
                    }
                }
                return null;
            }

            public System.Type Type =>
                typeof(ImageFormat);

            [CompilerGenerated]
            private sealed class <GetProperties>d__5 : IEnumerable<PropertyInfo>, IEnumerable, IEnumerator<PropertyInfo>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private PropertyInfo <>2__current;
                private int <>l__initialThreadId;
                public PrintingSystemXmlSerializer.ImageFormatConverter <>4__this;
                private PropertyInfo[] <>7__wrap1;
                private int <>7__wrap2;

                [DebuggerHidden]
                public <GetProperties>d__5(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        PropertyInfo[] properties = this.<>4__this.Type.GetProperties();
                        this.<>7__wrap1 = properties;
                        this.<>7__wrap2 = 0;
                    }
                    else
                    {
                        if (num != 1)
                        {
                            return false;
                        }
                        this.<>1__state = -1;
                        goto TR_0007;
                    }
                TR_0005:
                    if (this.<>7__wrap2 >= this.<>7__wrap1.Length)
                    {
                        this.<>7__wrap1 = null;
                        return false;
                    }
                    PropertyInfo info = this.<>7__wrap1[this.<>7__wrap2];
                    MethodInfo getMethod = info.GetGetMethod();
                    if ((getMethod != null) && getMethod.IsStatic)
                    {
                        this.<>2__current = info;
                        this.<>1__state = 1;
                        return true;
                    }
                TR_0007:
                    while (true)
                    {
                        this.<>7__wrap2++;
                        break;
                    }
                    goto TR_0005;
                }

                [DebuggerHidden]
                IEnumerator<PropertyInfo> IEnumerable<PropertyInfo>.GetEnumerator()
                {
                    PrintingSystemXmlSerializer.ImageFormatConverter.<GetProperties>d__5 d__;
                    if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                    {
                        this.<>1__state = 0;
                        d__ = this;
                    }
                    else
                    {
                        d__ = new PrintingSystemXmlSerializer.ImageFormatConverter.<GetProperties>d__5(0) {
                            <>4__this = this.<>4__this
                        };
                    }
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                PropertyInfo IEnumerator<PropertyInfo>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        public class ImageSizeModeConverter : StructStringConverter
        {
            public static readonly PrintingSystemXmlSerializer.ImageSizeModeConverter Instance = new PrintingSystemXmlSerializer.ImageSizeModeConverter();

            protected override object CreateObject(string[] values)
            {
                string str = values[0];
                return ((str != "Zoom") ? Enum.Parse(typeof(ImageSizeMode), str, false) : ImageSizeMode.ZoomImage);
            }

            protected override string[] GetValues(object obj) => 
                new string[] { obj.ToString() };

            public override System.Type Type =>
                typeof(ImageSizeMode);
        }

        public class MarginsConverter : StructConverter<string>
        {
            public static readonly PrintingSystemXmlSerializer.MarginsConverter Instance = new PrintingSystemXmlSerializer.MarginsConverter();

            private static Margins ConvertFromString(string strValue)
            {
                string str = strValue.Trim();
                if (str.Length == 0)
                {
                    return null;
                }
                CultureInfo invariantCulture = CultureInfo.InvariantCulture;
                char ch = invariantCulture.TextInfo.ListSeparator[0];
                char[] separator = new char[] { ch };
                string[] strArray = str.Split(separator);
                int[] numArray = new int[strArray.Length];
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                for (int i = 0; i < numArray.Length; i++)
                {
                    numArray[i] = (int) converter.ConvertFromString(null, invariantCulture, strArray[i]);
                }
                if (numArray.Length != 4)
                {
                    throw new ArgumentException($"Text '{str}' is not valid for format '{"left, right, top, bottom"}'.");
                }
                return new Margins(numArray[0], numArray[1], numArray[2], numArray[3]);
            }

            private static string ConvertToString(object obj)
            {
                if (!(obj is Margins))
                {
                    return obj.ToString();
                }
                Margins margins = (Margins) obj;
                CultureInfo invariantCulture = CultureInfo.InvariantCulture;
                string separator = invariantCulture.TextInfo.ListSeparator + " ";
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                string[] strArray = new string[4];
                int num = 0;
                strArray[num++] = converter.ConvertToString(null, invariantCulture, margins.Left);
                strArray[num++] = converter.ConvertToString(null, invariantCulture, margins.Right);
                strArray[num++] = converter.ConvertToString(null, invariantCulture, margins.Top);
                strArray[num++] = converter.ConvertToString(null, invariantCulture, margins.Bottom);
                return string.Join(separator, strArray);
            }

            protected override object CreateObject(string[] values)
            {
                string strValue = values.FirstOrDefault<string>();
                return ((strValue != null) ? ConvertFromString(strValue) : null);
            }

            protected override string ElementToString(string obj) => 
                obj;

            protected override string[] GetValues(object obj)
            {
                string str = ConvertToString(obj);
                return new string[] { str };
            }

            protected override string[] SplitValue(string value) => 
                new string[] { value };

            protected override string ToType(string str) => 
                str;

            public override System.Type Type =>
                typeof(Margins);
        }

        private class MetafileConverter : PrintingSystemXmlSerializer.ImageConverter
        {
            public static readonly PrintingSystemXmlSerializer.MetafileConverter Instance = new PrintingSystemXmlSerializer.MetafileConverter();

            private MetafileConverter()
            {
            }

            public override System.Type Type =>
                typeof(Metafile);
        }

        private class PointConverter : StructIntConverter
        {
            public static readonly PrintingSystemXmlSerializer.PointConverter Instance = new PrintingSystemXmlSerializer.PointConverter();

            private PointConverter()
            {
            }

            protected override object CreateObject(int[] values) => 
                new Point(values[0], values[1]);

            protected override int[] GetValues(object obj)
            {
                Point point = (Point) obj;
                return new int[] { point.X, point.Y };
            }

            public override System.Type Type =>
                typeof(Point);
        }

        private class PointFConverter : StructFloatConverter
        {
            public static readonly PrintingSystemXmlSerializer.PointFConverter Instance = new PrintingSystemXmlSerializer.PointFConverter();

            private PointFConverter()
            {
            }

            protected override object CreateObject(float[] values) => 
                new PointF(values[0], values[1]);

            protected override float[] GetValues(object obj)
            {
                PointF tf = (PointF) obj;
                return new float[] { tf.X, tf.Y };
            }

            public override System.Type Type =>
                typeof(PointF);
        }

        private class PrintingSystemObjectConverterImplementation : ObjectConverterImplementation
        {
            protected override string SerialzeWithBinaryFormatter(object obj) => 
                base.SerialzeWithBinaryFormatter(obj);
        }

        private class RectangleConverter : StructIntConverter
        {
            public static readonly PrintingSystemXmlSerializer.RectangleConverter Instance = new PrintingSystemXmlSerializer.RectangleConverter();

            private RectangleConverter()
            {
            }

            protected override object CreateObject(int[] values) => 
                new Rectangle(values[0], values[1], values[2], values[3]);

            protected override int[] GetValues(object obj)
            {
                Rectangle rectangle = (Rectangle) obj;
                return new int[] { rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height };
            }

            public override System.Type Type =>
                typeof(Rectangle);
        }

        private class RectangleFConverter : StructFloatConverter
        {
            public static readonly PrintingSystemXmlSerializer.RectangleFConverter Instance = new PrintingSystemXmlSerializer.RectangleFConverter();

            private RectangleFConverter()
            {
            }

            protected override object CreateObject(float[] values) => 
                new RectangleF(values[0], values[1], values[2], values[3]);

            protected override float[] GetValues(object obj)
            {
                RectangleF ef = (RectangleF) obj;
                return new float[] { ef.X, ef.Y, ef.Width, ef.Height };
            }

            public override System.Type Type =>
                typeof(RectangleF);
        }

        private class SizeConverter : StructIntConverter
        {
            public static readonly PrintingSystemXmlSerializer.SizeConverter Instance = new PrintingSystemXmlSerializer.SizeConverter();

            private SizeConverter()
            {
            }

            protected override object CreateObject(int[] values) => 
                new Size(values[0], values[1]);

            protected override int[] GetValues(object obj)
            {
                Size size = (Size) obj;
                return new int[] { size.Width, size.Height };
            }

            public override System.Type Type =>
                typeof(Size);
        }

        private class SizeFConverter : StructFloatConverter
        {
            public static readonly PrintingSystemXmlSerializer.SizeFConverter Instance = new PrintingSystemXmlSerializer.SizeFConverter();

            private SizeFConverter()
            {
            }

            protected override object CreateObject(float[] values) => 
                new SizeF(values[0], values[1]);

            protected override float[] GetValues(object obj)
            {
                SizeF ef = (SizeF) obj;
                return new float[] { ef.Width, ef.Height };
            }

            public override System.Type Type =>
                typeof(SizeF);
        }
    }
}

