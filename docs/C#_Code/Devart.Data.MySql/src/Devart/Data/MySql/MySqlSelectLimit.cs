namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Text;

    public sealed class MySqlSelectLimit : SelectStatementNode
    {
        private Devart.Data.MySql.MySqlSelectLimit.a a;
        private Devart.Data.MySql.MySqlSelectLimit.a b;

        public MySqlSelectLimit();
        public MySqlSelectLimit(int offset, int count);
        public MySqlSelectLimit(string offset, string count);
        private MySqlSelectLimit(int A_0, int A_1, Devart.Data.MySql.MySqlSelectLimit.a A_2, Devart.Data.MySql.MySqlSelectLimit.a A_3);
        internal static MySqlSelectLimit a(Lexer A_0);
        private static bool a(Token A_0);
        internal void a(StringBuilder A_0, ref int A_1);
        private static void a(Lexer A_0, ref Token A_1, Devart.Data.MySql.MySqlSelectLimit.a A_2);
        private static void a(StringBuilder A_0, ref int A_1, Devart.Data.MySql.MySqlSelectLimit.a A_2, int A_3);
        public override string ToString();

        public object Offset { get; set; }

        public object Count { get; set; }

        private sealed class a : SelectStatementNode
        {
            private object a;

            public bool a();
            public void a(object A_0);
            public override string b();
            public object c();
        }
    }
}

