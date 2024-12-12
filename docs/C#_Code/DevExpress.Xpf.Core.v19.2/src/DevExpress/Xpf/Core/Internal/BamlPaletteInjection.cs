namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    internal class BamlPaletteInjection
    {
        private static readonly string uidContentName;
        private static readonly Regex ResourceSourceRegex;
        private static readonly Regex ThemeNameReplaceRegex;
        private readonly ParsedBaml baml;

        static BamlPaletteInjection();
        public BamlPaletteInjection(Stream bamlStream);
        private Dictionary<string, string> CreateUidMap(BamlEntry uid);
        private Color? FindDXPaletteColor(BamlEntry entry);
        private BamlEntry FindKeyElementStartEntry(BamlEntry child);
        private List<BamlEntry> FindWholeElementByIndex(int uidIndex);
        private string GetAttributeById(short id);
        public Dictionary<string, Color> GetDXPaletteColors(string paletteName);
        private void Init(Dictionary<string, Color> palette);
        public MemoryStream Inject(Dictionary<string, Color> palette, string themeName, string baseTheme);
        private bool IsUid(BamlEntry x);
        private void ProcessColors();
        private void ProcessResourceDictionarySource(string themeName, string baseThemeName);
        private void ProcessThemePartLoader(string themeName, string baseThemeName);
        private void ReplaceThemeName(string themeName, string baseThemeName);
        public static string ReplaceThemeName(string value, string themeName, string baseThemeName);

        private Dictionary<int, BamlEntry> Map { get; set; }

        private List<BamlEntry> Entries { get; }

        private List<BamlEntry> UidList { get; set; }

        private Dictionary<short, AttributeEntryContext> AttributeMap { get; set; }

        private Dictionary<string, Color> Palette { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BamlPaletteInjection.<>c <>9;
            public static Func<BamlEntry, int> <>9__24_0;
            public static Func<BamlEntry, BamlEntry> <>9__24_1;
            public static Func<BamlEntry, bool> <>9__24_2;
            public static Func<BamlEntry, short> <>9__24_3;
            public static Func<BamlEntry, AttributeEntryContext> <>9__24_4;
            public static Func<BamlEntry, bool> <>9__25_0;
            public static Func<Tuple<string, string>, string> <>9__31_0;
            public static Func<Tuple<string, string>, string> <>9__31_1;
            public static Func<BamlEntry, bool> <>9__34_0;
            public static Func<BamlEntry, ConvertedPropertyEntryContext> <>9__34_1;
            public static Func<BamlEntry, bool> <>9__35_0;
            public static Func<BamlEntry, ConvertedPropertyEntryContext> <>9__35_1;

            static <>c();
            internal string <CreateUidMap>b__31_0(Tuple<string, string> x);
            internal string <CreateUidMap>b__31_1(Tuple<string, string> x);
            internal bool <GetDXPaletteColors>b__25_0(BamlEntry x);
            internal int <Init>b__24_0(BamlEntry x);
            internal BamlEntry <Init>b__24_1(BamlEntry x);
            internal bool <Init>b__24_2(BamlEntry x);
            internal short <Init>b__24_3(BamlEntry x);
            internal AttributeEntryContext <Init>b__24_4(BamlEntry x);
            internal bool <ProcessResourceDictionarySource>b__35_0(BamlEntry x);
            internal ConvertedPropertyEntryContext <ProcessResourceDictionarySource>b__35_1(BamlEntry x);
            internal bool <ProcessThemePartLoader>b__34_0(BamlEntry x);
            internal ConvertedPropertyEntryContext <ProcessThemePartLoader>b__34_1(BamlEntry x);
        }
    }
}

