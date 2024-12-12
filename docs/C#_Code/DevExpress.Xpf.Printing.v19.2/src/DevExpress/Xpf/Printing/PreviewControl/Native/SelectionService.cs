namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.Preview;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Windows.Media;

    public class SelectionService
    {
        private const int LengthsSideInsensitiveZone = 10;
        private Point selectionRectangleStartPosition = Point.Empty;
        private Point selectionRectangleCurrentPosition = Point.Empty;
        private System.Drawing.Color selectionColorForBricks = System.Drawing.Color.FromArgb(180, SystemColors.Highlight);
        private bool leftButtonDown;
        private bool canSelect;
        private bool multiSelect;
        private readonly List<Pair<Page, RectangleF>> selectedPages = new List<Pair<Page, RectangleF>>();
        private List<Tuple<Brick, RectangleF>> selectedBricks = new List<Tuple<Brick, RectangleF>>();
        private readonly List<Tuple<Brick, RectangleF>> previousSelectedBricks = new List<Tuple<Brick, RectangleF>>();
        private readonly IPagesPresenter presenter;

        public event EventHandler InvalidatePage;

        public SelectionService(IPagesPresenter presenter)
        {
            this.presenter = presenter;
        }

        internal void CopyToClipboard()
        {
            if (this.selectedBricks.Any<Tuple<Brick, RectangleF>>())
            {
                Clipboard.Clear();
                Clipboard.SetDataObject(this.GetClipboardData());
            }
        }

        internal void CorrectStartPoint(double delta)
        {
            foreach (Pair<Page, RectangleF> pair in this.selectedPages)
            {
                RectangleF second = pair.Second;
                pair.Second = new RectangleF(new PointF(pair.Second.X, pair.Second.Y + ((float) delta)), second.Size);
            }
        }

        private RectangleF CorrectWithDPI(RectangleF rect)
        {
            double scaleX = (this.presenter as Visual).GetScaleX();
            return new RectangleF((float) (rect.X * scaleX), (float) (rect.Y * scaleX), (float) (rect.Width * scaleX), (float) (rect.Height * scaleX));
        }

        private void CreateDocument(LinkBase link)
        {
            link.CreateDetailArea += delegate (object s, CreateAreaEventArgs args) {
                float num = 0f;
                Func<Pair<Page, RectangleF>, Page> selector = <>c.<>9__41_1;
                if (<>c.<>9__41_1 == null)
                {
                    Func<Pair<Page, RectangleF>, Page> local1 = <>c.<>9__41_1;
                    selector = <>c.<>9__41_1 = x => x.First;
                }
                foreach (Page page in this.selectedPages.Select<Pair<Page, RectangleF>, Page>(selector))
                {
                    List<Tuple<Brick, RectangleF>> source = new List<Tuple<Brick, RectangleF>>();
                    BrickEnumerator enumerator = page.GetEnumerator();
                    Page[] bricks = new Page[] { page };
                    NestedBrickIterator iterator = new NestedBrickIterator(bricks);
                    while (true)
                    {
                        Func<Tuple<Brick, RectangleF>, bool> <>9__2;
                        if (!iterator.MoveNext())
                        {
                            if (source.Any<Tuple<Brick, RectangleF>>())
                            {
                                Func<Tuple<Brick, RectangleF>, float> func2 = <>c.<>9__41_3;
                                if (<>c.<>9__41_3 == null)
                                {
                                    Func<Tuple<Brick, RectangleF>, float> local3 = <>c.<>9__41_3;
                                    func2 = <>c.<>9__41_3 = item => item.Item2.Y;
                                }
                                float num2 = source.Min<Tuple<Brick, RectangleF>>(func2);
                                Func<Tuple<Brick, RectangleF>, float> func4 = <>c.<>9__41_4;
                                if (<>c.<>9__41_4 == null)
                                {
                                    Func<Tuple<Brick, RectangleF>, float> local4 = <>c.<>9__41_4;
                                    func4 = <>c.<>9__41_4 = item => item.Item2.X;
                                }
                                float num3 = source.Min<Tuple<Brick, RectangleF>>(func4);
                                foreach (Tuple<Brick, RectangleF> tuple2 in source.Reverse<Tuple<Brick, RectangleF>>())
                                {
                                    BrickWrapper brick = new BrickWrapper(tuple2.Item1);
                                    args.Graph.DrawBrick(brick, RectFBase.Offset(tuple2.Item2, -num3, -num2 + num));
                                }
                                Func<Tuple<Brick, RectangleF>, float> func5 = <>c.<>9__41_5;
                                if (<>c.<>9__41_5 == null)
                                {
                                    Func<Tuple<Brick, RectangleF>, float> local5 = <>c.<>9__41_5;
                                    func5 = <>c.<>9__41_5 = item => item.Item2.Bottom;
                                }
                                num += source.Max<Tuple<Brick, RectangleF>>(func5);
                            }
                            break;
                        }
                        Func<Tuple<Brick, RectangleF>, bool> predicate = <>9__2;
                        if (<>9__2 == null)
                        {
                            Func<Tuple<Brick, RectangleF>, bool> local2 = <>9__2;
                            predicate = <>9__2 = x => x.Item1 == iterator.CurrentBrick;
                        }
                        Tuple<Brick, RectangleF> tuple = this.selectedBricks.FirstOrDefault<Tuple<Brick, RectangleF>>(predicate);
                        if (tuple != null)
                        {
                            source.Add(tuple);
                        }
                    }
                }
            };
            link.CreateDocument();
        }

        private List<Tuple<Brick, RectangleF>> FindBricks(IEnumerable<Pair<Page, RectangleF>> pages)
        {
            List<Tuple<Brick, RectangleF>> list = new List<Tuple<Brick, RectangleF>>();
            if (pages.Any<Pair<Page, RectangleF>>())
            {
                foreach (Pair<Page, RectangleF> pair in pages)
                {
                    RectangleF pageRect = PSUnitConverter.PixelToDoc(pair.Second, this.Zoom);
                    if (!pageRect.IsEmpty)
                    {
                        list.AddRange(SelectedBrickIterator.GetSelectedBricks(pair.First, pageRect, PSUnitConverter.PixelToDoc(this.SelectionScreenRectangle, this.Zoom)));
                    }
                }
            }
            return list;
        }

        public IEnumerable<Pair<Page, RectangleF>> FindPages(Rectangle rect)
        {
            List<Pair<Page, RectangleF>> list = new List<Pair<Page, RectangleF>>();
            foreach (Pair<Page, RectangleF> pair in this.presenter.GetPages())
            {
                RectangleF second = pair.Second;
                if (second.IntersectsWith(rect))
                {
                    RectangleF ef2 = this.CorrectWithDPI(pair.Second);
                    list.Add(new Pair<Page, RectangleF>(pair.First, ef2));
                }
            }
            return list;
        }

        private DataObject GetClipboardData()
        {
            DataObject obj3;
            using (PrintingSystemBase base2 = new PrintingSystemBase())
            {
                base2.Graph.PageUnit = GraphicsUnit.Document;
                using (LinkBase base3 = new LinkBase(base2))
                {
                    Func<Tuple<Brick, RectangleF>, float> selector = <>c.<>9__40_0;
                    if (<>c.<>9__40_0 == null)
                    {
                        Func<Tuple<Brick, RectangleF>, float> local1 = <>c.<>9__40_0;
                        selector = <>c.<>9__40_0 = item => item.Item1.Rect.Left;
                    }
                    float num = this.selectedBricks.Min<Tuple<Brick, RectangleF>>(selector);
                    Func<Tuple<Brick, RectangleF>, float> func6 = <>c.<>9__40_1;
                    if (<>c.<>9__40_1 == null)
                    {
                        Func<Tuple<Brick, RectangleF>, float> local2 = <>c.<>9__40_1;
                        func6 = <>c.<>9__40_1 = item => item.Item1.Rect.Top;
                    }
                    float num2 = this.selectedBricks.Min<Tuple<Brick, RectangleF>>(func6);
                    float num3 = 0f;
                    float num4 = 0f;
                    Func<Pair<Page, RectangleF>, Page> func7 = <>c.<>9__40_2;
                    if (<>c.<>9__40_2 == null)
                    {
                        Func<Pair<Page, RectangleF>, Page> local3 = <>c.<>9__40_2;
                        func7 = <>c.<>9__40_2 = x => x.First;
                    }
                    foreach (Page page in this.selectedPages.Select<Pair<Page, RectangleF>, Page>(func7))
                    {
                        List<Tuple<Brick, RectangleF>> source = new List<Tuple<Brick, RectangleF>>();
                        BrickEnumerator enumerator = page.GetEnumerator();
                        Page[] bricks = new Page[] { page };
                        NestedBrickIterator iterator = new NestedBrickIterator(bricks);
                        while (true)
                        {
                            Func<Tuple<Brick, RectangleF>, bool> <>9__3;
                            if (!iterator.MoveNext())
                            {
                                if (source.Count != 0)
                                {
                                    Func<Tuple<Brick, RectangleF>, float> func2 = <>c.<>9__40_4;
                                    if (<>c.<>9__40_4 == null)
                                    {
                                        Func<Tuple<Brick, RectangleF>, float> local5 = <>c.<>9__40_4;
                                        func2 = <>c.<>9__40_4 = item => item.Item1.Rect.Right;
                                    }
                                    num3 = Math.Max(num3, source.Max<Tuple<Brick, RectangleF>>(func2));
                                    Func<Tuple<Brick, RectangleF>, float> func4 = <>c.<>9__40_5;
                                    if (<>c.<>9__40_5 == null)
                                    {
                                        Func<Tuple<Brick, RectangleF>, float> local6 = <>c.<>9__40_5;
                                        func4 = <>c.<>9__40_5 = item => item.Item1.Rect.Bottom;
                                    }
                                    num4 += source.Max<Tuple<Brick, RectangleF>>(func4);
                                }
                                break;
                            }
                            Func<Tuple<Brick, RectangleF>, bool> predicate = <>9__3;
                            if (<>9__3 == null)
                            {
                                Func<Tuple<Brick, RectangleF>, bool> local4 = <>9__3;
                                predicate = <>9__3 = x => x.Item1 == iterator.CurrentBrick;
                            }
                            Tuple<Brick, RectangleF> tuple = this.selectedBricks.FirstOrDefault<Tuple<Brick, RectangleF>>(predicate);
                            if (tuple != null)
                            {
                                source.Add(tuple);
                            }
                        }
                    }
                    base3.PaperKind = PaperKind.Custom;
                    base3.CustomPaperSize = Size.Ceiling(GraphicsUnitConverter.Convert(new SizeF(num3 - num, num4 - num2), (float) 300f, (float) 100f));
                    base3.MinMargins = new Margins(0, 0, 0, 0);
                    base3.Margins = new Margins(0, 0, 0, 0);
                    this.CreateDocument(base3);
                    DataObject obj2 = new DataObject();
                    Encoding encoding = Encoding.UTF8;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        base2.ExportOptions.Rtf.ExportMode = RtfExportMode.SingleFile;
                        base2.ExportOptions.Rtf.ExportToClipboard = true;
                        base2.ExportToRtf(stream);
                        string data = Encoding.Default.GetString(stream.GetBuffer(), 0, (int) stream.Length);
                        obj2.SetData(DataFormats.Rtf, data);
                    }
                    using (MemoryStream stream2 = new MemoryStream())
                    {
                        base2.ExportOptions.Text.TextExportMode = TextExportMode.Text;
                        base2.ExportOptions.Text.Encoding = encoding;
                        base2.ExportToText(stream2);
                        int length = encoding.GetPreamble().Length;
                        string data = encoding.GetString(stream2.GetBuffer(), length, ((int) stream2.Length) - length);
                        obj2.SetData(DataFormats.Text, data);
                    }
                    using (MemoryStream stream3 = new MemoryStream())
                    {
                        base2.ExportOptions.Image.ExportMode = ImageExportMode.SingleFile;
                        base2.ExportToImage(stream3, ImageFormat.Bmp);
                        Bitmap data = new Bitmap(stream3);
                        obj2.SetData(DataFormats.Bitmap, data);
                    }
                    obj3 = obj2;
                }
            }
            return obj3;
        }

        internal PrintingSystemBase GetFakedPSWithSelection()
        {
            if (!this.selectedBricks.Any<Tuple<Brick, RectangleF>>())
            {
                return null;
            }
            PrintingSystemBase ps = new PrintingSystemBase {
                Graph = { PageUnit = GraphicsUnit.Document }
            };
            Page first = this.selectedPages.FirstOrDefault<Pair<Page, RectangleF>>().First;
            using (LinkBase base3 = new LinkBase(ps))
            {
                base3.PaperKind = first.PageData.PaperKind;
                if (base3.PaperKind == PaperKind.Custom)
                {
                    base3.CustomPaperSize = GraphicsUnitConverter.Convert(first.PageData.PageSize.ToSize(), (float) 300f, (float) 100f);
                }
                base3.MinMargins = new Margins(0, 0, 0, 0);
                base3.Margins = first.PageData.Margins;
                base3.Landscape = first.PageData.Landscape;
                base3.PaperName = first.PageData.PaperName;
                this.CreateDocument(base3);
            }
            return ps;
        }

        internal void OnDrawPage(Page page, Graphics gr, PointF position)
        {
            if ((this.selectedPages != null) && (this.selectedPages.FirstOrDefault<Pair<Page, RectangleF>>(x => (x.First == page)) != null))
            {
                using (SolidBrush brush = new SolidBrush(this.selectionColorForBricks))
                {
                    Func<Tuple<Brick, RectangleF>, Brick> selector = <>c.<>9__38_1;
                    if (<>c.<>9__38_1 == null)
                    {
                        Func<Tuple<Brick, RectangleF>, Brick> local1 = <>c.<>9__38_1;
                        selector = <>c.<>9__38_1 = item => item.Item1;
                    }
                    List<Brick> bricks = this.selectedBricks.Select<Tuple<Brick, RectangleF>, Brick>(selector).ToList<Brick>();
                    BrickNavigator navigator1 = new BrickNavigator(page, false, true);
                    navigator1.BrickPosition = position;
                    navigator1.ClipRect = page.DeflateMinMargins(page.GetRect(position));
                    navigator1.IterateBricks(delegate (Brick brick, RectangleF brickRect, RectangleF brickClipRect) {
                        if (bricks.Remove(brick))
                        {
                            gr.FillRectangle(brush, RectangleF.Intersect(brickRect, brickClipRect));
                        }
                        return bricks.Count == 0;
                    });
                }
            }
        }

        public bool OnMouseDown(Point cursorLocation, MouseButtons mouseButtons, Keys keys)
        {
            if (keys.HasFlag(Keys.Shift) || keys.HasFlag(Keys.Control))
            {
                this.multiSelect = true;
            }
            else
            {
                this.ResetSelectedBricks();
                this.multiSelect = false;
            }
            this.leftButtonDown = true;
            double scaleX = (this.presenter as Visual).GetScaleX();
            this.selectionRectangleStartPosition = this.selectionRectangleCurrentPosition = new Point((int) (cursorLocation.X * scaleX), (int) (cursorLocation.Y * ((float) scaleX)));
            return false;
        }

        public bool OnMouseMove(Point cursorLocation, MouseButtons mouseButtons, Keys keys)
        {
            double scaleX = (this.presenter as Visual).GetScaleX();
            this.selectionRectangleCurrentPosition = new Point((int) (cursorLocation.X * scaleX), (int) (cursorLocation.Y * ((float) scaleX)));
            if (!this.leftButtonDown || !this.CanSelect)
            {
                return false;
            }
            this.UpdateSelectedBricks();
            return true;
        }

        public bool OnMouseUp(Point cursorLocation, MouseButtons mouseButtons, Keys keys)
        {
            this.previousSelectedBricks.Clear();
            this.previousSelectedBricks.AddRange(this.selectedBricks);
            this.ResetService();
            return false;
        }

        private void RaiseInvalidate()
        {
            if (this.InvalidatePage == null)
            {
                EventHandler invalidatePage = this.InvalidatePage;
            }
            else
            {
                this.InvalidatePage(this, EventArgs.Empty);
            }
        }

        public void ResetAll()
        {
            this.ResetSelectedBricks();
            this.ResetService();
            this.RaiseInvalidate();
        }

        internal void ResetSelectedBricks()
        {
            this.selectedPages.Clear();
            bool flag = this.selectedBricks.Any<Tuple<Brick, RectangleF>>();
            this.selectedBricks.Clear();
            this.previousSelectedBricks.Clear();
            if (flag)
            {
                this.RaiseInvalidate();
            }
        }

        private void ResetService()
        {
            this.selectionRectangleStartPosition = Point.Empty;
            this.multiSelect = false;
            this.CanSelect = false;
            this.leftButtonDown = false;
        }

        internal void SelectBrick(Page page, Brick brick)
        {
            this.selectedPages.Clear();
            this.selectedPages.Add(new Pair<Page, RectangleF>(page, RectangleF.Empty));
            this.selectedBricks.Clear();
            this.selectedBricks.Add(new Tuple<Brick, RectangleF>(brick, brick.Rect));
        }

        internal void SetStartPoint(Point point)
        {
            this.selectionRectangleStartPosition = point;
        }

        private void UpdateSelectedBricks()
        {
            IEnumerable<Pair<Page, RectangleF>> source = this.FindPages(this.SelectionScreenRectangle);
            if (source.Any<Pair<Page, RectangleF>>())
            {
                source.ForEach<Pair<Page, RectangleF>>(delegate (Pair<Page, RectangleF> x) {
                    Pair<Page, RectangleF> item = this.selectedPages.FirstOrDefault<Pair<Page, RectangleF>>(page => page.First.Index == x.First.Index);
                    if (item != null)
                    {
                        this.selectedPages.Remove(item);
                    }
                    this.selectedPages.Add(x);
                });
                List<Tuple<Brick, RectangleF>> selectedBricks = this.selectedBricks;
                List<Tuple<Brick, RectangleF>> newSelectedBricks = this.FindBricks(this.selectedPages);
                if (this.multiSelect)
                {
                    List<Tuple<Brick, RectangleF>> collection = (from c in this.previousSelectedBricks
                        where !newSelectedBricks.Any<Tuple<Brick, RectangleF>>(any => (any.Item1 == c.Item1))
                        select c).ToList<Tuple<Brick, RectangleF>>();
                    List<Tuple<Brick, RectangleF>> list3 = (from c in newSelectedBricks
                        where this.previousSelectedBricks.Any<Tuple<Brick, RectangleF>>(any => any.Item1 == c.Item1)
                        select c).ToList<Tuple<Brick, RectangleF>>();
                    list3.AddRange(from c in selectedBricks
                        where !newSelectedBricks.Any<Tuple<Brick, RectangleF>>(any => (any.Item1 == c.Item1)) && !this.previousSelectedBricks.Any<Tuple<Brick, RectangleF>>(any => (any.Item1 == c.Item1))
                        select c);
                    foreach (Tuple<Brick, RectangleF> tuple in list3)
                    {
                        newSelectedBricks.Remove(tuple);
                    }
                    newSelectedBricks.AddRange(collection);
                }
                this.selectedBricks = newSelectedBricks;
                this.RaiseInvalidate();
            }
        }

        [DefaultValue((float) 1f)]
        public float Zoom { get; set; }

        public System.Drawing.Color SelectionColor
        {
            get => 
                this.selectionColorForBricks;
            set => 
                this.selectionColorForBricks = value;
        }

        internal bool CanSelect
        {
            get
            {
                if (!this.canSelect && (this.leftButtonDown && ((Math.Abs((int) (this.selectionRectangleCurrentPosition.X - this.selectionRectangleStartPosition.X)) > 10) || (Math.Abs((int) (this.selectionRectangleCurrentPosition.Y - this.selectionRectangleStartPosition.Y)) > 10))))
                {
                    this.canSelect = true;
                }
                return this.canSelect;
            }
            set => 
                this.canSelect = value;
        }

        public bool HasSelection =>
            this.selectedBricks.Count > 0;

        private Rectangle SelectionScreenRectangle =>
            Rectangle.Round(RectFBase.FromPoints((PointF) this.selectionRectangleStartPosition, (PointF) this.selectionRectangleCurrentPosition));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService.<>c <>9 = new DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService.<>c();
            public static Func<Tuple<Brick, RectangleF>, Brick> <>9__38_1;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__40_0;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__40_1;
            public static Func<Pair<Page, RectangleF>, Page> <>9__40_2;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__40_4;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__40_5;
            public static Func<Pair<Page, RectangleF>, Page> <>9__41_1;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__41_3;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__41_4;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__41_5;

            internal Page <CreateDocument>b__41_1(Pair<Page, RectangleF> x) => 
                x.First;

            internal float <CreateDocument>b__41_3(Tuple<Brick, RectangleF> item) => 
                item.Item2.Y;

            internal float <CreateDocument>b__41_4(Tuple<Brick, RectangleF> item) => 
                item.Item2.X;

            internal float <CreateDocument>b__41_5(Tuple<Brick, RectangleF> item) => 
                item.Item2.Bottom;

            internal float <GetClipboardData>b__40_0(Tuple<Brick, RectangleF> item) => 
                item.Item1.Rect.Left;

            internal float <GetClipboardData>b__40_1(Tuple<Brick, RectangleF> item) => 
                item.Item1.Rect.Top;

            internal Page <GetClipboardData>b__40_2(Pair<Page, RectangleF> x) => 
                x.First;

            internal float <GetClipboardData>b__40_4(Tuple<Brick, RectangleF> item) => 
                item.Item1.Rect.Right;

            internal float <GetClipboardData>b__40_5(Tuple<Brick, RectangleF> item) => 
                item.Item1.Rect.Bottom;

            internal Brick <OnDrawPage>b__38_1(Tuple<Brick, RectangleF> item) => 
                item.Item1;
        }
    }
}

