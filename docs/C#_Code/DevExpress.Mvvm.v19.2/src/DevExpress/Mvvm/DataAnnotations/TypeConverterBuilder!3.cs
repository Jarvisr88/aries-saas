namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public class TypeConverterBuilder<T, TProperty, TParentBuilder> : TypeConverterBuilderBase<T, TProperty, TParentBuilder, TypeConverterBuilder<T, TProperty, TParentBuilder>> where TParentBuilder: PropertyMetadataBuilderBase<T, TProperty, TParentBuilder>
    {
        internal TypeConverterBuilder(TParentBuilder parent) : base(parent)
        {
        }
    }
}

