namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using System;

    public class DocumentModelTwipsToLayoutDocumentsConverter : DocumentModelUnitToLayoutUnitConverter
    {
        public override int ToLayoutUnits(int value) => 
            Units.TwipsToDocuments(value);

        public override float ToLayoutUnits(float value) => 
            Units.TwipsToDocumentsF(value);

        public override int ToModelUnits(int value) => 
            Units.DocumentsToTwips(value);

        public override float ToModelUnits(float value) => 
            Units.DocumentsToTwipsF(value);
    }
}

