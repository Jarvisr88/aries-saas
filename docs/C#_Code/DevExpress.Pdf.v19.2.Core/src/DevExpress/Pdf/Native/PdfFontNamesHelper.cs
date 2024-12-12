namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class PdfFontNamesHelper
    {
        public const string SymbolFontFamily = "Symbol";
        private static readonly Regex advancedStylePattern = new Regex("(?i)(semibold|semilight|demibold|demi|light|black|md|bd|it|sb|scn)");
        private static readonly Regex boldPattern = new Regex("(?i)(demibold|demi|black|bold|bd)");
        private static readonly Regex italicPattern = new Regex("(?i)(italic|oblique|it)");
        private static readonly Regex stylePattern = new Regex(@"(?i)(((?<=([\s|,|-]+.*))(md|sb|bd|it|scn)+)|(([\s|,|-]*(semibold|semilight|demi|light|black|bold|italic|oblique))+))(mt){0,1}$");
        private const string separators = @"[\s|.|\-|_|,]";
        private const string separatorsPattern = @"([\s|.|\-|_|,]+)";
        private static RegexContext regex;

        static PdfFontNamesHelper()
        {
            string[] patterns = new string[] { "italic", "kursiv", "ital", "ita", "cursive" };
            MappingInfo[] mappingInfo = new MappingInfo[0x15];
            mappingInfo[0] = new MappingInfo(builder => builder.Style = DXFontStyle.Italic, patterns);
            string[] textArray2 = new string[] { "oblique", "inclined", "backslanted", "backslant", "slanted" };
            mappingInfo[1] = new MappingInfo(builder => builder.Style = DXFontStyle.Oblique, textArray2);
            string[] textArray3 = new string[] { "thin", "extra thin", "ext thin", "ultra thin" };
            mappingInfo[2] = new MappingInfo(builder => builder.Weight = DXFontWeight.Thin, textArray3);
            string[] textArray4 = new string[] { "extralight", "ultralight", "extra light", "ext light", "ultra light" };
            mappingInfo[3] = new MappingInfo(builder => builder.Weight = DXFontWeight.ExtraLight, textArray4);
            string[] textArray5 = new string[] { "light" };
            mappingInfo[4] = new MappingInfo(builder => builder.Weight = DXFontWeight.Light, textArray5);
            string[] textArray6 = new string[] { "book", "regular", "normal" };
            mappingInfo[5] = new MappingInfo(builder => builder.Weight = DXFontWeight.Normal, textArray6);
            string[] textArray7 = new string[] { "medium" };
            mappingInfo[6] = new MappingInfo(builder => builder.Weight = DXFontWeight.Medium, textArray7);
            string[] textArray8 = new string[] { "demibold", "demi bold", "demi", "semibold", "semi bold" };
            mappingInfo[7] = new MappingInfo(builder => builder.Weight = DXFontWeight.DemiBold, textArray8);
            string[] textArray9 = new string[] { "bold" };
            mappingInfo[8] = new MappingInfo(builder => builder.Weight = DXFontWeight.Bold, textArray9);
            string[] textArray10 = new string[] { "ultrablack", "superblack", "extrablack", "extra black", "ext black", "ultra black" };
            mappingInfo[9] = new MappingInfo(builder => builder.Weight = DXFontWeight.ExtraBlack, textArray10);
            string[] textArray11 = new string[] { "black", "heavy", "nord" };
            mappingInfo[10] = new MappingInfo(builder => builder.Weight = DXFontWeight.Black, textArray11);
            string[] textArray12 = new string[] { "ultracondensed", "extra compressed", "ext compressed", "ultra compressed", "ultra condensed", "ultra cond" };
            mappingInfo[11] = new MappingInfo(builder => builder.Stretch = DXFontStretch.UltraCondensed, textArray12);
            string[] textArray13 = new string[] { "extracondensed", "compressed", "extra condensed", "ext condensed", "extra cond", "ext cond" };
            mappingInfo[12] = new MappingInfo(builder => builder.Stretch = DXFontStretch.ExtraCondensed, textArray13);
            string[] textArray14 = new string[] { "semicondensed", "narrow", "compact", "semi condensed", "semi cond" };
            mappingInfo[13] = new MappingInfo(builder => builder.Stretch = DXFontStretch.SemiCondensed, textArray14);
            string[] textArray15 = new string[] { "condensed", "cond" };
            mappingInfo[14] = new MappingInfo(builder => builder.Stretch = DXFontStretch.Condensed, textArray15);
            string[] textArray16 = new string[] { "normal" };
            mappingInfo[15] = new MappingInfo(builder => builder.Stretch = DXFontStretch.Normal, textArray16);
            string[] textArray17 = new string[] { "semiexpanded", "wide", "semi expanded", "semi extended" };
            mappingInfo[0x10] = new MappingInfo(builder => builder.Stretch = DXFontStretch.SemiExpanded, textArray17);
            string[] textArray18 = new string[] { "ultraexpanded", "ultra expanded", "ultra extended" };
            mappingInfo[0x11] = new MappingInfo(builder => builder.Stretch = DXFontStretch.UltraExpanded, textArray18);
            string[] textArray19 = new string[] { "expanded", "extended" };
            mappingInfo[0x12] = new MappingInfo(builder => builder.Stretch = DXFontStretch.Expanded, textArray19);
            string[] textArray20 = new string[] { "extraexpanded", "extra expanded", "ext expanded", "extra extended", "ext extended" };
            mappingInfo[0x13] = new MappingInfo(builder => builder.Stretch = DXFontStretch.ExtraExpanded, textArray20);
            string[] textArray21 = new string[] { "extrabold", "superbold", "ultrabold", "extra bold", "ext bold", "ultra bold", "ultra" };
            mappingInfo[20] = new MappingInfo(builder => builder.Weight = DXFontWeight.ExtraBold, textArray21);
            regex = CreateRegex(mappingInfo);
        }

        public static bool ContainsBoldStyle(string fontStyle) => 
            !string.IsNullOrEmpty(fontStyle) && boldPattern.IsMatch(fontStyle);

        public static bool ContainsItalicStyle(string fontStyle) => 
            !string.IsNullOrEmpty(fontStyle) && italicPattern.IsMatch(fontStyle);

        private static RegexContext CreateRegex(IList<MappingInfo> mappingInfo)
        {
            StringBuilder builder = new StringBuilder();
            IDictionary<string, Action<DXFontDescriptorBuilder>> groupMapping = new Dictionary<string, Action<DXFontDescriptorBuilder>>();
            builder.Append("(?i)(");
            builder.Append(@"([\s|.|\-|_|,]+)");
            builder.Append("(");
            int num = 0;
            while (num < mappingInfo.Count)
            {
                string key = "A" + num.ToString();
                if (num > 0)
                {
                    builder.Append("|");
                }
                builder.Append("(?'" + key + "'(");
                IList<string> patterns = mappingInfo[num].Patterns;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= patterns.Count)
                    {
                        builder.Append("))");
                        groupMapping.Add(key, mappingInfo[num].Action);
                        num++;
                        break;
                    }
                    if (num2 > 0)
                    {
                        builder.Append("|");
                    }
                    builder.Append("(");
                    builder.Append(patterns[num2].Replace(" ", @"([\s|.|\-|_|,]+)"));
                    builder.Append(")");
                    num2++;
                }
            }
            builder.Append("))+");
            return new RegexContext(groupMapping, new Regex(builder.ToString()));
        }

        public static IEnumerable<string> ExtractAdditionalStyles(string actualStyle) => 
            advancedStylePattern.Split(actualStyle);

        public static DXFontDescriptor GetDescriptor(string fontName, PdfFontStyle style = 0)
        {
            DXFontDescriptorBuilder builder = new DXFontDescriptorBuilder();
            regex.Match(fontName, builder);
            if ((builder.Style == DXFontStyle.Regular) && style.HasFlag(PdfFontStyle.Italic))
            {
                builder.Style = DXFontStyle.Italic;
            }
            if ((builder.Weight == DXFontWeight.Normal) && style.HasFlag(PdfFontStyle.Bold))
            {
                builder.Weight = DXFontWeight.Bold;
            }
            return builder.CreateFontDescriptor();
        }

        public static string GetFontFamily(string fontName) => 
            stylePattern.Replace(fontName, string.Empty);

        internal static DXFontDescriptor GetGDICompatibleDescriptor(string fontName, PdfFontStyle style)
        {
            DXFontDescriptorBuilder builder = new DXFontDescriptorBuilder();
            regex.Match(fontName, builder);
            return new DXFontDescriptor(fontName, ((builder.Weight >= DXFontWeight.Bold) || !style.HasFlag(PdfFontStyle.Bold)) ? DXFontWeight.Normal : DXFontWeight.Bold, ((builder.Style != DXFontStyle.Regular) || !style.HasFlag(PdfFontStyle.Italic)) ? DXFontStyle.Regular : DXFontStyle.Italic, DXFontStretch.Normal);
        }

        public static string GetNormalizedFontFamily(string fontName) => 
            Normalize(GetFontFamily(fontName));

        public static string GetNormalizedFontStyle(string fontName) => 
            Normalize(stylePattern.Match(fontName).Value);

        public static string Normalize(string value)
        {
            StringBuilder builder = new StringBuilder(value);
            builder.Replace(" ", string.Empty);
            builder.Replace("-", string.Empty);
            builder.Replace(",", string.Empty);
            return builder.ToString().ToLowerInvariant();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfFontNamesHelper.<>c <>9 = new PdfFontNamesHelper.<>c();

            internal void <.cctor>b__11_0(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Style = DXFontStyle.Italic;
            }

            internal void <.cctor>b__11_1(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Style = DXFontStyle.Oblique;
            }

            internal void <.cctor>b__11_10(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.Black;
            }

            internal void <.cctor>b__11_11(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.UltraCondensed;
            }

            internal void <.cctor>b__11_12(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.ExtraCondensed;
            }

            internal void <.cctor>b__11_13(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.SemiCondensed;
            }

            internal void <.cctor>b__11_14(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.Condensed;
            }

            internal void <.cctor>b__11_15(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.Normal;
            }

            internal void <.cctor>b__11_16(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.SemiExpanded;
            }

            internal void <.cctor>b__11_17(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.UltraExpanded;
            }

            internal void <.cctor>b__11_18(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.Expanded;
            }

            internal void <.cctor>b__11_19(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Stretch = DXFontStretch.ExtraExpanded;
            }

            internal void <.cctor>b__11_2(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.Thin;
            }

            internal void <.cctor>b__11_20(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.ExtraBold;
            }

            internal void <.cctor>b__11_3(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.ExtraLight;
            }

            internal void <.cctor>b__11_4(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.Light;
            }

            internal void <.cctor>b__11_5(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.Normal;
            }

            internal void <.cctor>b__11_6(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.Medium;
            }

            internal void <.cctor>b__11_7(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.DemiBold;
            }

            internal void <.cctor>b__11_8(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.Bold;
            }

            internal void <.cctor>b__11_9(PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                builder.Weight = DXFontWeight.ExtraBlack;
            }
        }

        private class DXFontDescriptorBuilder
        {
            private DXFontWeight? weight;
            private DXFontStretch? stretch;
            private DXFontStyle? style;

            public DXFontDescriptor CreateFontDescriptor() => 
                new DXFontDescriptor(this.FamilyName, this.Weight, this.Style, this.Stretch);

            public string FamilyName { get; set; }

            public DXFontStyle Style
            {
                get
                {
                    DXFontStyle? style = this.style;
                    return ((style != null) ? style.GetValueOrDefault() : DXFontStyle.Regular);
                }
                set
                {
                    if (this.style == null)
                    {
                        this.style = new DXFontStyle?(value);
                    }
                }
            }

            public DXFontStretch Stretch
            {
                get
                {
                    DXFontStretch? stretch = this.stretch;
                    return ((stretch != null) ? stretch.GetValueOrDefault() : DXFontStretch.Normal);
                }
                set
                {
                    if (this.stretch == null)
                    {
                        this.stretch = new DXFontStretch?(value);
                    }
                }
            }

            public DXFontWeight Weight
            {
                get
                {
                    DXFontWeight? weight = this.weight;
                    return ((weight != null) ? weight.GetValueOrDefault() : DXFontWeight.Normal);
                }
                set
                {
                    if (this.weight == null)
                    {
                        this.weight = new DXFontWeight?(value);
                    }
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MappingInfo
        {
            public Action<PdfFontNamesHelper.DXFontDescriptorBuilder> Action { get; }
            public IList<string> Patterns { get; }
            public MappingInfo(Action<PdfFontNamesHelper.DXFontDescriptorBuilder> action, IList<string> patterns)
            {
                this = new PdfFontNamesHelper.MappingInfo();
                this.<Action>k__BackingField = action;
                this.<Patterns>k__BackingField = patterns;
            }
        }

        private class RegexContext
        {
            private IDictionary<string, Action<PdfFontNamesHelper.DXFontDescriptorBuilder>> groupMapping;
            private Regex regex;

            public RegexContext(IDictionary<string, Action<PdfFontNamesHelper.DXFontDescriptorBuilder>> groupMapping, Regex regex)
            {
                this.groupMapping = groupMapping;
                this.regex = regex;
            }

            public void Match(string fontName, PdfFontNamesHelper.DXFontDescriptorBuilder builder)
            {
                foreach (System.Text.RegularExpressions.Match match in this.regex.Matches(fontName))
                {
                    foreach (KeyValuePair<string, Action<PdfFontNamesHelper.DXFontDescriptorBuilder>> pair in this.groupMapping)
                    {
                        if (match.Groups[pair.Key].Success)
                        {
                            pair.Value(builder);
                        }
                    }
                }
                builder.FamilyName = this.regex.Replace(fontName, "").Trim();
            }
        }
    }
}

