namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Linq.Expressions;

    public class GroupBuilder<T, TParentBuilder> where TParentBuilder: ClassMetadataBuilder<T>
    {
        private readonly TParentBuilder owner;
        private readonly string groupName;

        internal GroupBuilder(TParentBuilder owner, string groupName)
        {
            this.owner = owner;
            this.groupName = groupName;
        }

        public unsafe GroupBuilder<T, TParentBuilder> ContainsProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            this.owner.PropertyCore<TProperty>(propertyExpression).AddOrModifyAttribute<DisplayAttribute>(delegate (DisplayAttribute x) {
                x.GroupName = base.groupName;
                TParentBuilder* localPtr1 = &base.owner;
                int currentDisplayAttributeOrder = localPtr1.CurrentDisplayAttributeOrder;
                localPtr1.CurrentDisplayAttributeOrder = currentDisplayAttributeOrder + 1;
                x.Order = currentDisplayAttributeOrder;
            });
            return (GroupBuilder<T, TParentBuilder>) this;
        }

        public TParentBuilder EndGroup() => 
            this.owner;
    }
}

