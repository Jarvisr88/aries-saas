namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public abstract class CriteriaCompilerDescriptor
    {
        protected CriteriaCompilerDescriptor();
        public virtual CriteriaCompilerRefResult DiveIntoCollectionProperty(Expression baseExpression, string collectionPropertyPath);
        public static CriteriaCompilerDescriptor Get();
        public static CriteriaCompilerDescriptor Get<T>();
        public static CriteriaCompilerDescriptor Get(PropertyDescriptorCollection pds);
        public static CriteriaCompilerDescriptor Get(Type t);
        public static CriteriaCompilerDescriptor GetExpando();
        public virtual LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, Aggregate aggregateType, CriteriaOperator aggregateExpression, OperandParameter[] aggregateExpresssionParameters, Type[] invokeTypes);
        public virtual LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, string customAggregateName, IEnumerable<CriteriaOperator> aggregateExpressions, OperandParameter[] aggregateExpresssionsParameters, Type[] invokeTypes);
        public abstract Expression MakePropertyAccess(Expression baseExpression, string propertyPath);

        public abstract Type ObjectType { get; }
    }
}

