namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Child")]
    public class RenderDecorator : FrameworkRenderElement
    {
        private FrameworkRenderElement child;

        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        [IteratorStateMachine(typeof(RenderDecorator.<GetChildren>d__8))]
        protected override IEnumerable<IFrameworkRenderElement> GetChildren();
        protected override void InitializeContext(FrameworkRenderElementContext context);
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);

        public FrameworkRenderElement Child { get; set; }

        [CompilerGenerated]
        private sealed class <GetChildren>d__8 : IEnumerable<IFrameworkRenderElement>, IEnumerable, IEnumerator<IFrameworkRenderElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IFrameworkRenderElement <>2__current;
            private int <>l__initialThreadId;
            public RenderDecorator <>4__this;

            [DebuggerHidden]
            public <GetChildren>d__8(int <>1__state);
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

