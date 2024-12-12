namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;

    public class FilteringPropertyMetadataBuilder<T, TProperty> : FilteringPropertyMetadataBuilderGeneric<T, TProperty, FilteringPropertyMetadataBuilder<T, TProperty>>
    {
        internal FilteringPropertyMetadataBuilder(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent) : base(storage, parent)
        {
        }
    }
}

