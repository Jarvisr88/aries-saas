namespace DevExpress.Office.Drawing
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class ShapeStyle : DrawingUndoableIndexBasedObject<ShapeStyleInfo>, ISupportsCopyFrom<ShapeStyle>
    {
        public ShapeStyle(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.EffectColor = DrawingColor.Create(documentModel, DXColor.Empty);
            this.LineColor = DrawingColor.Create(documentModel, DXColor.Empty);
            this.FillColor = DrawingColor.Create(documentModel, DXColor.Empty);
            this.FontColor = DrawingColor.Create(documentModel, DXColor.Empty);
        }

        public void CopyFrom(ShapeStyle value)
        {
            base.CopyFrom(value);
            this.EffectColor.CopyFrom(value.EffectColor);
            this.LineColor.CopyFrom(value.LineColor);
            this.FillColor.CopyFrom(value.FillColor);
            this.FontColor.CopyFrom(value.FontColor);
        }

        public override DocumentModelChangeActions GetBatchUpdateChangeActions() => 
            DocumentModelChangeActions.None;

        protected internal override UniqueItemsCache<ShapeStyleInfo> GetCache(IDocumentModel documentModel) => 
            base.DrawingCache.ShapeStyleInfoCache;

        private DocumentModelChangeActions SetEffectReferenceIdx(ShapeStyleInfo info, int value)
        {
            info.EffectReferenceIdx = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetFillReferenceIdx(ShapeStyleInfo info, int value)
        {
            info.FillReferenceIdx = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetFontReferenceIdx(ShapeStyleInfo info, XlFontSchemeStyles value)
        {
            info.FontReferenceIdx = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetLineReferenceIdx(ShapeStyleInfo info, int value)
        {
            info.LineReferenceIdx = value;
            return DocumentModelChangeActions.None;
        }

        public int EffectReferenceIdx
        {
            get => 
                base.Info.EffectReferenceIdx;
            set
            {
                if (this.EffectReferenceIdx != value)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<ShapeStyleInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetEffectReferenceIdx), value);
                }
            }
        }

        public int FillReferenceIdx
        {
            get => 
                base.Info.FillReferenceIdx;
            set
            {
                if (this.FillReferenceIdx != value)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<ShapeStyleInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetFillReferenceIdx), value);
                }
            }
        }

        public XlFontSchemeStyles FontReferenceIdx
        {
            get => 
                base.Info.FontReferenceIdx;
            set
            {
                if (this.FontReferenceIdx != value)
                {
                    this.SetPropertyValue<XlFontSchemeStyles>(new UndoableIndexBasedObject<ShapeStyleInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<XlFontSchemeStyles>(this.SetFontReferenceIdx), value);
                }
            }
        }

        public int LineReferenceIdx
        {
            get => 
                base.Info.LineReferenceIdx;
            set
            {
                if (this.LineReferenceIdx != value)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<ShapeStyleInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetLineReferenceIdx), value);
                }
            }
        }

        public DrawingColor EffectColor { get; set; }

        public DrawingColor LineColor { get; set; }

        public DrawingColor FillColor { get; set; }

        public DrawingColor FontColor { get; set; }
    }
}

