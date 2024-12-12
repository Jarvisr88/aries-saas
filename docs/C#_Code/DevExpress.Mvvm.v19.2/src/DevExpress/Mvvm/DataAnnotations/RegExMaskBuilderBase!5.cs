namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    public class RegExMaskBuilderBase<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder> : MaskBuilderBase<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder> where TMaskAttribute: RegExMaskAttributeBase, new() where TBuilder: RegExMaskBuilderBase<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder> where TParentBuilder: PropertyMetadataBuilderBase<T, TProperty, TParentBuilder>
    {
        public RegExMaskBuilderBase(TParentBuilder parent) : base(parent)
        {
        }

        public TBuilder MaskDoNotIgnoreBlank()
        {
            Action<TMaskAttribute> action = <>c<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder>.<>9__1_0;
            if (<>c<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder>.<>9__1_0 == null)
            {
                Action<TMaskAttribute> local1 = <>c<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder>.<>9__1_0;
                action = <>c<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder>.<>9__1_0 = delegate (TMaskAttribute x) {
                    x.IgnoreBlank = false;
                };
            }
            return this.ChangeAttribute(action);
        }

        public TBuilder MaskPlaceHolder(char placeHolder) => 
            base.ChangeAttribute(delegate (TMaskAttribute x) {
                x.PlaceHolder = placeHolder;
            });

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RegExMaskBuilderBase<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder>.<>c <>9;
            public static Action<TMaskAttribute> <>9__1_0;

            static <>c()
            {
                RegExMaskBuilderBase<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder>.<>c.<>9 = new RegExMaskBuilderBase<T, TProperty, TMaskAttribute, TBuilder, TParentBuilder>.<>c();
            }

            internal void <MaskDoNotIgnoreBlank>b__1_0(TMaskAttribute x)
            {
                x.IgnoreBlank = false;
            }
        }
    }
}

