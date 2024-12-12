namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Linq.Expressions;

    public class DefaultTopLevelCriteriaCompilerContextDescriptor<T> : CriteriaCompilerDescriptor
    {
        public override CriteriaCompilerRefResult DiveIntoCollectionProperty(Expression baseExpression, string collectionName);
        public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath);

        public override Type ObjectType { get; }
    }
}

