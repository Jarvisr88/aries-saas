namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class StreamingExportPageBuildEngine : ExportPageBuildEngine, IStreamingExportPageBuildEngine, IPageBuildEngine
    {
        private PSPage psPage;

        public StreamingExportPageBuildEngine(PrintingDocument document) : base(document)
        {
        }

        private void BeforeFillPage()
        {
            base.BuildPages(base.Root);
            this.psPage = this.CreatePage(base.ActualPageSizeF);
            YPageContentEngine contentEngine = this.CreateContentEngine(this.psPage, null);
            this.InitializeContentEngine(contentEngine);
            base.rowBuilder = this.CreatePageRowBuilder(contentEngine);
            base.rowBuilder.BeforeFillPage(contentEngine);
            this.StreamingContentEngine.ContinuousInfo.StartCollecting();
        }

        protected override void Build()
        {
        }

        protected override YPageContentEngine CreateContentEngine(PSPage psPage, YPageContentEngine previous)
        {
            ExportPageBuildEngine.ExportContentEngine engine;
            base.contentEngine = engine = new StreamingExportContentEngine(psPage, this, base.PrintingSystem);
            return engine;
        }

        public override ContinuousExportInfo CreateContinuousExportInfo() => 
            (ContinuousExportInfo) this.ContinuousInfo;

        protected override PageRowBuilder CreatePageRowBuilder(YPageContentEngine contentEngine)
        {
            StreamingPageRowBuilder builder1 = new StreamingPageRowBuilder(contentEngine);
            builder1.CanApplyPageBreaks = false;
            return builder1;
        }

        void IStreamingExportPageBuildEngine.AfterFillPage(DocumentBand root)
        {
            try
            {
                BuildEngineService serviceInstance = new BuildEngineService();
                serviceInstance.BuildEngineID = 1;
                base.PrintingSystem.AddService(typeof(IBuildEngineService), serviceInstance);
                this.StreamingRowBuilder.FillPageFooter(root, this.psPage.Rect);
                this.StreamingContentEngine.ContinuousInfo.EndCollecting();
                base.PrintingSystem.OnAfterPagePrint(new PageEventArgs(null, null));
            }
            finally
            {
                base.PrintingSystem.RemoveService<IBuildEngineService>();
            }
        }

        void IStreamingExportPageBuildEngine.FillPage(DocumentBand root, IPageBuildEngine externalEngine)
        {
            if (base.rowBuilder == null)
            {
                this.BeforeFillPage();
            }
            this.StreamingRowBuilder.ExternalBuildEngine = externalEngine;
            this.StreamingRowBuilder.FillPage(base.rootBand, this.psPage.Rect);
        }

        protected internal override Pair<Dictionary<Brick, RectangleDF>, double> Execute() => 
            new Pair<Dictionary<Brick, RectangleDF>, double>(base.contentEngine.BrickList, base.rowBuilder.Offset.Y);

        protected internal Pair<Dictionary<Brick, RectangleDF>, double> ExecuteInfo() => 
            base.ExecuteInfo(base.document.InfoString);

        protected internal double GetCurrentRowBuilderOffset() => 
            base.rowBuilder.Offset.Y;

        protected override void ResetRootBand(DocumentBand rootBand)
        {
        }

        private StreamingPageRowBuilder StreamingRowBuilder =>
            (StreamingPageRowBuilder) base.rowBuilder;

        private StreamingExportContentEngine StreamingContentEngine =>
            (StreamingExportContentEngine) base.contentEngine;

        public IStreamingContinuousInfo ContinuousInfo =>
            ((StreamingExportContentEngine) base.contentEngine).ContinuousInfo;

        private protected class StreamingContinuousInfo : ContinuousExportInfo, IStreamingContinuousInfo
        {
            private PrintingSystemBase ps;
            private MergeBrickHelper mergeHelper;
            private StreamingExportPageBuildEngine engine;
            private Dictionary<Brick, RectangleDF> brickList;
            private double topBricksOffset;

            public StreamingContinuousInfo(StreamingExportPageBuildEngine engine, PrintingSystemBase ps)
            {
                this.ps = ps;
                this.engine = engine;
                this.mergeHelper = ((IServiceProvider) ps).GetService(typeof(MergeBrickHelper)) as MergeBrickHelper;
            }

            public void Add(Brick brick, RectangleDF rect)
            {
                this.brickList.Add(brick, RectangleDF.Offset(rect, 0.0, this.topBricksOffset));
            }

            public void EndCollecting()
            {
                double currentRowBuilderOffset = this.engine.GetCurrentRowBuilderOffset();
                Pair<Dictionary<Brick, RectangleF>, double> pair = this.engine.ExecuteMargin(DocumentBandKind.BottomMargin, BrickModifier.MarginalFooter);
                this.engine.JoinBricks(this.brickList, pair.First.ToRectangleDFEnumerable(), this.topBricksOffset + currentRowBuilderOffset);
            }

            public override void ExecuteExport(IBrickExportVisitor brickVisitor, IPrintingSystemContext context)
            {
                foreach (BrickLayoutInfo info in base.Bricks)
                {
                    base.ExportBrick(info.Brick, info.Rect, brickVisitor, context);
                }
            }

            public void FixChunk()
            {
                object syncObject = this.SyncObject;
                lock (syncObject)
                {
                    Dictionary<Brick, RectangleDF> brickList = this.brickList;
                    this.brickList = new Dictionary<Brick, RectangleDF>();
                    base.Bricks = new BrickLayoutInfoCollection(brickList);
                    this.ProcessBricks(brickList);
                }
            }

            private void ProcessBricks(Dictionary<Brick, RectangleDF> bricks)
            {
                if (this.mergeHelper != null)
                {
                    this.mergeHelper.ProcessBricks(this.ps, bricks);
                }
            }

            public void StartCollecting()
            {
                Pair<Dictionary<Brick, RectangleDF>, double> pair = this.engine.ExecuteInfo();
                Pair<Dictionary<Brick, RectangleF>, double> pair2 = this.engine.ExecuteMargin(DocumentBandKind.TopMargin, BrickModifier.MarginalHeader);
                this.brickList = pair.First;
                this.topBricksOffset = (pair2.First.Count > 0) ? ((double) GraphicsUnitConverter.Convert(this.ps.PageMargins.Top, 100f, 300f)) : ((double) 0);
                this.topBricksOffset += pair.Second;
                this.engine.JoinBricks(this.brickList, pair2.First.ToRectangleDFEnumerable(), pair.Second);
            }

            public object SyncObject =>
                this.brickList;
        }

        protected class StreamingExportContentEngine : ExportPageBuildEngine.ExportContentEngine
        {
            private StreamingExportPageBuildEngine.StreamingContinuousInfo continuousInfo;

            public StreamingExportContentEngine(PSPage psPage, StreamingExportPageBuildEngine engine, PrintingSystemBase ps) : base(psPage, ps)
            {
                this.continuousInfo = new StreamingExportPageBuildEngine.StreamingContinuousInfo(engine, ps);
            }

            public override PageUpdateData UpdateContent(DocumentBand docBand, PointD offset, RectangleF bounds, bool forceSplit)
            {
                object syncObject = this.continuousInfo.SyncObject;
                lock (syncObject)
                {
                    foreach (Brick brick in docBand.Bricks)
                    {
                        if (!brick.IsServiceBrick)
                        {
                            brick.PerformLayout(base.ps);
                            this.continuousInfo.Add(brick, RectangleDF.Offset(brick.InitialRect, bounds.X + offset.X, bounds.Y + offset.Y));
                        }
                    }
                }
                base.UpdateAdditionalInfo(docBand);
                return new PageUpdateData(bounds, true);
            }

            public IStreamingContinuousInfo ContinuousInfo =>
                this.continuousInfo;
        }
    }
}

