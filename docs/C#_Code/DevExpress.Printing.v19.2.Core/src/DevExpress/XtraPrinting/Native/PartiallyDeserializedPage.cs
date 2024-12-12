namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Caching;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class PartiallyDeserializedPage : PSPage
    {
        private static object locker = new object();

        public PartiallyDeserializedPage(ReadonlyPageData pageData)
        {
            base.PageData = pageData;
        }

        protected override void DrawPage(Graphics gr, PointF position)
        {
            if (this.Restored)
            {
                base.DrawPage(gr, position);
            }
            else
            {
                ((PartiallyDeserializedDocument) base.Document).QueueRestoringPage(this);
                PSPage page1 = new PSPage(base.PageData);
                page1.Owner = base.Owner;
                using (PSPage page = page1)
                {
                    using (GdiGraphics graphics = new GdiGraphics(gr, base.Document.PrintingSystem))
                    {
                        ((PageExporter) base.Document.PrintingSystem.ExportersFactory.GetExporter(page)).DrawPage(graphics, GraphicsUnitConverter.Convert(position, GraphicsUnit.Pixel, graphics.PageUnit));
                    }
                }
            }
        }

        protected override void IncProgressReflector()
        {
            ((PartiallyDeserializedDocument) base.Document).IncProgressReflector();
        }

        private bool NeedRestore(PartiallyDeserializedDocument document) => 
            !this.Restored;

        private void RestoreFromPrnx(DocumentStorage storage, PartiallyDeserializedDocument document, int streamIndex, Page page)
        {
            StreamingXmlSerializer serializer = new StreamingXmlSerializer(document.SerializationContext);
            using (Stream stream = storage.RestorePage(streamIndex))
            {
                page.PageData = document.PrintingSystem.PageSettings.Data;
                page.AssignWatermarkReference(new PageWatermark());
                DocumentSerializationCollection objects = new DocumentSerializationCollection();
                objects.Add(new XtraObjectInfo("Page", page));
                objects.Add(new XtraObjectInfo("PageData", page.PageData));
                objects.Add(new XtraObjectInfo("Watermark", page.Watermark));
                serializer.DeserializeObjects(new StreamingSerializationRootObject(), objects, stream, string.Empty, null);
            }
        }

        internal bool RestorePage()
        {
            bool flag2;
            PartiallyDeserializedDocument document = (PartiallyDeserializedDocument) base.Document;
            DocumentStorage storage = document.Storage;
            if (!this.NeedRestore(document))
            {
                return false;
            }
            object locker = PartiallyDeserializedPage.locker;
            lock (locker)
            {
                if (!this.NeedRestore(document))
                {
                    flag2 = false;
                }
                else
                {
                    ((PartiallyDeserializedInnerPageList) base.Owner.InnerList).Push(this);
                    PSPage page = new PSPage(base.PageData);
                    PrintingSystemBase printingSystem = document.PrintingSystem;
                    this.RestoreFromPrnx(storage, document, (base.Index < storage.PageDataList.Count) ? storage.PageDataList[base.Index].StreamIndex : base.Index, page);
                    base.AssignWatermarkReference(page.Watermark);
                    this.Restored = true;
                    foreach (BrickBase base3 in this.WithStylesFromPS(page.AllBricks(), printingSystem))
                    {
                        Brick brick = base3 as Brick;
                        if (brick != null)
                        {
                            brick.Initialize(printingSystem, brick.Rect, false);
                        }
                    }
                    this.InnerBricks.AddRange(page.InnerBricks);
                    document.UpdatedObjects.RestorePageBrickValues((Page) this, storage.PageDataList);
                    flag2 = true;
                }
            }
            return flag2;
        }

        private void SetStyleFromPS(Dictionary<BrickStyle, BrickStyle> replacedStyles, VisualBrick visualBrick, PrintingSystemBase ps)
        {
            BrickStyle style;
            BrickStyle key = visualBrick.Style;
            if (!replacedStyles.TryGetValue(key, out style))
            {
                style = ps.Styles.GetStyle(key);
                replacedStyles[key] = style;
            }
            visualBrick.Style = style;
        }

        [IteratorStateMachine(typeof(<WithStylesFromPS>d__16))]
        private IEnumerable<BrickBase> WithStylesFromPS(IEnumerable<BrickBase> bricks, PrintingSystemBase ps)
        {
            <WithStylesFromPS>d__16 d__1 = new <WithStylesFromPS>d__16(-2);
            d__1.<>4__this = this;
            d__1.<>3__bricks = bricks;
            d__1.<>3__ps = ps;
            return d__1;
        }

        public bool Restored { get; private set; }

        internal override IList InnerBrickList
        {
            get
            {
                this.RestorePage();
                return base.InnerBrickList;
            }
        }

        public override List<BrickBase> InnerBricks
        {
            get
            {
                this.RestorePage();
                return base.InnerBricks;
            }
        }

        [CompilerGenerated]
        private sealed class <WithStylesFromPS>d__16 : IEnumerable<BrickBase>, IEnumerable, IEnumerator<BrickBase>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private BrickBase <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<BrickBase> bricks;
            public IEnumerable<BrickBase> <>3__bricks;
            private BrickBase <item>5__1;
            public PartiallyDeserializedPage <>4__this;
            private Dictionary<BrickStyle, BrickStyle> <replacedStyles>5__2;
            private PrintingSystemBase ps;
            public PrintingSystemBase <>3__ps;
            private IEnumerator<BrickBase> <>7__wrap1;

            [DebuggerHidden]
            public <WithStylesFromPS>d__16(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<replacedStyles>5__2 = new Dictionary<BrickStyle, BrickStyle>();
                        this.<>7__wrap1 = this.bricks.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                        CheckBoxTextBrick brick = this.<item>5__1 as CheckBoxTextBrick;
                        if (brick == null)
                        {
                            VisualBrick visualBrick = this.<item>5__1 as VisualBrick;
                            if (visualBrick != null)
                            {
                                this.<>4__this.SetStyleFromPS(this.<replacedStyles>5__2, visualBrick, this.ps);
                            }
                        }
                        else
                        {
                            foreach (Brick brick2 in brick.Bricks)
                            {
                                VisualBrick visualBrick = brick2 as VisualBrick;
                                if (visualBrick != null)
                                {
                                    this.<>4__this.SetStyleFromPS(this.<replacedStyles>5__2, visualBrick, this.ps);
                                }
                            }
                        }
                        this.<item>5__1 = null;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        this.<item>5__1 = this.<>7__wrap1.Current;
                        this.<>2__current = this.<item>5__1;
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<BrickBase> IEnumerable<BrickBase>.GetEnumerator()
            {
                PartiallyDeserializedPage.<WithStylesFromPS>d__16 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new PartiallyDeserializedPage.<WithStylesFromPS>d__16(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.bricks = this.<>3__bricks;
                d__.ps = this.<>3__ps;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.XtraPrinting.BrickBase>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            BrickBase IEnumerator<BrickBase>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

