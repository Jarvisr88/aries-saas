namespace DevExpress.XtraPrinting.Native.Preview
{
    using DevExpress.DocumentView;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class SelectionService
    {
        private Point startMousePosition;
        private PointF startDocPosition;
        internal Point currentMousePosition;
        private bool leftButtonDown;
        private bool mouseMove;
        private bool canSelect;
        private bool multiSelect;
        private Page page;
        private List<Tuple<Brick, RectangleF>> selectedBricks;
        private readonly List<Tuple<Brick, RectangleF>> previousSelectedBricks;
        private RectangleF prevSelectionClientRectangle;
        private readonly Color selectionColorForBricks;
        private readonly Color selectionColor;
        private const int LengthsSideInsensitiveZone = 10;
        private readonly IViewControlController controller;

        internal SelectionService(IViewControlController viewControlLogic);
        internal void CopyToClipboard();
        private void CreateDocument(LinkBase link);
        private void DrawBricks(Page page, SolidBrush brush, Graphics gr, PointF position, List<Brick> bricks);
        private void FillSelectionRectangle(Graphics graph, RectangleF rect);
        public IPage FindPage(Rectangle rect);
        private DataObject GetClipboardData();
        internal PrintingSystemBase GetFakedPSWithSelection();
        internal bool HandleKillFocus();
        internal bool HandleLeftMouseDown();
        internal bool HandleLeftMouseUp();
        internal bool HandleMouseMove();
        internal bool HandleMouseWheel();
        public void InvalidateControl();
        internal void OnDrawPage(Page page, Graphics gr, PointF position);
        internal void OnSearchStarted();
        internal void ResetSelectedBricks();
        private void ResetService();
        public void SelectBrick(Page page, Brick brick);
        private void SetClipboardDataObject(DataObject dataObj);
        private static string StreamToString(Encoding encoding, MemoryStream stream);
        private void UpdateCommands();
        private void UpdateSelectedBricks();
        private void viewControlLogic_ViewControlPaint(Graphics graphics);

        private bool IsSelecting { get; }

        private bool CanSelect { get; set; }

        private RectangleF SelectionDocRectangle { get; }

        private Rectangle SelectionClientRectangle { get; }

        public bool HasSelection { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectionService.<>c <>9;
            public static Func<Tuple<Brick, RectangleF>, Brick> <>9__41_0;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__45_0;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__45_1;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__45_2;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__45_3;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__48_0;
            public static Func<Tuple<Brick, RectangleF>, float> <>9__48_1;

            static <>c();
            internal float <CreateDocument>b__48_0(Tuple<Brick, RectangleF> item);
            internal float <CreateDocument>b__48_1(Tuple<Brick, RectangleF> item);
            internal float <GetClipboardData>b__45_0(Tuple<Brick, RectangleF> x);
            internal float <GetClipboardData>b__45_1(Tuple<Brick, RectangleF> x);
            internal float <GetClipboardData>b__45_2(Tuple<Brick, RectangleF> x);
            internal float <GetClipboardData>b__45_3(Tuple<Brick, RectangleF> x);
            internal Brick <OnDrawPage>b__41_0(Tuple<Brick, RectangleF> x);
        }

        private class PageInfoExporter : TextBrickExporter
        {
            public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
            protected internal override void FillRtfTableCellInternal(IRtfExportProvider exportProvider);
            protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);

            public IPageItem PageItem { get; set; }

            private PageInfoTextBrickBase PageInfoBrick { get; }
        }

        private class PageItem : IPageItem
        {
            public int Index { get; set; }

            public int OriginalIndex { get; set; }

            public int OriginalPageCount { get; set; }

            public int PageCount { get; set; }
        }
    }
}

