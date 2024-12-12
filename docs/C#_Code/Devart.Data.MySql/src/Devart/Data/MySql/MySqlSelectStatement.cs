namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Text;

    public sealed class MySqlSelectStatement : SelectStatement
    {
        private MySqlSelectLimit a;
        private LexerBehavior b;
        private ArrayList c;
        private ArrayList d;
        private MySqlSelectOption e;
        private bool f;
        private const string g = "all";
        private const string h = "distinct";
        private const string i = "distinctrow";
        private const string j = "high_priority";
        private const string k = "straight_join";
        private const string l = "sql_big_result";
        private const string m = "sql_buffer_result";
        private const string n = "sql_cache";
        private const string o = "sql_calc_found_rows";
        private const string p = "sql_no_cache";
        private const string q = "sql_small_result";

        public MySqlSelectStatement();
        private void a();
        private static MySqlSelectOption a(int A_0);
        private static string a(MySqlSelectOption A_0, ArrayList A_1);
        internal static MySqlSelectStatement a(string A_0, ParserBehavior A_1, LexerBehavior A_2);
        internal static MySqlSelectStatement a(string A_0, ParserBehavior A_1, bool A_2);
        internal static MySqlSelectStatement a(string A_0, ParserBehavior A_1, LexerBehavior A_2, bool A_3);
        private static bool a(string A_0, ParserBehavior A_1, LexerBehavior A_2, bool A_3, bool A_4, out MySqlSelectStatement A_5);
        protected override bool Apply();
        private void b();
        protected override bool CheckId(int id);
        protected override bool CheckSemicolons();
        public override void Clear();
        protected override object CreateLexer();
        protected override bool IsJoinClause(object current);
        protected override bool IsLimitClause(object current);
        protected override bool IsSelectKeywordId(int id);
        protected override bool IsSelectOption(object token);
        protected override bool IsValidAlias(int tokenType);
        protected override bool IsValidIdentifierLastChar(char ch);
        public static MySqlSelectStatement Parse(string text, ParserBehavior behavior);
        protected override bool ParseClause(object lexerObj);
        protected override bool ParseCondition(object lexerObj, bool parseWhere);
        public static bool TryParse(string text, ParserBehavior behavior, out MySqlSelectStatement statement);
        private void UpdateSelectOptions(StringBuilder sql, ref int offset, ref int pos);

        public MySqlSelectLimit Limit { get; set; }

        internal MySqlSelectOption SelectOptions { get; set; }

        private class a : SelectStatementNode
        {
            public readonly MySqlSelectOption a;

            public a(MySqlSelectOption A_0);
        }
    }
}

