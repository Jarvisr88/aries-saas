namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public abstract class PrintingDocument : Document, IDocumentProxy
    {
        public static readonly SizeF MinPageSize;
        private const string documentCreatingMessage = "This property can't be changed because document is creating.";
        private RootDocumentBand root;
        private float savedScaleFactor;
        private float scaleFactor;
        private int autoFitToPagesWidth;
        private DocumentHelper documentHelper;
        private bool buildPagesInBackground;
        private DevExpress.XtraPrinting.Native.NavigationInfo navigationInfo;
        private string infoString;
        private RectangleF usefulPageRectF;

        static PrintingDocument();
        protected PrintingDocument(PrintingSystemBase ps, RootDocumentBand root);
        protected internal override void AddBrick(Brick brick);
        protected internal virtual void AfterBuild();
        protected void AfterBuildPages();
        private void AfterPrintOnPages();
        protected internal override void Begin();
        public void BuildPages();
        protected override void Clear();
        public void CopyScaleParameters(PrintingDocument document);
        public virtual void CopyScaleParameters(float scaleFactor, int autoFitToPagesWidth);
        private DocumentHelper CreateDocumentHelper();
        protected virtual PageBuildEngine CreatePageBuildEngine(bool buildPagesInBackground, bool rollPaper);
        protected override PageList CreatePageList();
        void IDocumentProxy.AddPage(PSPage page);
        protected internal override void Dispose(bool disposing);
        private void DisposeDocumentHelper();
        protected internal override void End(bool buildPagesInBackground);
        protected override object[] GetAvailableExportModes(Type exportModeType);
        protected virtual double GetBricksRightToPageWidthRatio();
        protected internal override ContinuousExportInfo GetContinuousExportInfo();
        private static float? GetPageWidth(IListWrapper<PageBreakInfo> pageBreaks);
        protected internal override void HandleNewPageSettings();
        protected internal override void HandleNewScaleFactor();
        private static void SetNavigationPairs(IDictionary<string, IList<Brick>> links, IDictionary<string, BrickPagePair> targets);
        protected void SetRoot(RootDocumentBand root);
        private bool SetScaleFactor(float value);
        protected internal override void StopPageBuilding();

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string InfoString { get; set; }

        internal DevExpress.XtraPrinting.Native.NavigationInfo NavigationInfo { get; set; }

        public override int PageCount { get; }

        private IEnumerable<DocumentBand> AllBands { [IteratorStateMachine(typeof(PrintingDocument.<get_AllBands>d__20))] get; }

        public override int AutoFitToPagesWidth { get; set; }

        bool IDocumentProxy.SmartXDivision { get; }

        bool IDocumentProxy.SmartYDivision { get; }

        public DevExpress.XtraPrinting.VerticalContentSplitting VerticalContentSplitting { get; set; }

        public DevExpress.XtraPrinting.HorizontalContentSplitting HorizontalContentSplitting { get; set; }

        public bool BookmarkDuplicateSuppress { get; set; }

        private float AutoFitScaleFactor { get; }

        public override float ScaleFactor { get; set; }

        protected internal override RectangleF PageFootBounds { get; }

        protected internal override RectangleF PageHeadBounds { get; }

        protected internal override float MinPageHeight { get; }

        protected internal override float MinPageWidth { get; }

        public override bool CanPerformContinuousExport { get; }

        public RootDocumentBand Root { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintingDocument.<>c <>9;
            public static Comparison<EditingField> <>9__79_0;

            static <>c();
            internal int <AfterPrintOnPages>b__79_0(EditingField x, EditingField y);
        }

        [CompilerGenerated]
        private sealed class <get_AllBands>d__20 : IEnumerable<DocumentBand>, IEnumerable, IEnumerator<DocumentBand>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DocumentBand <>2__current;
            private int <>l__initialThreadId;
            public PrintingDocument <>4__this;
            private DocumentBandEnumerator <en>5__1;

            [DebuggerHidden]
            public <get_AllBands>d__20(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<DocumentBand> IEnumerable<DocumentBand>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            DocumentBand IEnumerator<DocumentBand>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

