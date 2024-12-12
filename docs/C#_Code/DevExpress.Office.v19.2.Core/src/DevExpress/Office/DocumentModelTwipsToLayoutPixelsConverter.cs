namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using System;

    public class DocumentModelTwipsToLayoutPixelsConverter : DocumentModelUnitToLayoutUnitConverter
    {
        private readonly float dpi;

        public DocumentModelTwipsToLayoutPixelsConverter(float dpi)
        {
            this.dpi = dpi;
        }

        public override int ToLayoutUnits(int value) => 
            Units.TwipsToPixels(value, this.dpi);

        public override float ToLayoutUnits(float value) => 
            Units.TwipsToPixelsF(value, this.dpi);

        public override int ToModelUnits(int value) => 
            Units.PixelsToTwips(value, this.dpi);

        public override float ToModelUnits(float value) => 
            Units.PixelsToTwipsF(value, this.dpi);
    }
}

