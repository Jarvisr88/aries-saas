namespace DevExpress.Office.Import.Binary
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;

    public static class ModelShapePathImportHelper
    {
        private static void SetupCustomPathsSize(ModelShapeCustomGeometry customGeometry, IEnumerable<object> artProperties)
        {
            DrawingGeometryRight propertyByType = OfficeArtPropertiesHelper.GetPropertyByType(artProperties, typeof(DrawingGeometryRight)) as DrawingGeometryRight;
            DrawingGeometryBottom bottom = OfficeArtPropertiesHelper.GetPropertyByType(artProperties, typeof(DrawingGeometryBottom)) as DrawingGeometryBottom;
            int num = (propertyByType != null) ? propertyByType.Value : 0x5460;
            int num2 = (bottom != null) ? bottom.Value : 0x5460;
            foreach (ModelShapePath path in customGeometry.Paths)
            {
                path.Width = num;
                path.Height = num2;
            }
        }

        private static void SetupCustomXlsAdjustValues(ModelShapeCustomGeometry geometry, int?[] adjustValues)
        {
            for (int i = 0; i < geometry.AdjustValues.Count; i++)
            {
                if ((i < adjustValues.Length) && (adjustValues[i] != null))
                {
                    geometry.AdjustValues[i].Formula = ModelShapeGuideFormula.FromString("val " + adjustValues[i].ToString());
                }
            }
        }

        private static void SetupPresetGeometry(ShapeProperties shapeProperties, int widthEmu, int heightEmu, IEnumerable<object> artProperties)
        {
            BinaryDrawingImportHelper.SetupAdjustValues(shapeProperties, widthEmu, heightEmu, artProperties);
        }

        private static void SetupShapeCustomGeometry(ModelShapeCustomGeometry customGeometry, IEnumerable<object> artProperties, int shapeTypeCode)
        {
            ModelShapeCustomGeometry customGeometryByShapeTypeCode = BinaryCustomGeometryGenerator.GetCustomGeometryByShapeTypeCode(shapeTypeCode);
            customGeometry.CopyFrom(customGeometryByShapeTypeCode);
            if (artProperties != null)
            {
                SetupCustomPathsSize(customGeometry, artProperties);
                int?[] adjustValues = BinaryDrawingImportHelper.GetAdjustValues(artProperties);
                SetupCustomXlsAdjustValues(customGeometry, adjustValues);
            }
        }

        public static void SetupShapeGeometry(ShapeProperties shapeProperties, OfficeArtPropertiesBase artProperties, float width, float height, int shapeTypeCode)
        {
            SetupShapeGeometry(shapeProperties, artProperties?.Properties, width, height, shapeTypeCode);
        }

        public static void SetupShapeGeometry(ShapeProperties shapeProperties, IEnumerable<object> artProperties, float width, float height, int shapeTypeCode)
        {
            DocumentModelUnitConverter unitConverter = shapeProperties.DocumentModel.UnitConverter;
            int widthEmu = unitConverter.CeilingModelUnitsToEmu(width);
            int heightEmu = unitConverter.CeilingModelUnitsToEmu(height);
            if (shapeProperties.ShapeType != ShapePreset.None)
            {
                SetupPresetGeometry(shapeProperties, widthEmu, heightEmu, artProperties);
            }
            else
            {
                ModelShapeCustomGeometry customGeometry = shapeProperties.CustomGeometry;
                if ((shapeTypeCode != 0) && (shapeTypeCode != 100))
                {
                    SetupShapeCustomGeometry(customGeometry, artProperties, shapeTypeCode);
                }
                else
                {
                    BinaryDrawingImportHelper.SetupShapeCustomGeometry(customGeometry, widthEmu, heightEmu, artProperties);
                }
            }
        }
    }
}

