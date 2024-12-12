namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class CriteriaCompilerDescriptorITypedListComplete : CriteriaCompilerDescriptor
    {
        private readonly ITypedList Root;
        private readonly PropertyDescriptor[] ListAccessors;
        private PropertyDescriptorCollection _PDs;

        public CriteriaCompilerDescriptorITypedListComplete(ITypedList _Root, PropertyDescriptor[] _ListAccessors);
        public override CriteriaCompilerRefResult DiveIntoCollectionProperty(Expression baseExpression, string collectionPropertyPath);
        public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath);

        private PropertyDescriptorCollection PDs { get; }

        public override Type ObjectType { get; }
    }
}

