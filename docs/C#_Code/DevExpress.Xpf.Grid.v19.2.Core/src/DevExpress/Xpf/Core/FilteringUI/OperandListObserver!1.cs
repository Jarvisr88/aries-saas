namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal class OperandListObserver<TOwner>
    {
        public OperandListObserver(Action<TOwner> onAdding, Action<TOwner> onRemoving)
        {
            this.<OnAdding>k__BackingField = onAdding;
            this.<OnRemoving>k__BackingField = onRemoving;
        }

        public static OperandListObserver<TOwner> Empty() => 
            new OperandListObserver<TOwner>(<>c<TOwner>.<>9__7_0 ??= delegate (TOwner _) {
            }, <>c<TOwner>.<>9__7_1 ??= delegate (TOwner __) {
            });

        public Action<TOwner> OnAdding { get; }

        public Action<TOwner> OnRemoving { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OperandListObserver<TOwner>.<>c <>9;
            public static Action<TOwner> <>9__7_0;
            public static Action<TOwner> <>9__7_1;

            static <>c()
            {
                OperandListObserver<TOwner>.<>c.<>9 = new OperandListObserver<TOwner>.<>c();
            }

            internal void <Empty>b__7_0(TOwner _)
            {
            }

            internal void <Empty>b__7_1(TOwner __)
            {
            }
        }
    }
}

