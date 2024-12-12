namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class SvgTextWrapper : SvgElementWrapper
    {
        private GraphicsPath pathCore;
        private static readonly Regex RemoveSpaces = new Regex(" {2,}", RegexOptions.Compiled);

        public SvgTextWrapper(SvgElement element) : base(element)
        {
        }

        private void CalcPathCore(TextPart textPart, double scale)
        {
            foreach (SvgElementWrapper wrapper in base.Childs)
            {
                SvgTextWrapper element = wrapper as SvgTextWrapper;
                if (element == null)
                {
                    SvgContentWrapper item = wrapper as SvgContentWrapper;
                    if (string.IsNullOrEmpty(item.Text))
                    {
                        continue;
                    }
                    textPart.AddStringToPath(this.TrimText(item), base.graphics);
                    continue;
                }
                TextPart part = new TextPart(textPart, element);
                element.graphics = base.graphics;
                element.CalcPathCore(part, scale);
                textPart.CharCount += part.CharCount;
                textPart.Offset = part.Offset;
            }
            GraphicsPath path = textPart.GetPath();
            GraphicsPath path2 = path;
            if (path == null)
            {
                GraphicsPath local1 = path;
                path2 = new GraphicsPath();
            }
            this.pathCore = path2;
            Matrix matrix = new Matrix();
            matrix.Scale((float) scale, (float) scale);
            this.pathCore.Transform(matrix);
            this.AddPathToCache(scale, this.pathCore);
        }

        internal GdiFontWrapper GetFont()
        {
            SvgFontStyle style2;
            SvgUnit fontSize = this.Text.FontSize;
            float emSize = (fontSize != null) ? ((float) fontSize.Value) : 15f;
            emSize = (emSize <= 0f) ? 15f : emSize;
            object fontFamily = GetFontFamily(this.Text.FontFamily);
            FontStyle regular = FontStyle.Regular;
            SvgFontWeight fontWeight = this.Text.FontWeight;
            if (fontWeight > SvgFontWeight.Bold)
            {
                if ((fontWeight != SvgFontWeight.W800) && ((fontWeight != SvgFontWeight.W900) && (fontWeight != SvgFontWeight.Bolder)))
                {
                    goto TR_0002;
                }
            }
            else if ((fontWeight != SvgFontWeight.W600) && (fontWeight != SvgFontWeight.Bold))
            {
                goto TR_0002;
            }
            regular |= FontStyle.Bold;
        TR_0002:
            style2 = this.Text.FontStyle;
            if ((style2 == SvgFontStyle.Oblique) || (style2 == SvgFontStyle.Italic))
            {
                regular |= FontStyle.Italic;
            }
            return new GdiFontWrapper(new Font(fontFamily as FontFamily, emSize, regular, GraphicsUnit.Pixel));
        }

        public static object GetFontFamily(string fontFamilyString)
        {
            object genericSerif;
            if (string.IsNullOrEmpty(fontFamilyString))
            {
                return FontFamily.GenericSansSerif;
            }
            char[] separator = new char[] { ',' };
            FontFamily[] families = FontFamily.Families;
            using (IEnumerator<string> enumerator = (fontFamilyString ?? "").Split(separator).Select<string, string>((<>c.<>9__9_0 ??= fontName => fontName.Trim(new char[] { '"', ' ', '\'' }))).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        string f = enumerator.Current;
                        FontFamily family = families.FirstOrDefault<FontFamily>(ff => ff.Name.ToLower() == f.ToLower());
                        if (family != null)
                        {
                            genericSerif = family;
                        }
                        else
                        {
                            string str = f;
                            if (str == "serif")
                            {
                                genericSerif = FontFamily.GenericSerif;
                            }
                            else if (str == "sans-serif")
                            {
                                genericSerif = FontFamily.GenericSansSerif;
                            }
                            else
                            {
                                if (str != "monospace")
                                {
                                    continue;
                                }
                                genericSerif = FontFamily.GenericMonospace;
                            }
                        }
                    }
                    else
                    {
                        return FontFamily.GenericSansSerif;
                    }
                    break;
                }
            }
            return genericSerif;
        }

        protected override GraphicsPath GetPathCore(double scale)
        {
            this.CalcPathCore(new TextPart(this), scale);
            return this.pathCore;
        }

        protected override SmoothingMode GetSmoothingModeCore(SmoothingMode defaultValue) => 
            SmoothingMode.AntiAlias;

        private string TrimText(SvgElementWrapper item) => 
            RemoveSpaces.Replace((item as SvgContentWrapper).Text.Replace("\r", "").Replace("\n", "").Replace('\t', ' '), " ");

        public SvgText Text =>
            base.Element as SvgText;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgTextWrapper.<>c <>9 = new SvgTextWrapper.<>c();
            public static Func<string, string> <>9__9_0;

            internal string <GetFontFamily>b__9_0(string fontName)
            {
                char[] trimChars = new char[] { '"', ' ', '\'' };
                return fontName.Trim(trimChars);
            }
        }
    }
}

