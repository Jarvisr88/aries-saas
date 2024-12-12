namespace DevExpress.Data.WcfLinq.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CriteriaToString : IClientCriteriaVisitor<string>, ICriteriaVisitor<string>
    {
        private readonly Type elementType;
        private readonly ElementDescriptor elementDescriptor;
        private static readonly Dictionary<char, string> escapeDict;
        private static readonly string[] alfabet;
        private int currentLevel;

        static CriteriaToString();
        public CriteriaToString(Type elementType);
        private static void AppendDateTime(StringBuilder sb, DateTime dt);
        public static string Convert<T>(CriteriaOperator criteria);
        public static string Convert(Type elementType, CriteriaOperator criteria);
        string IClientCriteriaVisitor<string>.Visit(OperandProperty theOperand);
        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator);
        private static string EscapeString(string str);
        private static void GetUnderType(ref Type leftType);
        private string MakeConcat(CriteriaOperatorCollection operands);
        public string Process(CriteriaOperator criteria);
        public static string TimeSpanToString(TimeSpan time);
        public string Visit(AggregateOperand theOperand);
        public string Visit(BetweenOperator theOperator);
        public string Visit(FunctionOperator theOperator);
        public string Visit(GroupOperator theOperator);
        public string Visit(InOperator theOperator);
        public string Visit(JoinOperand theOperand);
        public string Visit(OperandValue theOperand);
        public string Visit(UnaryOperator theOperator);
    }
}

