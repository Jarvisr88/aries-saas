namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    public class SimpleMaskBuilder<T, TProperty, TParentBuilder> : RegExMaskBuilderBase<T, TProperty, SimpleMaskAttribute, SimpleMaskBuilder<T, TProperty, TParentBuilder>, TParentBuilder> where TParentBuilder: PropertyMetadataBuilderBase<T, TProperty, TParentBuilder>
    {
        public SimpleMaskBuilder(TParentBuilder parent) : base(parent)
        {
        }

        public SimpleMaskBuilder<T, TProperty, TParentBuilder> MaskDoNotSaveLiteral()
        {
            Action<SimpleMaskAttribute> action = <>c<T, TProperty, TParentBuilder>.<>9__1_0;
            if (<>c<T, TProperty, TParentBuilder>.<>9__1_0 == null)
            {
                Action<SimpleMaskAttribute> local1 = <>c<T, TProperty, TParentBuilder>.<>9__1_0;
                action = <>c<T, TProperty, TParentBuilder>.<>9__1_0 = delegate (SimpleMaskAttribute x) {
                    x.SaveLiteral = false;
                };
            }
            return this.ChangeAttribute(action);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SimpleMaskBuilder<T, TProperty, TParentBuilder>.<>c <>9;
            public static Action<SimpleMaskAttribute> <>9__1_0;

            static <>c()
            {
                SimpleMaskBuilder<T, TProperty, TParentBuilder>.<>c.<>9 = new SimpleMaskBuilder<T, TProperty, TParentBuilder>.<>c();
            }

            internal void <MaskDoNotSaveLiteral>b__1_0(SimpleMaskAttribute x)
            {
                x.SaveLiteral = false;
            }
        }
    }
}

