namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Threading;

    public class Namescope : INamescope, IElementHost, IPropertyChangedListener
    {
        private static readonly Func<UIElement, bool> get_MeasureInProgress;
        private static readonly Func<UIElement, bool> get_ArrangeInProgress;
        private static readonly Func<UIElement, bool> get_MeasureDuringArrange;
        private readonly Dictionary<string, FrameworkRenderElementContext> dictionary;
        private readonly List<RenderControlBaseContext> children;
        private readonly DevExpress.Xpf.Core.Native.ValueChangedStorage valueChangedStorage;
        private readonly Queue<ChangeValueListenerTask> subscribeQueue;
        private readonly IChrome chrome;
        private readonly FrameworkElement chromeElement;
        private readonly List<Namescope> childrenScopes;
        private DispatcherOperation propagateDeferredActionsOperation;
        private bool notificationsSuspended;

        static Namescope();
        public Namescope();
        public Namescope(IChrome chrome);
        public Namescope(Namescope parent);
        public void AddChild(FrameworkRenderElementContext context);
        private void AddToTree(RenderControlBaseContext context);
        public void DetachFromParent();
        FrameworkElement IElementHost.GetChild(int index);
        void IElementHost.InvalidateArrange();
        void IElementHost.InvalidateMeasure();
        void IElementHost.InvalidateVisual();
        void IPropertyChangedListener.Resume();
        void IPropertyChangedListener.Suspend();
        public void Flush();
        public FrameworkRenderElementContext GetElement(string name);
        public void GoToState(string stateName);
        public void PropagateDeferredActions();
        public void PropagateDeferredActionsAsync();
        public void RegisterElement(FrameworkRenderElementContext context);
        private void RegisterElementInternal(FrameworkRenderElementContext context);
        public void ReleaseElement(FrameworkRenderElementContext element);
        private FrameworkRenderElementContext ReleaseElementInternal(string name);
        public void RemoveChild(FrameworkRenderElementContext context);
        public void RemoveFromTree(RenderControlBaseContext context);
        public void SubscribeValueChanged(object target, RenderPropertyChangedListenerContext context);
        public void SubscribeValueChangedAsync(RenderPropertyChangedListenerContext context);
        public void UnsubscribeValueChanged(object target, RenderPropertyChangedListenerContext context);

        private Namescope Parent { get; set; }

        protected List<Namescope> ChildrenScopes { get; }

        public IChrome Chrome { get; }

        public RenderTriggerContextCollection Triggers { get; set; }

        public FrameworkRenderElementContext RootElement { get; set; }

        public DevExpress.Xpf.Core.Native.ValueChangedStorage ValueChangedStorage { get; }

        int IElementHost.ChildrenCount { get; }

        IEnumerator IElementHost.LogicalChildren { get; }

        FrameworkElement IElementHost.TemplatedParent { get; }

        FrameworkElement IElementHost.Parent { get; }

        bool IElementHost.IsMeasureValid { get; }

        bool IElementHost.IsArrangeValid { get; }

        bool IElementHost.MeasureInProgress { get; }

        bool IElementHost.ArrangeInProgress { get; }

        bool IElementHost.MeasureDuringArrange { get; }

        double IElementHost.DpiScale { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Namescope.<>c <>9;
            public static Func<RenderControlBaseContext, FrameworkElement> <>9__49_0;

            static <>c();
            internal FrameworkElement <DevExpress.Xpf.Core.Native.IElementHost.get_LogicalChildren>b__49_0(RenderControlBaseContext child);
        }
    }
}

