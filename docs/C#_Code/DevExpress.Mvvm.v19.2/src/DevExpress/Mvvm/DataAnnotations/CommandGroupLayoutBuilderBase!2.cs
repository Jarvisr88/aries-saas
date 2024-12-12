namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Linq.Expressions;

    public abstract class CommandGroupLayoutBuilderBase<T, TBuilder> where TBuilder: CommandGroupLayoutBuilderBase<T, TBuilder>
    {
        protected readonly ClassMetadataBuilder<T> owner;

        internal CommandGroupLayoutBuilderBase(ClassMetadataBuilder<T> owner)
        {
            this.owner = owner;
        }

        public TBuilder ContainsCommand(Expression<Action<T>> methodExpression) => 
            this.ContainsCommandCore<CommandMethodMetadataBuilder<T>>(this.owner.CommandFromMethodInternal(methodExpression));

        public TBuilder ContainsCommand(Expression<Func<T, ICommand>> propertyExpression) => 
            this.ContainsCommandCore<CommandMetadataBuilder<T>>(this.owner.CommandCore(propertyExpression));

        protected abstract TBuilder ContainsCommandCore<TCommandBuilder>(CommandMetadataBuilderBase<T, TCommandBuilder> commandBuilder) where TCommandBuilder: CommandMetadataBuilderBase<T, TCommandBuilder>;
    }
}

