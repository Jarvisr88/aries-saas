namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class DescriptorWeakEventHandler<TOwner> : WeakEventHandler<TOwner, EventArgs, EventHandler> where TOwner: class
    {
        private static Func<WeakEventHandler<TOwner, EventArgs, EventHandler>, EventHandler> createHandler;
        private readonly PropertyDescriptor descriptor;

        static DescriptorWeakEventHandler()
        {
            DescriptorWeakEventHandler<TOwner>.createHandler = h => new EventHandler(h.OnEvent);
        }

        public DescriptorWeakEventHandler(PropertyDescriptor descriptor, TOwner owner, Action<TOwner, object, EventArgs> onEventAction) : base(owner, onEventAction, new Action<WeakEventHandler<TOwner, EventArgs, EventHandler>, object>(DescriptorWeakEventHandler<TOwner>.OnDetach), DescriptorWeakEventHandler<TOwner>.createHandler)
        {
            this.descriptor = descriptor;
        }

        public void Detach(object source)
        {
            if ((source != null) && ((base.Handler != null) && (this.Descriptor != null)))
            {
                this.Descriptor.RemoveValueChanged(source, base.Handler);
            }
        }

        private static void OnDetach(WeakEventHandler<TOwner, EventArgs, EventHandler> handler, object source)
        {
            DescriptorWeakEventHandler<TOwner> handler2 = handler as DescriptorWeakEventHandler<TOwner>;
            if (handler2 != null)
            {
                handler2.Detach(source);
            }
        }

        public PropertyDescriptor Descriptor =>
            this.descriptor;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DescriptorWeakEventHandler<TOwner>.<>c <>9;

            static <>c()
            {
                DescriptorWeakEventHandler<TOwner>.<>c.<>9 = new DescriptorWeakEventHandler<TOwner>.<>c();
            }

            internal EventHandler <.cctor>b__7_0(WeakEventHandler<TOwner, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);
        }
    }
}

