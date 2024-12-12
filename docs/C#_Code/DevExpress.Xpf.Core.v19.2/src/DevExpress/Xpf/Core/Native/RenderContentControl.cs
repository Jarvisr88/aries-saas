namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Content")]
    public class RenderContentControl : RenderControl
    {
        public const string ContentPresenterName = "PART_ContentPresenter";
        protected internal static readonly RenderTemplate defaultTemplate;
        private RenderTemplateSelector renderContentTemplateSelector;
        private RenderTemplate renderContentTemplate;
        private HorizontalAlignment hca;
        private VerticalAlignment vca;
        private Thickness padding;
        private DataTemplate contentTemplate;
        private DataTemplateSelector contentTemplateSelector;
        private bool preferRenderTemplate;
        private object content;

        static RenderContentControl();
        public RenderContentControl();
        protected override object CalcContent(RenderControlContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        [IteratorStateMachine(typeof(RenderContentControl.<GetChildren>d__46))]
        protected override IEnumerable<IFrameworkRenderElement> GetChildren();
        protected RenderContentPresenterContext GetContentPresenter(RenderControlContext context);
        protected virtual FrameworkRenderElementContext GetPaddingTarget(RenderControlContext context);
        protected override void OnApplyTemplate(FrameworkRenderElementContext context);
        protected override void RenderOverride(DrawingContext dc, IFrameworkRenderElementContext context);

        public bool PreferRenderTemplate { get; set; }

        public RenderTemplateSelector RenderContentTemplateSelector { get; set; }

        public RenderTemplate RenderContentTemplate { get; set; }

        public DataTemplate ContentTemplate { get; set; }

        public DataTemplateSelector ContentTemplateSelector { get; set; }

        public HorizontalAlignment HorizontalContentAlignment { get; set; }

        public VerticalAlignment VerticalContentAlignment { get; set; }

        public Thickness Padding { get; set; }

        public object Content { get; set; }

        [CompilerGenerated]
        private sealed class <GetChildren>d__46 : IEnumerable<IFrameworkRenderElement>, IEnumerable, IEnumerator<IFrameworkRenderElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IFrameworkRenderElement <>2__current;
            private int <>l__initialThreadId;
            public RenderContentControl <>4__this;

            [DebuggerHidden]
            public <GetChildren>d__46(int <>1__state);
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

