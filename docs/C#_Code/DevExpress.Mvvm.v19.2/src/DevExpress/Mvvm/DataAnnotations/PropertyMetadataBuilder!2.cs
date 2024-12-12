namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;

    public class PropertyMetadataBuilder<T, TProperty> : PropertyMetadataBuilderGeneric<T, TProperty, PropertyMetadataBuilder<T, TProperty>>
    {
        internal PropertyMetadataBuilder(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent) : base(storage, parent)
        {
        }
    }
}

