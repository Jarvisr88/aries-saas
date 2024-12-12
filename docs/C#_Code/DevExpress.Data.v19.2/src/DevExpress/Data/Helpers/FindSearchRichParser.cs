namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class FindSearchRichParser
    {
        public const char OpEqualsSymbol = '=';
        public const char OpStartsWithSymbol = '^';
        public const char OpContainsSymbol = '*';
        public const char OpLikeSymbol = '~';
        public const char GroupAndSymbol = '+';
        public const char GroupOrSymbol = '?';
        public const char GroupNotSymbol = '-';
        public const char DefaultGroupSymbol = '+';
        public const char DefaultOpSymbol = '*';

        private static char CharFromFilterCondition(FilterCondition operation);
        private static FilterCondition FilterConditionFromChar(char opChar);
        private static bool IsBlank(char ch);
        private static bool IsGroupSymbol(char ch);
        private static bool IsOperationSymbol(char ch);
        [IteratorStateMachine(typeof(FindSearchRichParser.<Parse>d__19))]
        public static IEnumerable<FindSearchRichParser.RawToken> Parse(TextReader reader, char defaultGroupSymbol, char defaultOperationSymbol);
        public static FindSearchRichParser.Token[] Parse(string findPanelText, IEnumerable<IDataColumnInfo> columns, char defaultGroupSymbol, FilterCondition defaultOperation);
        private static bool PeekEofOrChar(TextReader reader, out char peekedChar);
        private static void ProcessEtc(TextReader reader, bool dontAcceptColon, StringBuilder builder);
        private static void ProcessQuoted(TextReader reader, StringBuilder builder);
        private static void ProcessSqBrFieldName(TextReader reader, StringBuilder builder);
        private static void SkipBlanks(TextReader reader);


        [StructLayout(LayoutKind.Sequential)]
        public struct RawToken
        {
            public char GroupingSymbol { get; }
            public char OperationSymbol { get; }
            public bool OperationForced { get; }
            public string Field { get; }
            public string Text { get; }
            public RawToken(string text, string field, char groupingSymbol, char opSymbol, bool opForced);
            public override string ToString();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Token
        {
            public char GroupingSymbol { get; }
            public FilterCondition Operation { get; }
            public bool OperationForced { get; }
            public IDataColumnInfo Field { get; }
            public string Text { get; }
            public Token(string text, IDataColumnInfo field, char groupingSymbol, FilterCondition op, bool opForced);
            public static FindSearchRichParser.Token FromRaw(FindSearchRichParser.RawToken raw, IEnumerable<IDataColumnInfo> columns);
        }

        public static class Tweaks
        {
            public static bool DisableAll;
            public static bool? DisableGroupSymbols;
            public static bool? DisableOpSymbols;
            public static bool? DisableSquareBracketsFields;
        }
    }
}

