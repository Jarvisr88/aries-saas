namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RenderControl : FrameworkRenderElement
    {
        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected DevExpress.Xpf.Core.Native.RenderTemplate CalcActualRenderTemplate(RenderControlContext context);
        protected virtual object CalcContent(RenderControlContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        [IteratorStateMachine(typeof(RenderControl.<GetChildren>d__19))]
        protected override IEnumerable<IFrameworkRenderElement> GetChildren();
        protected override void InitializeContext(FrameworkRenderElementContext context);
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        protected override void OnApplyTemplate(FrameworkRenderElementContext context);

        public bool UsePropagatedIsMouseOver { get; set; }

        public DevExpress.Xpf.Core.Native.RenderTemplate RenderTemplate { get; set; }

        public DevExpress.Xpf.Core.Native.RenderTemplateSelector RenderTemplateSelector { get; set; }

        [CompilerGenerated]
        private sealed class <GetChildren>d__19 : IEnumerable<IFrameworkRenderElement>, IEnumerable, IEnumerator<IFrameworkRenderElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IFrameworkRenderElement <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetChildren>d__19(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<IFrameworkRenderElement> IEnumerable<IFrameworkRenderElement>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            IFrameworkRenderElement IEnumerator<IFrameworkRenderElement>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

