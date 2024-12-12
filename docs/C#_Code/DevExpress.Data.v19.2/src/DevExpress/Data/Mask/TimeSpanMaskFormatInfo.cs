namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class TimeSpanMaskFormatInfo : IEnumerable<TimeSpanMaskFormatElement>, IEnumerable
    {
        protected IList<TimeSpanMaskFormatElement> innerList;
        protected TimeSpanMaskHiddenGroupCollection hiddenGroups;
        protected readonly TimeSpanCultureInfoBase cultureInfo;
        private static readonly Dictionary<Type, Type> nonEditableTypesToEditableDictionary;

        static TimeSpanMaskFormatInfo();
        public TimeSpanMaskFormatInfo(string mask, TimeSpanCultureInfoBase cultureInfo);
        private bool CheckNavigationSymbol(char chr, TimeSpanMaskFormatElementEditable format);
        private string ExpandFormat(string mask);
        private void FlushAccumulator(StringBuilder accumulator, List<TimeSpanMaskFormatElement> result, bool hideOnNull);
        private bool ForeachFormatCheckNavigationSymbol(char chr, int startIndex, int endIndex, out int formatIndex);
        public void ForEachParentParts(int currentIndex, Func<TimeSpanMaskFormatElementEditable, bool> evaluator);
        public string Format(TimeSpanMaskManagerValue formatted);
        public string Format(TimeSpanMaskManagerValue formatted, int startFormatIndex, int endFormatIndex, int showHiddenGroupIndex = -1);
        public int GetEditablePartFromLiteral(int i);
        private static Type GetEditablePartTypeByLiteralType(TimeSpanStringLiteralType literalType);
        public IEnumerator<TimeSpanMaskFormatElement> GetEnumerator();
        private int GetGroupLength(string mask);
        public int GetHiddenGroupEditableIndex(int index);
        public TimeSpanMaskFormatElementEditable GetNextPart(int currentIndex);
        public int GetNextVisibleIndex(TimeSpanMaskManagerValue currentValue, int currentIndex);
        private TimeSpanMaskFormatElementEditable GetPartByType(Type elementType);
        public int GetPartIndex(TimeSpanMaskPart part);
        public int GetPreviousVisibleIndex(TimeSpanMaskManagerValue currentValue, int currentIndex);
        private bool IsEditableElementVisible(int index, TimeSpanMaskManagerValue value);
        private bool IsGroupHidden(int groupStartIndex, int groupEndIndex, TimeSpanMaskManagerValue value);
        public bool IsHeadFormat(int index);
        public bool IsNavigationSymbol(string insertion, out int formatIndex, int startIndex);
        private void ParseMask(string mask);
        IEnumerator IEnumerable.GetEnumerator();
        public bool TryParse(string initialEditText, int startIndex, TimeSpan currentValue, out TimeSpan res);

        public int Count { get; }

        public bool IsNegative { get; set; }

        public TimeSpanMaskFormatElement this[int index] { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimeSpanMaskFormatInfo.<>c <>9;
            public static Predicate<TimeSpanMaskFormatElement> <>9__17_0;
            public static Predicate<TimeSpanMaskFormatElement> <>9__17_1;
            public static Predicate<TimeSpanMaskFormatElement> <>9__17_2;
            public static Predicate<TimeSpanMaskFormatElement> <>9__17_3;
            public static Predicate<TimeSpanMaskFormatElement> <>9__17_4;
            public static Predicate<TimeSpanMaskFormatElement> <>9__17_5;

            static <>c();
            internal bool <GetPartIndex>b__17_0(TimeSpanMaskFormatElement x);
            internal bool <GetPartIndex>b__17_1(TimeSpanMaskFormatElement x);
            internal bool <GetPartIndex>b__17_2(TimeSpanMaskFormatElement x);
            internal bool <GetPartIndex>b__17_3(TimeSpanMaskFormatElement x);
            internal bool <GetPartIndex>b__17_4(TimeSpanMaskFormatElement x);
            internal bool <GetPartIndex>b__17_5(TimeSpanMaskFormatElement x);
        }

        private class TimeSpanParser
        {
            private void AddTokenFromAccumulator(StringBuilder accumulator, TimeSpanMaskFormatInfo.TimeSpanParser.TokenType type, out TimeSpanMaskFormatInfo.TimeSpanParser.ParserState currentState, ref int index);
            private static TimeSpanMaskFormatInfo.TimeSpanParser.CharType GetCharType(char chr);
            public void Parse(string input);

            public List<TimeSpanMaskFormatInfo.TimeSpanParser.TimeSpanToken> Tokens { get; set; }

            private enum CharType
            {
                public const TimeSpanMaskFormatInfo.TimeSpanParser.CharType Letter = TimeSpanMaskFormatInfo.TimeSpanParser.CharType.Letter;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.CharType Digit = TimeSpanMaskFormatInfo.TimeSpanParser.CharType.Digit;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.CharType Separator = TimeSpanMaskFormatInfo.TimeSpanParser.CharType.Separator;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.CharType Tabulator = TimeSpanMaskFormatInfo.TimeSpanParser.CharType.Tabulator;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.CharType None = TimeSpanMaskFormatInfo.TimeSpanParser.CharType.None;
            }

            private enum ParserState
            {
                public const TimeSpanMaskFormatInfo.TimeSpanParser.ParserState Tabulator = TimeSpanMaskFormatInfo.TimeSpanParser.ParserState.Tabulator;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.ParserState Digit = TimeSpanMaskFormatInfo.TimeSpanParser.ParserState.Digit;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.ParserState String = TimeSpanMaskFormatInfo.TimeSpanParser.ParserState.String;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.ParserState Separator = TimeSpanMaskFormatInfo.TimeSpanParser.ParserState.Separator;
            }

            public class TimeSpanToken
            {
                public TimeSpanToken(string value, TimeSpanMaskFormatInfo.TimeSpanParser.TokenType type);

                public string Value { get; }

                public TimeSpanMaskFormatInfo.TimeSpanParser.TokenType Type { get; }
            }

            public enum TokenType
            {
                public const TimeSpanMaskFormatInfo.TimeSpanParser.TokenType Digit = TimeSpanMaskFormatInfo.TimeSpanParser.TokenType.Digit;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.TokenType Separator = TimeSpanMaskFormatInfo.TimeSpanParser.TokenType.Separator;,
                public const TimeSpanMaskFormatInfo.TimeSpanParser.TokenType String = TimeSpanMaskFormatInfo.TimeSpanParser.TokenType.String;
            }
        }
    }
}

