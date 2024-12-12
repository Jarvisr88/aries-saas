namespace DevExpress.Office.API.Internal
{
    using DevExpress.Office;
    using System;

    public class DocumentsToModelUnitsConverter : UnitConverter
    {
        public DocumentsToModelUnitsConverter(DocumentModelUnitConverter unitConverter) : base(unitConverter)
        {
        }

        public override float FromUnits(float value) => 
            base.Converter.ModelUnitsToDocumentsF(value);

        public override float ToUnits(float value) => 
            base.Converter.DocumentsToModelUnitsF(value);
    }
}

