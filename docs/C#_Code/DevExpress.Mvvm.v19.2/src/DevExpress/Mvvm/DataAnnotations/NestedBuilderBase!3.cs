namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public abstract class NestedBuilderBase<TAttribute, TBuilder, TParentBuilder> where TAttribute: Attribute, new() where TBuilder: NestedBuilderBase<TAttribute, TBuilder, TParentBuilder> where TParentBuilder: IAttributeBuilderInternal
    {
        private readonly TParentBuilder parent;

        public NestedBuilderBase(TParentBuilder parent)
        {
            this.parent = parent;
        }

        protected TBuilder ChangeAttribute(Action<TAttribute> action)
        {
            this.parent.AddOrModifyAttribute<TAttribute>(action);
            return (TBuilder) this;
        }

        protected TParentBuilder EndCore() => 
            this.parent;
    }
}

