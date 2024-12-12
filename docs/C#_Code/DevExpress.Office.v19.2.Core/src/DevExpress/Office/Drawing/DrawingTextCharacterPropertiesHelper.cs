namespace DevExpress.Office.Drawing
{
    using DevExpress.Export.Xl;
    using System;

    public static class DrawingTextCharacterPropertiesHelper
    {
        public static string GetFontTypeFace(DrawingTextCharacterProperties runProperties, ShapeStyle shapeStyle) => 
            GetFontTypeFace((IDrawingTextCharacterProperties) runProperties, shapeStyle);

        internal static string GetFontTypeFace(IDrawingTextCharacterProperties runProperties, ShapeStyle shapeStyle)
        {
            XlFontSchemeStyles fontReferenceIdx = shapeStyle.FontReferenceIdx;
            if (runProperties == null)
            {
                return shapeStyle.DocumentModel.OfficeTheme.FontScheme.MinorFont.Latin.Typeface;
            }
            string typeface = runProperties.Latin.Typeface;
            if (string.IsNullOrEmpty(typeface) || typeface.StartsWith("+mn"))
            {
                fontReferenceIdx ??= XlFontSchemeStyles.Minor;
                typeface = shapeStyle.DocumentModel.OfficeTheme.FontScheme.GetTypeface(fontReferenceIdx, runProperties.Language);
            }
            return typeface;
        }
    }
}

