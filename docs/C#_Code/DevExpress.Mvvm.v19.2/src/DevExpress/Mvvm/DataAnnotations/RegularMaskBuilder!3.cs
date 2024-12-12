namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    public class RegularMaskBuilder<T, TProperty, TParentBuilder> : RegExMaskBuilderBase<T, TProperty, RegularMaskAttribute, RegularMaskBuilder<T, TProperty, TParentBuilder>, TParentBuilder> where TParentBuilder: PropertyMetadataBuilderBase<T, TProperty, TParentBuilder>
    {
        public RegularMaskBuilder(TParentBuilder parent) : base(parent)
        {
        }

        public RegularMaskBuilder<T, TProperty, TParentBuilder> MaskDoNotSaveLiteral()
        {
            Action<RegularMaskAttribute> action = <>c<T, TProperty, TParentBuilder>.<>9__1_0;
            if (<>c<T, TProperty, TParentBuilder>.<>9__1_0 == null)
            {
                Action<RegularMaskAttribute> local1 = <>c<T, TProperty, TParentBuilder>.<>9__1_0;
                action = <>c<T, TProperty, TParentBuilder>.<>9__1_0 = delegate (RegularMaskAttribute x) {
                    x.SaveLiteral = false;
                };
            }
            return this.ChangeAttribute(action);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RegularMaskBuilder<T, TProperty, TParentBuilder>.<>c <>9;
            public static Action<RegularMaskAttribute> <>9__1_0;

            static <>c()
            {
                RegularMaskBuilder<T, TProperty, TParentBuilder>.<>c.<>9 = new RegularMaskBuilder<T, TProperty, TParentBuilder>.<>c();
            }

            internal void <MaskDoNotSaveLiteral>b__1_0(RegularMaskAttribute x)
            {
                x.SaveLiteral = false;
            }
        }
    }
}

