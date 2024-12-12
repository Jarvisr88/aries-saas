namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    public class RegExMaskBuilder<T, TProperty, TParentBuilder> : RegExMaskBuilderBase<T, TProperty, RegExMaskAttribute, RegExMaskBuilder<T, TProperty, TParentBuilder>, TParentBuilder> where TParentBuilder: PropertyMetadataBuilderBase<T, TProperty, TParentBuilder>
    {
        public RegExMaskBuilder(TParentBuilder parent) : base(parent)
        {
        }

        public RegExMaskBuilder<T, TProperty, TParentBuilder> MaskDoNotShowPlaceHolders()
        {
            Action<RegExMaskAttribute> action = <>c<T, TProperty, TParentBuilder>.<>9__1_0;
            if (<>c<T, TProperty, TParentBuilder>.<>9__1_0 == null)
            {
                Action<RegExMaskAttribute> local1 = <>c<T, TProperty, TParentBuilder>.<>9__1_0;
                action = <>c<T, TProperty, TParentBuilder>.<>9__1_0 = delegate (RegExMaskAttribute x) {
                    x.ShowPlaceHolders = false;
                };
            }
            return this.ChangeAttribute(action);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RegExMaskBuilder<T, TProperty, TParentBuilder>.<>c <>9;
            public static Action<RegExMaskAttribute> <>9__1_0;

            static <>c()
            {
                RegExMaskBuilder<T, TProperty, TParentBuilder>.<>c.<>9 = new RegExMaskBuilder<T, TProperty, TParentBuilder>.<>c();
            }

            internal void <MaskDoNotShowPlaceHolders>b__1_0(RegExMaskAttribute x)
            {
                x.ShowPlaceHolders = false;
            }
        }
    }
}

