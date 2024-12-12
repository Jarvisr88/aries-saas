namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class RenderControlContext : FrameworkRenderElementContext
    {
        private DevExpress.Xpf.Core.Native.RenderTemplate renderTemplate;
        private DevExpress.Xpf.Core.Native.RenderTemplateSelector renderTemplateSelector;
        private bool isPressed;
        private bool isMouseOver;
        private bool isFocused;

        public RenderControlContext(RenderControl factory);
        public override void AddChild(FrameworkRenderElementContext child);
        protected override FrameworkRenderElementContext GetRenderChild(int index);
        public virtual void GoToState(string stateName);
        protected override void OnIsEnabledChanged(bool oldValue, bool newValue);
        protected override void OnIsMouseOverCoreChanged();
        protected override void OnMouseMove(MouseRenderEventArgs args);
        public override void Release();
        protected override void RenderSizeChanged();
        protected override void ResetTemplatesAndVisualsInternal();
        private void UpadteIsMouseOver();
        protected virtual void UpdateCommonState();
        protected virtual void UpdateFocusState();
        protected virtual void UpdateIsPressed();
        public virtual void UpdateStates();

        public DevExpress.Xpf.Core.Native.RenderTemplate ActualRenderTemplate { get; internal set; }

        protected override int RenderChildrenCount { get; }

        public DevExpress.Xpf.Core.Native.RenderTemplate RenderTemplate { get; set; }

        public DevExpress.Xpf.Core.Native.RenderTemplateSelector RenderTemplateSelector { get; set; }

        public bool IsFocused { get; set; }

        public bool IsMouseOver { get; set; }

        public bool IsPressed { get; set; }

        public FrameworkRenderElementContext Context { get; private set; }

        public Namescope InnerNamescope { get; internal set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderControlContext.<>c <>9;
            public static Action<FrameworkRenderElementContext> <>9__36_0;

            static <>c();
            internal void <ResetTemplatesAndVisualsInternal>b__36_0(FrameworkRenderElementContext x);
        }
    }
}

