namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class RenderPropertyChangedListenerContext : RenderPropertyContextBase, IRenderConditionContext, IRenderPropertyContext
    {
        public RenderPropertyChangedListenerContext(RenderPropertyChangedListener factory);
        protected override void AttachOverride(Namescope scope, RenderTriggerContextBase context);
        protected override void DetachOverride();
        protected virtual object GetDescriptorSource(IElementHost elementHost);
        public object GetValue();
        public void InitializeDescriptor();
        protected virtual void InitializeDescriptorOverride();
        private void InitializeDescriptorSource();
        public virtual void PreviewValueChanged(object sender, EventArgs args);
        protected override void ResetOverride(RenderValueSource valueSource);
        public void SubscribeValue();
        protected void SubscribeValueChanged(object target);
        public void UnInitializeDescriptor();
        public void UnInitializeDescriptorSource();
        public void UnsubscribeValue();
        protected void UnsubscribeValueChanged(object target);
        public virtual void ValueChanged(object sender, EventArgs args);

        protected RenderPropertyChangedListener Factory { get; }

        public DependencyPropertyDescriptor Descriptor { get; private set; }

        public bool IsValid { get; protected set; }

        protected object CurrentDescriptorSource { get; private set; }

        protected string CurrentPropertyName { get; private set; }

        protected bool IsInitialized { get; private set; }

        public bool HasDescriptor { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderPropertyChangedListenerContext.<>c <>9;
            public static Func<FrameworkRenderElementContext, object> <>9__34_1;

            static <>c();
            internal object <GetDescriptorSource>b__34_1(FrameworkRenderElementContext x);
        }
    }
}

