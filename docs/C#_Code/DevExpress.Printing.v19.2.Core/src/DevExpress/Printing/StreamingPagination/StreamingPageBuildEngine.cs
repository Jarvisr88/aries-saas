namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class StreamingPageBuildEngine : IterativePageBuildEngine, IStreamingPageBuildEngine, IPageBuildEngine
    {
        private PSStreamingDocument streamingDocument;

        public StreamingPageBuildEngine(PSStreamingDocument streamingDocument) : base(streamingDocument)
        {
            this.streamingDocument = streamingDocument;
        }

        protected override void AfterBuildPage(PSPage page, RectangleF usefulPageArea)
        {
            page.OriginalIndex = this.BuiltPageCount;
            page.SetOwner(this.streamingDocument.Pages, this.BuiltPageCount);
            base.AfterBuildPage(page, usefulPageArea);
        }

        protected override bool CheckPages(int pageCount, YPageContentEngine pageContentEngine) => 
            (this.BuiltPageCount <= 0) || AddedBandsAreActual(pageContentEngine.AddedBands);

        [IteratorStateMachine(typeof(<EnumeratePages>d__4))]
        public IEnumerable<Page> EnumeratePages(IAfterPrintOnPageProcessor processor)
        {
            <EnumeratePages>d__4 d__1 = new <EnumeratePages>d__4(-2);
            d__1.<>4__this = this;
            d__1.<>3__processor = processor;
            return d__1;
        }

        protected override void StopIteration()
        {
            if (base.pageContentEngine != null)
            {
                base.pageContentEngine.ResetObservableItems();
            }
            base.CanIterate = false;
        }

        public int BuiltPageCount =>
            this.streamingDocument.BuiltPageCount;

        [CompilerGenerated]
        private sealed class <EnumeratePages>d__4 : IEnumerable<Page>, IEnumerable, IEnumerator<Page>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Page <>2__current;
            private int <>l__initialThreadId;
            public StreamingPageBuildEngine <>4__this;
            private int <lastBuiltPageIndex>5__1;
            private IAfterPrintOnPageProcessor processor;
            public IAfterPrintOnPageProcessor <>3__processor;

            [DebuggerHidden]
            public <EnumeratePages>d__4(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<lastBuiltPageIndex>5__1 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    goto TR_000B;
                }
            TR_0002:
                if (!this.<>4__this.CanIterate && (this.<lastBuiltPageIndex>5__1 >= this.<>4__this.streamingDocument.PageCount))
                {
                    return false;
                }
            TR_000B:
                while (true)
                {
                    if (this.<lastBuiltPageIndex>5__1 >= this.<>4__this.streamingDocument.PageCount)
                    {
                        this.<>4__this.streamingDocument.Pages.Clear();
                        this.<lastBuiltPageIndex>5__1 = 0;
                        if (this.<>4__this.CanIterate)
                        {
                            this.<>4__this.Iterate();
                        }
                        break;
                    }
                    int num2 = this.<lastBuiltPageIndex>5__1;
                    this.<lastBuiltPageIndex>5__1 = num2 + 1;
                    Page page = this.<>4__this.streamingDocument.Pages[num2];
                    this.processor.Process(page);
                    if (page.IsFake)
                    {
                        this.<>4__this.streamingDocument.Pages.RemoveAt(page.Index);
                    }
                    this.<>2__current = page;
                    this.<>1__state = 1;
                    return true;
                }
                goto TR_0002;
            }

            [DebuggerHidden]
            IEnumerator<Page> IEnumerable<Page>.GetEnumerator()
            {
                StreamingPageBuildEngine.<EnumeratePages>d__4 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new StreamingPageBuildEngine.<EnumeratePages>d__4(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.processor = this.<>3__processor;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.XtraPrinting.Page>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Page IEnumerator<Page>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

