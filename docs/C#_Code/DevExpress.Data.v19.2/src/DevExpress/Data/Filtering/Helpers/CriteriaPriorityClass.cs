namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    public enum CriteriaPriorityClass
    {
        public const CriteriaPriorityClass Atom = CriteriaPriorityClass.Atom;,
        public const CriteriaPriorityClass Neg = CriteriaPriorityClass.Neg;,
        public const CriteriaPriorityClass Mul = CriteriaPriorityClass.Mul;,
        public const CriteriaPriorityClass Add = CriteriaPriorityClass.Add;,
        public const CriteriaPriorityClass BinaryNot = CriteriaPriorityClass.BinaryNot;,
        public const CriteriaPriorityClass BinaryAnd = CriteriaPriorityClass.BinaryAnd;,
        public const CriteriaPriorityClass BinaryXor = CriteriaPriorityClass.BinaryXor;,
        public const CriteriaPriorityClass BinaryOr = CriteriaPriorityClass.BinaryOr;,
        public const CriteriaPriorityClass InBetween = CriteriaPriorityClass.InBetween;,
        public const CriteriaPriorityClass CmpGt = CriteriaPriorityClass.CmpGt;,
        public const CriteriaPriorityClass CmpEq = CriteriaPriorityClass.CmpEq;,
        public const CriteriaPriorityClass IsNull = CriteriaPriorityClass.IsNull;,
        public const CriteriaPriorityClass Not = CriteriaPriorityClass.Not;,
        public const CriteriaPriorityClass And = CriteriaPriorityClass.And;,
        public const CriteriaPriorityClass Or = CriteriaPriorityClass.Or;
    }
}

