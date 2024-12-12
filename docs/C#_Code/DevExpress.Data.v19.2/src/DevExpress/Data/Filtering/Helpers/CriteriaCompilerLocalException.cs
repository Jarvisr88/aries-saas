namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class CriteriaCompilerLocalException : Exception
    {
        public CriteriaOperator Cause;

        public CriteriaCompilerLocalException(Exception innerException, CriteriaOperator cause);
    }
}

