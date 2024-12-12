namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    internal class Token
    {
        public const int CONST = 0x101;
        public const int MINVALUECONST = 0x102;
        public const int AGG_EXISTS = 0x103;
        public const int AGG_COUNT = 260;
        public const int AGG_MIN = 0x105;
        public const int AGG_MAX = 0x106;
        public const int AGG_AVG = 0x107;
        public const int AGG_SUM = 0x108;
        public const int AGG_SINGLE = 0x109;
        public const int PARAM = 0x10a;
        public const int COL = 0x10b;
        public const int FN_ISNULL = 0x10c;
        public const int FN_IIF = 0x10d;
        public const int FUNCTION = 270;
        public const int SORT_ASC = 0x10f;
        public const int SORT_DESC = 0x110;
        public const int OR = 0x111;
        public const int AND = 0x112;
        public const int NOT = 0x113;
        public const int IS = 0x114;
        public const int NULL = 0x115;
        public const int OP_EQ = 0x116;
        public const int OP_NE = 0x117;
        public const int OP_LIKE = 280;
        public const int OP_GT = 0x119;
        public const int OP_LT = 0x11a;
        public const int OP_GE = 0x11b;
        public const int OP_LE = 0x11c;
        public const int OP_IN = 0x11d;
        public const int OP_BETWEEN = 0x11e;
        public const int NEG = 0x11f;
        public const int yyErrorCode = 0x100;
    }
}

