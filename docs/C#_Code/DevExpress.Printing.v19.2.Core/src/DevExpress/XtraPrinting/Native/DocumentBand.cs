namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DocumentBand : IDisposable, IListWrapper<PageBreakInfo>, IEnumerable<PageBreakInfo>, IEnumerable, IListWrapper<Brick>, IEnumerable<Brick>, IListWrapper<DocumentBand>, IEnumerable<DocumentBand>
    {
        public const float MaxBandHeightPix = 4294967f;
        public const int UndefinedFriendLevel = 0x7fffffff;
        private static object synch;
        private static int globalID;
        private static BitVector32.Section kindSection;
        private static BitVector32.Section printOnSection;
        private static BitVector32.Section cleanupStatusSection;
        private static readonly int bitHasDetailBands;
        private static readonly int bitKeepTogether;
        private static readonly int bitRepeatEveryPage;
        private static readonly int bitPrintAtBottom;
        private static readonly int bitKeepTogetherOnTheWHolePage;
        private static readonly int bitTopSpanActive;
        private static readonly int bitPrintAcrossBands;
        private DocumentBandCollection bands;
        private BrickList bricks;
        private PageBreakList pageBreaks;
        private DocumentBand parent;
        protected DocumentBand primarySource;
        private float topSpan;
        private float bottomSpan;
        private int rowIndex;
        private float fOffsetX;
        private BitVector32 flags;
        private int friendLevel;
        private int index;
        private static object[] emptyArray;

        static DocumentBand();
        public DocumentBand();
        public DocumentBand(DocumentBandKind kind);
        protected DocumentBand(DocumentBand source, int rowIndex);
        public DocumentBand(DocumentBandKind kind, int rowIndex);
        internal void AddBand(DocumentBand docBand);
        protected internal virtual void AfterBuild();
        public void ApplySpans();
        public void ApplySpans(float height);
        protected internal virtual void BeforeBuild();
        private void CalcMaxBottom(ref float bottom, float bottomSpan);
        private void CalcMinTop(ref float top, float topSpan);
        public virtual bool ContainsDetailBands();
        public static DocumentBand CreateEmptyInstance(DocumentBandKind kind);
        public static DocumentBand CreateEmptyMarginBand(DocumentBandKind kind, float height);
        public static DocumentBand CreatePageBreakBand(float pageBreak, CustomPageData nextPageData = null);
        public static DocumentBand CreateSpanBand(DocumentBandKind kind, float span);
        void IListWrapper<Brick>.Add(Brick item);
        void IListWrapper<Brick>.Clear();
        int IListWrapper<Brick>.IndexOf(Brick brick);
        void IListWrapper<Brick>.Insert(Brick brick, int index);
        void IListWrapper<Brick>.RemoveAt(int index);
        void IListWrapper<DocumentBand>.Add(DocumentBand item);
        void IListWrapper<DocumentBand>.Clear();
        int IListWrapper<DocumentBand>.IndexOf(DocumentBand band);
        void IListWrapper<DocumentBand>.Insert(DocumentBand band, int index);
        void IListWrapper<DocumentBand>.RemoveAt(int index);
        void IListWrapper<PageBreakInfo>.Add(PageBreakInfo item);
        void IListWrapper<PageBreakInfo>.Clear();
        int IListWrapper<PageBreakInfo>.IndexOf(PageBreakInfo pageBreak);
        void IListWrapper<PageBreakInfo>.Insert(PageBreakInfo pageBreak, int index);
        void IListWrapper<PageBreakInfo>.RemoveAt(int index);
        public virtual void Dispose();
        public void EnsureGroupFooter();
        public void EnsureReportFooterBand();
        public DocumentBand FindTopLevelBand();
        public void GenerateBandChildren();
        protected virtual void GenerateContent(DocumentBand source, int rowIndex);
        public DocumentBand GetBand(DocumentBandKind kind);
        public DocumentBand GetBand(PageBuildInfo pageBuildInfo);
        public DocumentBand GetBand(int index);
        public DocumentBand GetBand(int index, RectangleF bounds, PointF offset);
        public DocumentBand GetDataSourceRoot();
        public DocumentBand GetDetailBand();
        private static int GetGlobalID();
        public virtual DocumentBand GetInstance(int rowIndex);
        private static short GetMaxValue(Type enumType);
        public DocumentBand GetPageBand(DocumentBandKind kind);
        public int GetPageBreakIndex(float value);
        public DocumentBand GetPageFooterBand();
        public DocumentBand GetPageHeaderBand();
        public int GetRowIndex(int rowIndex);
        public DocumentBand GetSubRoot();
        public bool HasBands(RectangleF bounds, PointF offset);
        public bool HasReportHeader();
        internal void InsertBand(DocumentBand docBand, int index);
        public void InvalidateBrickBounds();
        public bool IsKindOf(DocumentBandKind kind);
        public bool IsKindOf(params DocumentBandKind[] kinds);
        public bool IsPageBand(DocumentBandKind kind);
        private bool IsReportHeaderOnPosition(int index);
        public virtual bool ProcessIsFinished(ProcessState processState, int bandIndex);
        internal void RemoveChildBand(int index);
        public virtual void Reset(bool shouldResetBricksOffset, bool pageBreaksActiveStatus);
        public virtual void Scale(double scaleFactor, Predicate<DocumentBand> shouldScale);
        protected void ScaleContent(double ratio);
        public void SetParents();
        public void SortBands(IComparer<DocumentBand> comparer);
        IEnumerator<Brick> IEnumerable<Brick>.GetEnumerator();
        IEnumerator<DocumentBand> IEnumerable<DocumentBand>.GetEnumerator();
        IEnumerator<PageBreakInfo> IEnumerable<PageBreakInfo>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
        internal static bool TryGetMultiColumnRootDocumentBand(DocumentBand docBand, out DocumentBand mcBand);

        public virtual object GroupKey { get; set; }

        public virtual bool IsGroupItem { get; }

        public IBandManager BandManager { get; set; }

        public int ID { get; private set; }

        public virtual DevExpress.XtraPrinting.Native.MultiColumn MultiColumn { get; set; }

        public int Index { get; internal set; }

        public int FriendLevel { get; set; }

        public bool IsFriendLevelSet { get; }

        public PrintOnPages PrintOn { get; set; }

        public bool HasDetailBands { get; set; }

        internal DocumentBand PrimarySource { get; }

        internal DocumentBand PrimaryParent { get; }

        internal float Top { get; }

        protected internal virtual float Bottom { get; }

        public virtual float TotalHeight { get; }

        internal float TotalTop { get; }

        internal float TotalBottom { get; }

        public virtual float SelfHeight { get; }

        public virtual float MinSelfHeight { get; }

        public DocumentBandKind Kind { get; set; }

        public IListWrapper<DocumentBand> Bands { get; }

        public virtual bool ShouldAssignParent { get; }

        public IListWrapper<Brick> Bricks { get; }

        public RectangleF BrickBounds { get; }

        public IListWrapper<PageBreakInfo> PageBreaks { get; }

        public DocumentBand Parent { get; set; }

        public bool IsLast { get; }

        public bool PrintAcrossBands { get; set; }

        public virtual bool UnderlieOtherBands { get; }

        public bool TopSpanActive { get; set; }

        public float TopSpan { get; set; }

        public float BottomSpan { get; set; }

        public int RowIndex { get; }

        public bool RepeatEveryPage { get; set; }

        public bool PrintAtBottom { get; set; }

        public bool IsMarginBand { get; }

        public virtual bool IsDetailBand { get; }

        public virtual bool IsRoot { get; }

        public bool KeepTogether { get; set; }

        public bool KeepTogetherOnTheWholePage { get; set; }

        public float OffsetX { get; set; }

        public bool IsEmpty { get; }

        public virtual bool HasDataSource { get; }

        public bool HasDetailBandBehaviour { get; }

        public bool IsValid { get; }

        internal DocumentBandCleanupStatus CleanupStatus { get; set; }

        public bool HasActivePageBreaks { get; }

        int IListWrapper<PageBreakInfo>.Count { get; }

        PageBreakInfo IListWrapper<PageBreakInfo>.this[int index] { get; }

        int IListWrapper<Brick>.Count { get; }

        Brick IListWrapper<Brick>.this[int index] { get; }

        int IListWrapper<DocumentBand>.Count { get; }

        DocumentBand IListWrapper<DocumentBand>.this[int index] { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentBand.<>c <>9;
            public static Func<DocumentBand, bool> <>9__151_0;
            public static Func<DocumentBand, DocumentBand> <>9__166_0;
            public static Func<DocumentBand, bool> <>9__180_0;

            static <>c();
            internal bool <GetDataSourceRoot>b__151_0(DocumentBand item);
            internal bool <GetSubRoot>b__180_0(DocumentBand item);
            internal DocumentBand <SortBands>b__166_0(DocumentBand x);
        }
    }
}

