namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ModelShapeGuideDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly ModelShapeGuide modelShapeGuide;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public ModelShapeGuideDestination(DestinationAndXmlBasedImporter importer, ModelShapeGuide modelShapeGuide, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.modelShapeGuide = modelShapeGuide;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        private static ModelShapeGuideFormula CreateModelShapeGuideFormula(string formula, AdjustableCoordinateCache adjustableCoordinateCache)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return ModelShapeGuideFormula.Empty;
            }
            string[] strArray = formula.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 0)
            {
                return ModelShapeGuideFormula.Empty;
            }
            string str = strArray[0];
            string str2 = (strArray.Length >= 2) ? strArray[1] : string.Empty;
            string str3 = (strArray.Length >= 3) ? strArray[2] : string.Empty;
            string str4 = (strArray.Length >= 4) ? strArray[3] : string.Empty;
            return new ModelShapeGuideFormula(ModelShapeGuideFormula.GetFormulaTypeFromString(str), adjustableCoordinateCache.GetCachedAdjustableCoordinate(str2), adjustableCoordinateCache.GetCachedAdjustableCoordinate(str3), adjustableCoordinateCache.GetCachedAdjustableCoordinate(str4));
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            string attribute = reader.GetAttribute("fmla");
            string str2 = reader.GetAttribute("name");
            if (string.IsNullOrEmpty(attribute) || string.IsNullOrEmpty(str2))
            {
                this.Importer.ThrowInvalidFile();
            }
            this.modelShapeGuide.Formula = CreateModelShapeGuideFormula(attribute, this.adjustableCoordinateCache);
            this.modelShapeGuide.Name = str2;
        }
    }
}

