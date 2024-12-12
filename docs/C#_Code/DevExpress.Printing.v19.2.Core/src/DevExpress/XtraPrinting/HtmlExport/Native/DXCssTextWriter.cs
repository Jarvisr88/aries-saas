namespace DevExpress.XtraPrinting.HtmlExport.Native
{
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public sealed class DXCssTextWriter : TextWriter
    {
        private const int LookupCapacity = 0x2e;
        private static Dictionary<string, DXHtmlTextWriterStyle> styleLookup = new Dictionary<string, DXHtmlTextWriterStyle>(0x2e);
        private static AttributeInformation[] attributeNameLookup = new AttributeInformation[0x2e];
        private TextWriter writer;

        static DXCssTextWriter()
        {
            RegisterAttribute("background-color", DXHtmlTextWriterStyle.BackgroundColor);
            RegisterAttribute("background-image", DXHtmlTextWriterStyle.BackgroundImage, true, true);
            RegisterAttribute("border-collapse", DXHtmlTextWriterStyle.BorderCollapse);
            RegisterAttribute("border-color", DXHtmlTextWriterStyle.BorderColor);
            RegisterAttribute("border-style", DXHtmlTextWriterStyle.BorderStyle);
            RegisterAttribute("border-width", DXHtmlTextWriterStyle.BorderWidth);
            RegisterAttribute("color", DXHtmlTextWriterStyle.Color);
            RegisterAttribute("counter-reset", DXHtmlTextWriterStyle.CounterReset);
            RegisterAttribute("counter-increment", DXHtmlTextWriterStyle.CounterIncrement);
            RegisterAttribute("content", DXHtmlTextWriterStyle.Content);
            RegisterAttribute("cursor", DXHtmlTextWriterStyle.Cursor);
            RegisterAttribute("direction", DXHtmlTextWriterStyle.Direction);
            RegisterAttribute("display", DXHtmlTextWriterStyle.Display);
            RegisterAttribute("filter", DXHtmlTextWriterStyle.Filter);
            RegisterAttribute("font-family", DXHtmlTextWriterStyle.FontFamily, true);
            RegisterAttribute("font-size", DXHtmlTextWriterStyle.FontSize);
            RegisterAttribute("font-style", DXHtmlTextWriterStyle.FontStyle);
            RegisterAttribute("font-variant", DXHtmlTextWriterStyle.FontVariant);
            RegisterAttribute("font-weight", DXHtmlTextWriterStyle.FontWeight);
            RegisterAttribute("height", DXHtmlTextWriterStyle.Height);
            RegisterAttribute("left", DXHtmlTextWriterStyle.Left);
            RegisterAttribute("list-style-image", DXHtmlTextWriterStyle.ListStyleImage, true, true);
            RegisterAttribute("list-style-type", DXHtmlTextWriterStyle.ListStyleType);
            RegisterAttribute("margin", DXHtmlTextWriterStyle.Margin);
            RegisterAttribute("margin-bottom", DXHtmlTextWriterStyle.MarginBottom);
            RegisterAttribute("margin-left", DXHtmlTextWriterStyle.MarginLeft);
            RegisterAttribute("margin-right", DXHtmlTextWriterStyle.MarginRight);
            RegisterAttribute("margin-top", DXHtmlTextWriterStyle.MarginTop);
            RegisterAttribute("overflow-x", DXHtmlTextWriterStyle.OverflowX);
            RegisterAttribute("overflow-y", DXHtmlTextWriterStyle.OverflowY);
            RegisterAttribute("overflow", DXHtmlTextWriterStyle.Overflow);
            RegisterAttribute("padding", DXHtmlTextWriterStyle.Padding);
            RegisterAttribute("padding-bottom", DXHtmlTextWriterStyle.PaddingBottom);
            RegisterAttribute("padding-left", DXHtmlTextWriterStyle.PaddingLeft);
            RegisterAttribute("padding-right", DXHtmlTextWriterStyle.PaddingRight);
            RegisterAttribute("padding-top", DXHtmlTextWriterStyle.PaddingTop);
            RegisterAttribute("position", DXHtmlTextWriterStyle.Position);
            RegisterAttribute("text-align", DXHtmlTextWriterStyle.TextAlign);
            RegisterAttribute("text-decoration", DXHtmlTextWriterStyle.TextDecoration);
            RegisterAttribute("text-overflow", DXHtmlTextWriterStyle.TextOverflow);
            RegisterAttribute("top", DXHtmlTextWriterStyle.Top);
            RegisterAttribute("vertical-align", DXHtmlTextWriterStyle.VerticalAlign);
            RegisterAttribute("visibility", DXHtmlTextWriterStyle.Visibility);
            RegisterAttribute("width", DXHtmlTextWriterStyle.Width);
            RegisterAttribute("white-space", DXHtmlTextWriterStyle.WhiteSpace);
            RegisterAttribute("z-index", DXHtmlTextWriterStyle.ZIndex);
        }

        public DXCssTextWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public override void Close()
        {
            this.writer.Close();
            base.Close();
        }

        public override void Flush()
        {
            this.writer.Flush();
        }

        public static DXHtmlTextWriterStyle GetStyleKey(string styleName)
        {
            DXHtmlTextWriterStyle style;
            return ((string.IsNullOrEmpty(styleName) || !styleLookup.TryGetValue(styleName.ToLowerInvariant(), out style)) ? ~DXHtmlTextWriterStyle.BackgroundColor : style);
        }

        public static string GetStyleName(DXHtmlTextWriterStyle styleKey) => 
            ((styleKey < DXHtmlTextWriterStyle.BackgroundColor) || (styleKey >= attributeNameLookup.Length)) ? string.Empty : attributeNameLookup[(int) styleKey].name;

        public static bool IsStyleEncoded(DXHtmlTextWriterStyle styleKey) => 
            (styleKey < DXHtmlTextWriterStyle.BackgroundColor) || ((styleKey >= attributeNameLookup.Length) || attributeNameLookup[(int) styleKey].encode);

        internal static void RegisterAttribute(string name, DXHtmlTextWriterStyle key)
        {
            RegisterAttribute(name, key, false, false);
        }

        internal static void RegisterAttribute(string name, DXHtmlTextWriterStyle key, bool encode)
        {
            RegisterAttribute(name, key, encode, false);
        }

        internal static void RegisterAttribute(string name, DXHtmlTextWriterStyle key, bool encode, bool isUrl)
        {
            string str = name.ToLowerInvariant();
            styleLookup.Add(str, key);
            if (key < attributeNameLookup.Length)
            {
                attributeNameLookup[(int) key] = new AttributeInformation(name, encode, isUrl);
            }
        }

        public override void Write(bool value)
        {
            this.writer.Write(value);
        }

        public override void Write(char value)
        {
            this.writer.Write(value);
        }

        public override void Write(int value)
        {
            this.writer.Write(value);
        }

        public override void Write(char[] buffer)
        {
            this.writer.Write(buffer);
        }

        public override void Write(double value)
        {
            this.writer.Write(value);
        }

        public override void Write(long value)
        {
            this.writer.Write(value);
        }

        public override void Write(object value)
        {
            this.writer.Write(value);
        }

        public override void Write(float value)
        {
            this.writer.Write(value);
        }

        public override void Write(string s)
        {
            this.writer.Write(s);
        }

        public override void Write(string format, params object[] arg)
        {
            this.writer.Write(format, arg);
        }

        public override void Write(string format, object arg0)
        {
            this.writer.Write(format, arg0);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this.writer.Write(buffer, index, count);
        }

        public override void Write(string format, object arg0, object arg1)
        {
            this.writer.Write(format, arg0, arg1);
        }

        public void WriteAttribute(DXHtmlTextWriterStyle key, string value)
        {
            WriteAttribute(this.writer, key, GetStyleName(key), value);
        }

        public void WriteAttribute(string name, string value)
        {
            WriteAttribute(this.writer, GetStyleKey(name), name, value);
        }

        private static void WriteAttribute(TextWriter writer, DXHtmlTextWriterStyle key, string name, string value)
        {
            writer.Write(name);
            writer.Write(':');
            bool isUrl = false;
            if (key != ~DXHtmlTextWriterStyle.BackgroundColor)
            {
                isUrl = attributeNameLookup[(int) key].isUrl;
            }
            if (!isUrl)
            {
                writer.Write(value);
            }
            else
            {
                WriteUrlAttribute(writer, value);
            }
            writer.Write(';');
        }

        internal static void WriteAttributes(TextWriter writer, List<DXWebRenderStyle> styles)
        {
            for (int i = 0; i < styles.Count; i++)
            {
                DXWebRenderStyle style = styles[i];
                WriteAttribute(writer, style.key, style.name, style.value);
            }
        }

        public void WriteBeginCssRule(string selector)
        {
            this.writer.Write(selector);
            this.writer.Write(" { ");
        }

        public void WriteEndCssRule()
        {
            this.writer.WriteLine(" }");
        }

        public override void WriteLine()
        {
            this.writer.WriteLine();
        }

        public override void WriteLine(bool value)
        {
            this.writer.WriteLine(value);
        }

        public override void WriteLine(char value)
        {
            this.writer.WriteLine(value);
        }

        public override void WriteLine(double value)
        {
            this.writer.WriteLine(value);
        }

        public override void WriteLine(char[] buffer)
        {
            this.writer.WriteLine(buffer);
        }

        public override void WriteLine(int value)
        {
            this.writer.WriteLine(value);
        }

        public override void WriteLine(long value)
        {
            this.writer.WriteLine(value);
        }

        public override void WriteLine(object value)
        {
            this.writer.WriteLine(value);
        }

        public override void WriteLine(float value)
        {
            this.writer.WriteLine(value);
        }

        public override void WriteLine(string s)
        {
            this.writer.WriteLine(s);
        }

        [CLSCompliant(false)]
        public override void WriteLine(uint value)
        {
            this.writer.WriteLine(value);
        }

        public override void WriteLine(string format, object arg0)
        {
            this.writer.WriteLine(format, arg0);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            this.writer.WriteLine(format, arg);
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            this.writer.WriteLine(buffer, index, count);
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            this.writer.WriteLine(format, arg0, arg1);
        }

        private static void WriteUrlAttribute(TextWriter writer, string url)
        {
            string str = url;
            if (url.StartsWith("url("))
            {
                int startIndex = 4;
                int length = url.Length - 4;
                if (DXWebStringUtil.StringEndsWith(url, ')'))
                {
                    length--;
                }
                str = url.Substring(startIndex, length).Trim();
            }
            writer.Write("url(");
            writer.Write(DXHttpUtility.UrlPathEncode(str));
            writer.Write(')');
        }

        public override System.Text.Encoding Encoding =>
            this.writer.Encoding;

        public override string NewLine
        {
            get => 
                this.writer.NewLine;
            set => 
                this.writer.NewLine = value;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AttributeInformation
        {
            public string name;
            public bool isUrl;
            public bool encode;
            public AttributeInformation(string name, bool encode, bool isUrl)
            {
                this.name = name;
                this.encode = encode;
                this.isUrl = isUrl;
            }
        }
    }
}

