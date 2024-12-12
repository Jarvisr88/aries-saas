namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class UidParser
    {
        private static readonly char UidPairSeparator;
        private static readonly char UidPropertySeparator;
        public static readonly string UidStartSymbol;

        static UidParser();
        public static bool IsUid(DeferrableAttributeEntryContext context);
        public static List<Tuple<string, string>> Parse(DeferrableAttributeEntryContext context);
        private static Tuple<string, string> ParseUidPair(string pair);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UidParser.<>c <>9;
            public static Func<string, Tuple<string, string>> <>9__3_0;

            static <>c();
            internal Tuple<string, string> <Parse>b__3_0(string x);
        }
    }
}

