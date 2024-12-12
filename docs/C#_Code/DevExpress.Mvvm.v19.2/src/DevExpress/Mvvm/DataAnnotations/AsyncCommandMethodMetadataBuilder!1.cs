namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class AsyncCommandMethodMetadataBuilder<T> : CommandMethodMetadataBuilderBase<T, AsyncCommandMethodMetadataBuilder<T>>
    {
        internal AsyncCommandMethodMetadataBuilder(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent, string methodName) : base(storage, parent, methodName)
        {
        }

        public AsyncCommandMethodMetadataBuilder<T> AllowMultipleExecution()
        {
            Action<CommandAttribute> setAttributeValue = <>c<T>.<>9__1_0;
            if (<>c<T>.<>9__1_0 == null)
            {
                Action<CommandAttribute> local1 = <>c<T>.<>9__1_0;
                setAttributeValue = <>c<T>.<>9__1_0 = delegate (CommandAttribute x) {
                    x.AllowMultipleExecutionCore = true;
                };
            }
            return this.AddOrModifyAttribute<CommandAttribute>(setAttributeValue);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncCommandMethodMetadataBuilder<T>.<>c <>9;
            public static Action<CommandAttribute> <>9__1_0;

            static <>c()
            {
                AsyncCommandMethodMetadataBuilder<T>.<>c.<>9 = new AsyncCommandMethodMetadataBuilder<T>.<>c();
            }

            internal void <AllowMultipleExecution>b__1_0(CommandAttribute x)
            {
                x.AllowMultipleExecutionCore = true;
            }
        }
    }
}

