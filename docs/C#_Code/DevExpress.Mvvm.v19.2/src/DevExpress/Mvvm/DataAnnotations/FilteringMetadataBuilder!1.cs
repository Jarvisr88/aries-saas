namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Linq.Expressions;

    public class FilteringMetadataBuilder<T> : ClassMetadataBuilder<T>
    {
        public DataFormLayoutBuilder<T, FilteringMetadataBuilder<T>> DataFormLayout() => 
            new DataFormLayoutBuilder<T, FilteringMetadataBuilder<T>>(this);

        public FilteringMetadataBuilder<T> DisplayName(string name) => 
            DisplayNameCore<FilteringMetadataBuilder<T>>((FilteringMetadataBuilder<T>) this, name);

        public GroupBuilder<T, FilteringMetadataBuilder<T>> Group(string groupName) => 
            new GroupBuilder<T, FilteringMetadataBuilder<T>>((FilteringMetadataBuilder<T>) this, groupName);

        public FilteringPropertyMetadataBuilder<T, TProperty> Property<TProperty>(Expression<Func<T, TProperty>> propertyExpression) => 
            base.FilteringPropertyCore<TProperty>(propertyExpression);
    }
}

