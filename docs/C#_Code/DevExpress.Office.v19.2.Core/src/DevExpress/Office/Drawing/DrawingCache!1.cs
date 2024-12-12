namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class DrawingCache<TFormat> : IDrawingCache, IDisposable
    {
        private bool isDisposed;
        private DevExpress.Office.Drawing.DrawingColorModelInfoCache drawingColorModelInfoCache;
        private DevExpress.Office.Drawing.DrawingBlipFillInfoCache drawingBlipFillInfoCache;
        private DevExpress.Office.Drawing.DrawingBlipTileInfoCache drawingBlipTileInfoCache;
        private DevExpress.Office.Drawing.DrawingGradientFillInfoCache drawingGradientFillInfoCache;
        private DevExpress.Office.Drawing.OutlineInfoCache outlineInfoCache;
        private DevExpress.Office.DrawingML.Scene3DPropertiesInfoCache scene3DPropertiesInfoCache;
        private DevExpress.Office.DrawingML.Scene3DRotationInfoCache scene3DRotationInfoCache;
        private DevExpress.Office.Drawing.DrawingTextCharacterInfoCache drawingTextCharacterInfoCache;
        private DevExpress.Office.Drawing.DrawingTextParagraphInfoCache drawingTextParagraphInfoCache;
        private DevExpress.Office.Drawing.DrawingTextSpacingInfoCache drawingTextSpacingInfoCache;
        private DevExpress.Office.Drawing.DrawingTextBodyInfoCache drawingTextBodyInfoCache;
        private DevExpress.Office.Drawing.ShapeStyleInfoCache shapeStyleInfoCache;
        private DevExpress.Office.Drawing.CommonDrawingLocksInfoCache commonDrawingLocksInfoCache;
        private DevExpress.Office.Drawing.ShapePropertiesInfoCache shapePropertiesInfoCache;

        public DrawingCache(DocumentModelBase<TFormat> documentModelBase)
        {
            Guard.ArgumentNotNull(documentModelBase, "documentModelBase");
            this.Initialize(documentModelBase);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.isDisposed = true;
        }

        public List<SizeOfInfo> GetSizeOfInfo()
        {
            List<SizeOfInfo> list = ObjectSizeHelper.CalculateSizeOfInfo(this);
            list.Insert(0, ObjectSizeHelper.CalculateTotalSizeOfInfo(list, "ThemeOffice.Cache Total"));
            return list;
        }

        protected virtual void Initialize(DocumentModelBase<TFormat> documentModelBase)
        {
            DocumentModelUnitConverter unitConverter = documentModelBase.UnitConverter;
            this.drawingColorModelInfoCache = new DevExpress.Office.Drawing.DrawingColorModelInfoCache(unitConverter);
            this.drawingBlipFillInfoCache = new DevExpress.Office.Drawing.DrawingBlipFillInfoCache(unitConverter);
            this.drawingBlipTileInfoCache = new DevExpress.Office.Drawing.DrawingBlipTileInfoCache(unitConverter);
            this.drawingGradientFillInfoCache = new DevExpress.Office.Drawing.DrawingGradientFillInfoCache(unitConverter);
            this.outlineInfoCache = new DevExpress.Office.Drawing.OutlineInfoCache(unitConverter);
            this.scene3DPropertiesInfoCache = new DevExpress.Office.DrawingML.Scene3DPropertiesInfoCache(unitConverter);
            this.scene3DRotationInfoCache = new DevExpress.Office.DrawingML.Scene3DRotationInfoCache(unitConverter);
            this.drawingTextCharacterInfoCache = new DevExpress.Office.Drawing.DrawingTextCharacterInfoCache(unitConverter);
            this.drawingTextParagraphInfoCache = new DevExpress.Office.Drawing.DrawingTextParagraphInfoCache(unitConverter);
            this.drawingTextSpacingInfoCache = new DevExpress.Office.Drawing.DrawingTextSpacingInfoCache(unitConverter);
            this.drawingTextBodyInfoCache = new DevExpress.Office.Drawing.DrawingTextBodyInfoCache(unitConverter);
            this.shapeStyleInfoCache = new DevExpress.Office.Drawing.ShapeStyleInfoCache(unitConverter);
            this.commonDrawingLocksInfoCache = new DevExpress.Office.Drawing.CommonDrawingLocksInfoCache(unitConverter);
            this.shapePropertiesInfoCache = new DevExpress.Office.Drawing.ShapePropertiesInfoCache(unitConverter);
        }

        protected internal bool IsDisposed =>
            this.isDisposed;

        public DevExpress.Office.Drawing.DrawingColorModelInfoCache DrawingColorModelInfoCache =>
            this.drawingColorModelInfoCache;

        public DevExpress.Office.Drawing.DrawingBlipFillInfoCache DrawingBlipFillInfoCache =>
            this.drawingBlipFillInfoCache;

        public DevExpress.Office.Drawing.DrawingBlipTileInfoCache DrawingBlipTileInfoCache =>
            this.drawingBlipTileInfoCache;

        public DevExpress.Office.Drawing.DrawingGradientFillInfoCache DrawingGradientFillInfoCache =>
            this.drawingGradientFillInfoCache;

        public DevExpress.Office.Drawing.OutlineInfoCache OutlineInfoCache =>
            this.outlineInfoCache;

        public DevExpress.Office.DrawingML.Scene3DPropertiesInfoCache Scene3DPropertiesInfoCache =>
            this.scene3DPropertiesInfoCache;

        public DevExpress.Office.DrawingML.Scene3DRotationInfoCache Scene3DRotationInfoCache =>
            this.scene3DRotationInfoCache;

        public DevExpress.Office.Drawing.DrawingTextCharacterInfoCache DrawingTextCharacterInfoCache =>
            this.drawingTextCharacterInfoCache;

        public DevExpress.Office.Drawing.DrawingTextParagraphInfoCache DrawingTextParagraphInfoCache =>
            this.drawingTextParagraphInfoCache;

        public DevExpress.Office.Drawing.DrawingTextSpacingInfoCache DrawingTextSpacingInfoCache =>
            this.drawingTextSpacingInfoCache;

        public DevExpress.Office.Drawing.DrawingTextBodyInfoCache DrawingTextBodyInfoCache =>
            this.drawingTextBodyInfoCache;

        public DevExpress.Office.Drawing.ShapeStyleInfoCache ShapeStyleInfoCache =>
            this.shapeStyleInfoCache;

        public DevExpress.Office.Drawing.CommonDrawingLocksInfoCache CommonDrawingLocksInfoCache =>
            this.commonDrawingLocksInfoCache;

        public DevExpress.Office.Drawing.ShapePropertiesInfoCache ShapePropertiesInfoCache =>
            this.shapePropertiesInfoCache;
    }
}

