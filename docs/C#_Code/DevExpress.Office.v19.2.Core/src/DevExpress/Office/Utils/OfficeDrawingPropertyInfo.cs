namespace DevExpress.Office.Utils
{
    using System;

    public class OfficeDrawingPropertyInfo
    {
        private short identifier;
        private System.Type type;

        public OfficeDrawingPropertyInfo(short identifier, System.Type type)
        {
            this.identifier = identifier;
            this.type = type;
        }

        public short Identifier =>
            this.identifier;

        public System.Type Type =>
            this.type;
    }
}

