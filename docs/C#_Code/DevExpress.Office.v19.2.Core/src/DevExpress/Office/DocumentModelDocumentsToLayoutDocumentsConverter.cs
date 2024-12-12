namespace DevExpress.Office
{
    using System;

    public class DocumentModelDocumentsToLayoutDocumentsConverter : DocumentModelUnitToLayoutUnitConverter
    {
        public override int ToLayoutUnits(int value) => 
            value;

        public override float ToLayoutUnits(float value) => 
            value;

        public override int ToModelUnits(int value) => 
            value;

        public override float ToModelUnits(float value) => 
            value;
    }
}

