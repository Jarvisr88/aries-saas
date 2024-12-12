namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public abstract class MaskBuilderBase<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder> : NestedBuilderBase<TMaskAttribute, TBuilder, TParentBuilder> where TMaskAttribute: MaskAttributeBase, new() where TBuilder: MaskBuilderBase<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder> where TParentBuilder: PropertyMetadataBuilderBase<T, TProperty, TParentBuilder>
    {
        public MaskBuilderBase(TParentBuilder parent) : base(parent)
        {
        }

        public TParentBuilder EndMask() => 
            base.EndCore();

        internal TBuilder MaskCore(string mask, bool useMaskAsDisplayFormat) => 
            base.ChangeAttribute(delegate (TMaskAttribute x) {
                x.Mask = mask;
                x.UseAsDisplayFormat = useMaskAsDisplayFormat;
            });
    }
}

