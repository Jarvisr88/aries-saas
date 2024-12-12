namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using System;

    public class DocumentModelDocumentsToLayoutTwipsConverter : DocumentModelUnitToLayoutUnitConverter
    {
        public override int ToLayoutUnits(int value) => 
            Units.DocumentsToTwips(value);

        public override float ToLayoutUnits(float value) => 
            Units.DocumentsToTwipsF(value);

        public override int ToModelUnits(int value) => 
            Units.TwipsToDocuments(value);

        public override float ToModelUnits(float value) => 
            Units.TwipsToDocumentsF(value);
    }
}

