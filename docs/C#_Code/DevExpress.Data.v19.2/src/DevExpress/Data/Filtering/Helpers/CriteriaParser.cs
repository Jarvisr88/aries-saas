namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class CriteriaParser
    {
        private CriteriaOperator[] result;
        private List<OperandValue> resultParameters;
        private int yyMax;
        private static short[] yyLhs;
        private static short[] yyLen;
        private static short[] yyDefRed;
        protected static short[] yyDgoto;
        protected static int yyFinal;
        protected static short[] yySindex;
        protected static short[] yyRindex;
        protected static short[] yyGindex;
        protected static short[] yyTable;
        protected static short[] yyCheck;

        static CriteriaParser();
        private CriteriaParser();
        public static string AugmentExceptionText(string exceptionMessage, string failedQuery, int failedLine, int failedCol);
        private static void CheckFunctionArgumentsCount(FunctionOperator theOperator, bool isImplicitCustomFunction);
        private static AggregateOperand MakeCustomAggregate(IList<CriteriaOperator> args, CriteriaOperator condition);
        private static CriteriaOperator MakeJoinOrCustomAggregate(OperandProperty collectionProperty, CriteriaOperator condition, string customAggregateName, IList<CriteriaOperator> args);
        public static CriteriaOperator[] Parse(CriteriaLexer _lexer, out OperandValue[] criteriaParametersList);
        public static CriteriaOperator Parse(string stringCriteria, out OperandValue[] criteriaParametersList);
        private static CriteriaOperator[] ParseCore(string query, out OperandValue[] criteriaParametersList, bool allowSorting);
        public static CriteriaOperator[] ParseList(string criteriaList, out OperandValue[] criteriaParametersList);
        public static CriteriaOperator[] ParseSortings(string sorting);
        private static void ThrowInvalidAggregateArgumentsCount(string aggregateName, int argumentsCount);
        public void yyerror(string message);
        public void yyerror(string message, string[] expected);
        private object yyparse(yyInput yyLex);

        public CriteriaOperator[] Result { get; }

        public List<OperandValue> ResultParameters { get; }
    }
}

