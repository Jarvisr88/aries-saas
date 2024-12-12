namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class ParsedBaml
    {
        private readonly List<BamlEntry> entries;

        private ParsedBaml();
        public List<BamlEntry> Entries();
        private static int FindDeferrableEntryIndex(ParsedBaml baml, BamlEntry entry);
        private static int FindIndexByTree(List<BamlEntry> entries, byte startType, byte endType, int index);
        public static ParsedBaml FromStream(Stream stream, string name = "");
        private static int GetDeferrableContentStartSize(BamlEntryContext context);
        private static int GetDeferrableKeyPosition(BamlEntryContext context);
        private static int GetDeferrableTypePosition(BamlEntryContext context);
        internal void InsertEntry(int index, BamlEntry bamlEntry);
        private static void ReadDeferrableContent(ParsedBaml baml);
        private static void ReadEntries(ParsedBaml baml, BamlStreamReader reader);
        private static BamlEntry ReadEntry(BamlStreamReader reader);
        private static void ReadHeader(ParsedBaml baml, BamlStreamReader reader);
        public MemoryStream ToStream();
        private void WriteDeferrableContent(BamlStreamWriter writer);
        private void WriteEntries(BamlStreamWriter writer);
        private void WriteEntry(BamlStreamWriter writer, BamlEntry entry);
        private void WriteHeader(BamlStreamWriter writer);

        public string Name { get; private set; }

        public byte[] Header { get; private set; }

        public int ReaderVersion { get; private set; }

        public int UpdaterVersion { get; private set; }

        public int WriterVersion { get; private set; }

        private Dictionary<BamlEntry, BamlEntry> DeferrableContent { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ParsedBaml.<>c <>9;
            public static Func<BamlEntry, bool> <>9__30_0;
            public static Func<BamlEntry, int> <>9__36_0;
            public static Func<BamlEntry, BamlEntry> <>9__36_1;
            public static Func<BamlEntry, bool> <>9__36_2;

            static <>c();
            internal int <ReadDeferrableContent>b__36_0(BamlEntry x);
            internal BamlEntry <ReadDeferrableContent>b__36_1(BamlEntry x);
            internal bool <ReadDeferrableContent>b__36_2(BamlEntry x);
            internal bool <WriteDeferrableContent>b__30_0(BamlEntry x);
        }
    }
}

