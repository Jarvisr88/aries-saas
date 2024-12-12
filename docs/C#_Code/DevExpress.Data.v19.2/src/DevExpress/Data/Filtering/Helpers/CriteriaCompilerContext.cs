namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class CriteriaCompilerContext
    {
        protected CriteriaCompilerContext();
        public CriteriaCompilerRefResult DiveIntoCollectionProperty(string collectionPath);
        public abstract CriteriaCompilerLocalContext GetLocalContext(int upLevels);
        public LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, Aggregate aggregateType, CriteriaOperator aggregateExpression, OperandParameter[] aggregateExpresssionParameters, Type[] invokeTypes);
        public LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, string customAggregateName, IEnumerable<CriteriaOperator> aggregateExpressions, OperandParameter[] aggregateExpresssionsParameters, Type[] invokeTypes);
        public Expression MakePropertyAccess(string propertyPath);

        public abstract CriteriaCompilerAuxSettings AuxSettings { get; }
    }
}

