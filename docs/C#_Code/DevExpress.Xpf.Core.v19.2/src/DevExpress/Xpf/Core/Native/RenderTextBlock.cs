namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class RenderTextBlock : FrameworkRenderElement
    {
        private static readonly char[] NewLineArray;
        public static bool DefaultAllowGlyphRunRendering;
        private static readonly RenderRealTextBlock RealTextBlockTemplate;
        private System.Windows.TextTrimming textTrimming;
        private System.Windows.TextWrapping textWrapping;
        private System.Windows.TextAlignment textAlignment;
        private TextDecorationCollection textDecorations;
        private bool? allowGlyphRunRendering;
        private string text;

        static RenderTextBlock();
        public RenderTextBlock();
        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected override bool CalcNeedToClipSlot(Size layoutSlotSize, FrameworkRenderElementContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        private static Typeface CreateTypeface(RenderTextBlockContext context, RenderTextBlockMode renderMode);
        [IteratorStateMachine(typeof(RenderTextBlock.<GetChildren>d__29))]
        protected override IEnumerable<IFrameworkRenderElement> GetChildren();
        private static void InitializeFormattedTextContainer(RenderTextBlockContext context);
        private static bool InitializeGlyphRunContainer(RenderTextBlockContext context);
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        protected override void OnApplyTemplate(FrameworkRenderElementContext context);
        protected override void PreApplyTemplate(FrameworkRenderElementContext context);
        protected override void RenderOverride(DrawingContext dc, IFrameworkRenderElementContext context);
        private bool ShouldUseFastTextBlock(RenderTextBlockContext tbContext, RenderFontSettings fontSettings);
        public static bool ShouldUseRealTextBlock(RenderTextBlockContext tb, string text, System.Windows.TextWrapping textWrapping, System.Windows.TextTrimming textTrimming, string highlightedText);

        public System.Windows.TextTrimming TextTrimming { get; set; }

        public System.Windows.TextWrapping TextWrapping { get; set; }

        public System.Windows.TextAlignment TextAlignment { get; set; }

        public TextDecorationCollection TextDecorations { get; set; }

        public bool? AllowGlyphRunRendering { get; set; }

        public string Text { get; set; }

        [CompilerGenerated]
        private sealed class <GetChildren>d__29 : IEnumerable<IFrameworkRenderElement>, IEnumerable, IEnumerator<IFrameworkRenderElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IFrameworkRenderElement <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetChildren>d__29(int <>1__state);
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

