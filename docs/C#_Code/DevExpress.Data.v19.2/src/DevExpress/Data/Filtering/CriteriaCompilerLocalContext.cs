namespace DevExpress.Data.Filtering
{
    using System;
    using System.Linq.Expressions;

    public class CriteriaCompilerLocalContext
    {
        public readonly System.Linq.Expressions.Expression Expression;
        public readonly CriteriaCompilerDescriptor Descriptor;

        public CriteriaCompilerLocalContext(System.Linq.Expressions.Expression expression, CriteriaCompilerDescriptor descriptor);
    }
}

