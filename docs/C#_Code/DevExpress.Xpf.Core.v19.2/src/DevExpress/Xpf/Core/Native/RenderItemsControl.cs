namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RenderItemsControl : FrameworkRenderElement
    {
        private RenderTemplate itemTemplate;
        private RenderTemplateSelector itemTemplateSelector;
        private RenderPanel itemPanelTemplate;

        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected virtual void ClearContainerForItemOverride(RenderContentControlContext container);
        protected virtual RenderContentControl CreateContainerForItemOverride();
        protected override FrameworkRenderElementContext CreateContextInstance();
        [IteratorStateMachine(typeof(RenderItemsControl.<GetChildren>d__20))]
        protected override IEnumerable<IFrameworkRenderElement> GetChildren();
        private void InitializeItemsHost(RenderItemsControlContext context);
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        protected override void OnApplyTemplate(FrameworkRenderElementContext context);
        protected virtual void PrepareContainerForItemOverride(RenderItemsControlContext itemsControlContext, RenderContentControlContext context, object item);

        public RenderTemplate ItemTemplate { get; set; }

        public RenderTemplateSelector ItemTemplateSelector { get; set; }

        public RenderPanel ItemPanelTemplate { get; set; }

        [CompilerGenerated]
        private sealed class <GetChildren>d__20 : IEnumerable<IFrameworkRenderElement>, IEnumerable, IEnumerator<IFrameworkRenderElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IFrameworkRenderElement <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetChildren>d__20(int <>1__state);
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

