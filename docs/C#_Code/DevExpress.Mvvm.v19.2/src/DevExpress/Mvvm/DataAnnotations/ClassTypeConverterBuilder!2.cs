namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public class ClassTypeConverterBuilder<T, TParentBuilder> : ClassTypeConverterBuilderBase<T, TParentBuilder, ClassTypeConverterBuilder<T, TParentBuilder>> where TParentBuilder: MetadataBuilderBase<T, TParentBuilder>
    {
        internal ClassTypeConverterBuilder(TParentBuilder parent) : base(parent)
        {
        }
    }
}

