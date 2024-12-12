namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public abstract class CommandMethodMetadataBuilderBase<T, TBuilder> : CommandMetadataBuilderBase<T, TBuilder> where TBuilder: CommandMethodMetadataBuilderBase<T, TBuilder>
    {
        private readonly string methodName;

        internal CommandMethodMetadataBuilderBase(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent, string methodName) : base(storage, parent)
        {
            this.methodName = methodName;
        }

        public TBuilder CanExecuteMethod(Expression<Func<T, bool>> canExecuteMethodExpression) => 
            base.AddOrModifyAttribute<CommandAttribute>(delegate (CommandAttribute x) {
                x.CanExecuteMethod = ExpressionHelper.GetArgumentFunctionStrict<T, bool>(canExecuteMethodExpression);
            });

        public TBuilder CommandName(string commandName) => 
            base.AddOrModifyAttribute<CommandAttribute>(delegate (CommandAttribute x) {
                x.Name = commandName;
            });

        public TBuilder DoNotCreateCommand() => 
            base.AddOrReplaceAttribute<CommandAttribute>(new CommandAttribute(false));

        public TBuilder DoNotUseCommandManager()
        {
            Action<CommandAttribute> setAttributeValue = <>c<T, TBuilder>.<>9__5_0;
            if (<>c<T, TBuilder>.<>9__5_0 == null)
            {
                Action<CommandAttribute> local1 = <>c<T, TBuilder>.<>9__5_0;
                setAttributeValue = <>c<T, TBuilder>.<>9__5_0 = delegate (CommandAttribute x) {
                    x.UseCommandManager = false;
                };
            }
            return this.AddOrModifyAttribute<CommandAttribute>(setAttributeValue);
        }

        public TBuilder UseMethodNameAsCommandName() => 
            this.CommandName(this.methodName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CommandMethodMetadataBuilderBase<T, TBuilder>.<>c <>9;
            public static Action<CommandAttribute> <>9__5_0;

            static <>c()
            {
                CommandMethodMetadataBuilderBase<T, TBuilder>.<>c.<>9 = new CommandMethodMetadataBuilderBase<T, TBuilder>.<>c();
            }

            internal void <DoNotUseCommandManager>b__5_0(CommandAttribute x)
            {
                x.UseCommandManager = false;
            }
        }
    }
}

