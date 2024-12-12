namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class RenderImage : FrameworkRenderElement
    {
        private ImageSource source;
        private System.Windows.Media.Stretch stretch;
        private System.Windows.Controls.StretchDirection stretchDirection;

        public RenderImage();
        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        [IteratorStateMachine(typeof(RenderImage.<GetChildren>d__16))]
        protected override IEnumerable<IFrameworkRenderElement> GetChildren();
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        protected override void RenderOverride(DrawingContext dc, IFrameworkRenderElementContext context);

        public ImageSource Source { get; set; }

        public System.Windows.Media.Stretch Stretch { get; set; }

        public System.Windows.Controls.StretchDirection StretchDirection { get; set; }

        [CompilerGenerated]
        private sealed class <GetChildren>d__16 : IEnumerable<IFrameworkRenderElement>, IEnumerable, IEnumerator<IFrameworkRenderElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IFrameworkRenderElement <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetChildren>d__16(int <>1__state);
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

