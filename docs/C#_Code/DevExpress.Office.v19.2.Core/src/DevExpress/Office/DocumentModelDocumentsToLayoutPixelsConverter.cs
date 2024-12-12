namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using System;

    public class DocumentModelDocumentsToLayoutPixelsConverter : DocumentModelUnitToLayoutUnitConverter
    {
        private readonly float dpi;

        public DocumentModelDocumentsToLayoutPixelsConverter(float dpi)
        {
            this.dpi = dpi;
        }

        public override int ToLayoutUnits(int value) => 
            Units.DocumentsToPixels(value, this.dpi);

        public override float ToLayoutUnits(float value) => 
            Units.DocumentsToPixelsF(value, this.dpi);

        public override int ToModelUnits(int value) => 
            Units.PixelsToDocuments(value, this.dpi);

        public override float ToModelUnits(float value) => 
            Units.PixelsToDocumentsF(value, this.dpi);
    }
}

