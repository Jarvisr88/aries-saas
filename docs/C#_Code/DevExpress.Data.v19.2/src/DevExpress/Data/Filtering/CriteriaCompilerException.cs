namespace DevExpress.Data.Filtering
{
    using System;

    public class CriteriaCompilerException : InvalidOperationException
    {
        public CriteriaCompilerException(string message, Exception innerException);
        public CriteriaCompilerException(CriteriaOperator wholeCriteria, CriteriaOperator exceptionCauseCriteria, Exception innerException);
        private static string MakeExceptionText(CriteriaOperator wholeCriteria, CriteriaOperator exceptionCauseCriteria, Exception innerException);
    }
}

