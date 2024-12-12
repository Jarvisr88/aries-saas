namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.DrawingML;

    public interface IDrawingCache
    {
        DevExpress.Office.Drawing.DrawingColorModelInfoCache DrawingColorModelInfoCache { get; }

        DevExpress.Office.Drawing.DrawingBlipFillInfoCache DrawingBlipFillInfoCache { get; }

        DevExpress.Office.Drawing.DrawingBlipTileInfoCache DrawingBlipTileInfoCache { get; }

        DevExpress.Office.Drawing.DrawingGradientFillInfoCache DrawingGradientFillInfoCache { get; }

        DevExpress.Office.Drawing.OutlineInfoCache OutlineInfoCache { get; }

        DevExpress.Office.DrawingML.Scene3DPropertiesInfoCache Scene3DPropertiesInfoCache { get; }

        DevExpress.Office.DrawingML.Scene3DRotationInfoCache Scene3DRotationInfoCache { get; }

        DevExpress.Office.Drawing.DrawingTextCharacterInfoCache DrawingTextCharacterInfoCache { get; }

        DevExpress.Office.Drawing.DrawingTextParagraphInfoCache DrawingTextParagraphInfoCache { get; }

        DevExpress.Office.Drawing.DrawingTextSpacingInfoCache DrawingTextSpacingInfoCache { get; }

        DevExpress.Office.Drawing.DrawingTextBodyInfoCache DrawingTextBodyInfoCache { get; }

        DevExpress.Office.Drawing.ShapeStyleInfoCache ShapeStyleInfoCache { get; }

        DevExpress.Office.Drawing.CommonDrawingLocksInfoCache CommonDrawingLocksInfoCache { get; }

        DevExpress.Office.Drawing.ShapePropertiesInfoCache ShapePropertiesInfoCache { get; }
    }
}

