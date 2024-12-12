namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class SvgCommandCollection : List<SvgCommandBase>
    {
        private static readonly RegexOptions regOptions;

        static SvgCommandCollection();
        public SvgCommandCollection();
        public SvgCommandCollection(IList<SvgCommandBase> svgCommands);
        private static string ConstructNumbers(Match match);
        public SvgRect GetBoundaryPoints();
        private static char GetByPrevCommandSymbol(char lastCommandSymbol);
        internal static string NormalizeSourceString(string sourceString);
        public static SvgCommandCollection Parse(string commandsString);
        public static SvgCommandCollection Parse(string[] commandsElementsList);
        public override string ToString();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgCommandCollection.<>c <>9;
            public static MatchEvaluator <>9__5_0;
            public static MatchEvaluator <>9__5_1;
            public static MatchEvaluator <>9__5_2;
            public static MatchEvaluator <>9__5_3;

            static <>c();
            internal string <NormalizeSourceString>b__5_0(Match m);
            internal string <NormalizeSourceString>b__5_1(Match m);
            internal string <NormalizeSourceString>b__5_2(Match m);
            internal string <NormalizeSourceString>b__5_3(Match m);
        }
    }
}

