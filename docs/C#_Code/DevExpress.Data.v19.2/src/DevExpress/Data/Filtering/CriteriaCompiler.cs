namespace DevExpress.Data.Filtering
{
    using System;
    using System.Linq.Expressions;

    public static class CriteriaCompiler
    {
        private static Func<T, bool> ToBoolLambda<T>(LambdaExpression lambda);
        public static LambdaExpression ToLambda(CriteriaOperator expression, CriteriaCompilerDescriptor descriptor);
        public static LambdaExpression ToLambda(CriteriaOperator expression, CriteriaCompilerDescriptor descriptor, CriteriaCompilerAuxSettings settings);
        public static Func<T, bool> ToPredicate<T>(CriteriaOperator expression, CriteriaCompilerDescriptor descriptor);
        public static Func<T, bool> ToPredicate<T>(CriteriaOperator expression, CriteriaCompilerDescriptor descriptor, CriteriaCompilerAuxSettings settings);
        public static Func<object, object> ToUntypedDelegate(CriteriaOperator expression, CriteriaCompilerDescriptor descriptor);
        public static Func<object, object> ToUntypedDelegate(CriteriaOperator expression, CriteriaCompilerDescriptor descriptor, CriteriaCompilerAuxSettings settings);
        public static Func<object, bool> ToUntypedPredicate(CriteriaOperator expression, CriteriaCompilerDescriptor descriptor);
        public static Func<object, bool> ToUntypedPredicate(CriteriaOperator expression, CriteriaCompilerDescriptor descriptor, CriteriaCompilerAuxSettings settings);
    }
}

