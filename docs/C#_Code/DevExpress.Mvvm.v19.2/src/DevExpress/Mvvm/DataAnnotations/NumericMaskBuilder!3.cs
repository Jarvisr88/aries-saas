namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Globalization;

    public class NumericMaskBuilder<T, TProperty, TParentBuilder> : MaskBuilderBase<T, TProperty, NumericMaskAttribute, NumericMaskBuilder<T, TProperty, TParentBuilder>, TParentBuilder> where TParentBuilder: PropertyMetadataBuilderBase<T, TProperty, TParentBuilder>
    {
        public NumericMaskBuilder(TParentBuilder parent) : base(parent)
        {
        }

        public NumericMaskBuilder<T, TProperty, TParentBuilder> MaskCulture(CultureInfo culture) => 
            base.ChangeAttribute(delegate (NumericMaskAttribute x) {
                x.CultureInfo = culture;
            });
    }
}

